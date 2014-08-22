using AndreyTradeProject.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;

namespace AndreyTradeProject.Controllers
{
  public class AutorizeController : AbstractController
  {
    public ActionResult Index(UserModel model)
    {
      model = model ?? new UserModel();

      if (Request.HttpMethod == "POST" && ModelState.IsValid)
      {
        bool isExists = _NhibernateSession.Query<D_User>().Any(x => x.Login == model.Login);

        if (isExists)
        {
          D_User user = _NhibernateSession.Query<D_User>().Where(x => x.Login == model.Login).FirstOrDefault();
          HttpContext.Session.Add("Id", user.Id);
        }
        else
        {
          //UserType s = User;
          D_User d_user = new D_User
          {
            Login = model.Login,
            Password = model.Password,
            Name = model.Name,
            Patronimic = model.Patronimic,
            Surname = model.Surname,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            UserType = UserType.User
          };

          _NhibernateSession.Save(d_user);

          #region Авторизация
          HttpContext.Session.Add("Id", d_user.Id);
          #endregion
        }
        return Redirect(Url.Action("Index", "Stock"));
      }
      return View(model);
    }

    public ActionResult Identification()
    {
      ModelState.Clear();

      var model = (IdentificationModel)ViewData["IdentificationModel"];

      TryUpdateModel<IdentificationModel>(model);

      if (Request.HttpMethod == "POST" && ModelState.IsValid)
      {
        bool isExists = _NhibernateSession.Query<D_User>().Any(x => (x.Login == model.Login));
        //че то жесть!!!!..
        //bool isExistsNaxNeTak = _NhibernateSession.Query<D_User>().Any(x => (x.Password == model.Password));
        //if (isExists && isExistsNaxNeTak)
        if (isExists)
        {
          D_User user = _NhibernateSession.Query<D_User>().Where(x => x.Login == model.Login).FirstOrDefault();
          HttpContext.Session.Add("Id", user.Id);

          model.IsAuthorized = true;

          return Redirect(Url.Action("Index", "Stock"));
        }
      }

      return View(model);
    }
    public ActionResult Logout()
    {
      HttpContext.Session.Remove("Id");

      if (Request.UrlReferrer != null)
      {
        return Redirect(Url.Action("Index", "Home"));
      }
      else
      {
        return null;
      }
    }
  }
}