using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;
using AndreyTradeProject.Models;

namespace AndreyTradeProject.Controllers
{
  public class PurchaseListController : AbstractController
  {
    public ActionResult List()
    {
      List<D_Purchase> purchases = _NhibernateSession.Query<D_Purchase>().ToList();

      PurchaseListModel model = new PurchaseListModel
      {
        Purchases = purchases
      };

      return View(model);
    }
  }
}