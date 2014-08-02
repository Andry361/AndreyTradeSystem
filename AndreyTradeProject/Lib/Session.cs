using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
using AndreyTradeProject.Controllers;
using NHibernate;

namespace AndreyTradeProject.Lib
{
  /// <summary>
  /// Сессия ASP.NET.
  /// </summary>
  public class Session
  {
    private Session()
    {
    }

    private readonly static object _Locker = new { };
    private static Session _Instance;

    public static Session Default
    {
      get
      {
        if (_Instance == null)
        {
          lock (_Locker)
          {
            _Instance = new Session();
          }
        }

        return _Instance;
      }
    }

    #region CurrentUser
    private static D_User _CurrentUser
    {
      get
      {
        return System.Web.HttpContext.Current.Items["CurrentUser"] as D_User;
      }
    }

    /// <summary>
    /// Текущий пользователь.
    /// </summary>
    public D_User GetCurrentUser(NHibernate.ISession session)
    {
      if (session == null)
        throw new ArgumentNullException("session");
      
        if ( HttpContext.Current != null)
        {
          long? userId = HttpContext.Current.Session["Id"] as Int64?;

          lock (_Locker)
          {
            if (userId != null)
              System.Web.HttpContext.Current.Items["CurrentUser"] = session.Query<D_User>().Where(x => x.Id == userId.Value).FirstOrDefault();
          }
        }

      return _CurrentUser;
    }
    #endregion

    #region Purchases
    public IEnumerable<Entity> GetPurchases()
    {
      if (HttpContext.Current == null)
        throw new ArgumentNullException("Http context is null");

      List<Entity> entities = new List<Entity>();


      if (HttpContext.Current.Session["Purchases"] != null)
        entities.AddRange((IEnumerable<Entity>)HttpContext.Current.Session["Purchases"]);

      return entities;
    }
    #endregion
  }
}