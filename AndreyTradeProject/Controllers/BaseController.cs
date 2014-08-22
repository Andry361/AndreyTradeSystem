using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AndreyTradeProject.Controllers
{
  public abstract class AbstractController : Controller
  {
    protected ISession _NhibernateSession;

    protected override void Initialize(System.Web.Routing.RequestContext requestContext)
    {
      base.Initialize(requestContext);

      
      _NhibernateSession = Data.NhibernateConfiguration.Default.SessionFactory.OpenSession();
      _NhibernateSession.BeginTransaction();


      var currentUser = Lib.Session.Default.GetCurrentUser(_NhibernateSession);
      var model = new AndreyTradeProject.Models.IdentificationModel
      {
        IsAuthorized = currentUser != null ? true : false,
        User = currentUser
      };

      ViewData["IdentificationModel"] = model;

      Lib.Session.NhibernateSession = _NhibernateSession;
    }

    protected override void Dispose(bool disposing)
    {
      _NhibernateSession.Transaction.Commit();
      _NhibernateSession.Transaction.Dispose();
      _NhibernateSession.Dispose();

      base.Dispose(disposing);
    }
  }
}