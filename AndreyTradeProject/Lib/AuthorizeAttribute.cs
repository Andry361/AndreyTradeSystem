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
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if (HttpContext.Current.Session["Id"] == null)
      {
        filterContext.Result = new RedirectResult("/Autorize/Index");
      }

      base.OnActionExecuting(filterContext);
    }
  }
}