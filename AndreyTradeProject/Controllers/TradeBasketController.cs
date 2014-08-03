using AndreyTradeProject.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;
using AndreyTradeProject.Lib;

namespace AndreyTradeProject.Controllers
{
  [AndreyTradeProject.Lib.Authorize(UserType.User, UserType.Administrator)]
  public class TradeBasketController : AbstractController
  {
    public ActionResult Browse()
    {
      TradeBasketModel model = new TradeBasketModel
      {
        User = AndreyTradeProject.Lib.Session.Default.GetCurrentUser(_NhibernateSession),
        Entities = ((List<Entity>)Session["Purchases"])
      };

      return View(model);
    }

    public ActionResult AddEntity(Entity._Type entityType)
    {
      switch (entityType)
      {
        case Entity._Type.Stock:
          {
            Session["Purchases"] = Session["Purchases"] ?? new List<Entity>();

            Entity stock = new Entity { Type = Entity._Type.Stock, Count = 1 };

            ((List<Entity>)Session["Purchases"]).Add(stock);
          }
          break;

        case Entity._Type.Microphone:
          {
            Session["Purchases"] = Session["Purchases"] ?? new List<Entity>();

            Entity microphone = new Entity { Type = Entity._Type.Microphone, Count = 1 };

            ((List<Entity>)Session["Purchases"]).Add(microphone);
          }
          break;
      }

      return Redirect(Url.Action("Browse"));
    }

    /// <summary>
    /// Покупка сертификатов
    /// </summary>
    /// <param name="stockCount">Количество сертификатов</param>
    /// <returns></returns>
    public ActionResult PlaceAnOrder(uint stockCount)
    {
      D_User d_user = Lib.Session.Default.GetCurrentUser(_NhibernateSession);

      for (uint index = 0; index < stockCount; index++)
      {
        D_Stock nextStock = _NhibernateSession.Query<D_Stock>().FirstOrDefault(x => !x.IsSold);

        if (nextStock == null)
          throw new ApplicationException(AndreyTradeProject.Properties.GeneralResource.PurchaseAnOrder__Exception_NoFreStocks);

        D_Purchase purchase = new D_Purchase
        {
          User = Lib.Session.Default.GetCurrentUser(_NhibernateSession)
        };

        nextStock.IsSold = true;
        purchase.Stocks.Add(nextStock);
        _NhibernateSession.Save(purchase);

        #region Отпрака email
        IEmailSender email = new Email(Lib.Session.Default.GetCurrentUser(_NhibernateSession).Email);
        email.Send(nextStock.Number);
        #endregion
      }

      ((List<Entity>)Session["Purchases"]).Clear();
    
      return View("_Success");
    }
  }

  public class Entity
  {
    public _Type Type { get; set; }
    public ushort Count { get; set; }

    public enum _Type : int
    {
      Stock = 0,
      Microphone = 1
    }
  }
}