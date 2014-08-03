using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AndreyTradeProject.Lib
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
  public class AuthorizeAttribute : ActionFilterAttribute
  {
    public AuthorizeAttribute(params UserType[] userTypes)
    {
      _UserTypes = userTypes.ToList();
    }

    private readonly List<UserType> _UserTypes = new List<UserType>();

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if (HttpContext.Current.Session["Id"] == null)
      {
        filterContext.Result = new RedirectResult("/Autorize/Index");
      }
      else
      {
        D_User user = Lib.Session.NhibernateSession.QueryOver<D_User>().Where(x => x.Id == (long)HttpContext.Current.Session["Id"]).List().FirstOrDefault();

        if (user == null)
          filterContext.Result = new RedirectResult("/Autorize/Index");

        if (_UserTypes.Count > 0)
        {
          if (!_UserTypes.Contains(user.UserType))
            filterContext.Result = new RedirectResult("/Autorize/Index");
        }
      }

      base.OnActionExecuting(filterContext);
    }
  }
}