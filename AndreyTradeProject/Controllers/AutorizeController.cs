﻿using AndreyTradeProject.Models;
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
            D_User d_user = new D_User
            {
                Login = model.Login,
                Name = model.Name,
                Patronimic = model.Patronimic,
                Surname = model.Surname,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
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
  }
}