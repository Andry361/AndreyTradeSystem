using FluentNHibernate.Mapping;
using NHibernate.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Proxy;

namespace Data
{
  #region Entity
  public abstract class D_BaseObject
  {
    public virtual long Id { get; set; }
    public virtual DateTime CreationDateTime { get; set; }
  }

  #region User
  public class D_User : D_BaseObject
  {
    public D_User()
    {
      UserType = Data.UserType.User;
    }

    public virtual string Login { get; set; }
    public virtual string Password { get; set; }
    public virtual string Name { get; set; }
    public virtual string Surname { get; set; }
    public virtual string Patronimic { get; set; }
    public virtual string Email { get; set; }
    public virtual string PhoneNumber { get; set; }
    public virtual UserType UserType { get; set; }

  }

  /// <summary>
  /// Роль пользователя в системе
  /// </summary>
  public enum UserType : int
  {
    User = 1,
    Administrator = 2
  }
  #endregion

  public class D_Stock : D_BaseObject
  {
    public virtual string Number { get; set; }
    public virtual D_Purchase Purchase { get; set; }
    /// <summary>
    /// Прода ли сертификат
    /// </summary>
    public virtual bool IsSold { get; set; }
  }

  public class D_Purchase : D_BaseObject
  {
    public D_Purchase()
    {
      Stocks = new List<D_Stock>();
    }

    public virtual D_User User { get; set; }
    public virtual IList<D_Stock> Stocks { get; set; }
  }
  #endregion

  #region Mapping
  public class D_BaseObject_Map : ClassMap<D_BaseObject>
  {
    public D_BaseObject_Map()
    {
      Id(x => x.Id).GeneratedBy.HiLo("10").CustomType<Int64>();
      Map(x => x.CreationDateTime).Not.Nullable();
    }
  }

  public class D_User_Map : SubclassMap<D_User>
  {
    public D_User_Map()
    {
      Map(x => x.Login).Not.Nullable().Length(50);
      Map(x => x.Password).Not.Nullable().Length(50);
      Map(x => x.Name).Not.Nullable().Length(255);
      Map(x => x.Surname).Not.Nullable().Length(255);
      Map(x => x.Patronimic).Not.Nullable().Length(255);
      Map(x => x.Email).Not.Nullable().Length(100);
      Map(x => x.PhoneNumber).Not.Nullable().Length(20);
      Map(x => x.UserType).CustomType<int>().Default("1");
    }
  }

  public class D_Stock_Map : SubclassMap<D_Stock>
  {
    public D_Stock_Map()
    {
      Map(x => x.Number).Not.Nullable().Length(20);
      Map(x => x.IsSold).Not.Nullable().Default("0");
      References(x => x.Purchase).Column("PurchaseId").Nullable().LazyLoad();
    }
  }

  public class D_Purchase_Map : SubclassMap<D_Purchase>
  {
    public D_Purchase_Map()
    {
      References(x => x.User).Not.Nullable().Column("UserId").LazyLoad().Cascade.SaveUpdate();
      HasMany(x => x.Stocks).KeyColumn("PurchaseId").Cascade.SaveUpdate();
    }
  }
  #endregion

  #region Event listeners
  public class PreInsertEvent : IPreInsertEventListener
  {
    public bool OnPreInsert(NHibernate.Event.PreInsertEvent @event)
    {
      D_BaseObject baseObject = (@event.Entity as D_BaseObject);

      if (baseObject == null)
        return false;

      #region Задаю время создания
      int createdDateTimeIndex = Array.IndexOf(@event.Persister.PropertyNames, "CreationDateTime");

      DateTime creationDate = DateTime.UtcNow;
      @event.State[createdDateTimeIndex] = creationDate;
      baseObject.CreationDateTime = creationDate;
      #endregion

      return false;
    }
  }

  public class PreUpdateEvent : IPreUpdateEventListener
  {
    public bool OnPreUpdate(NHibernate.Event.PreUpdateEvent @event)
    {
      D_BaseObject baseObject = (@event.Entity as D_BaseObject);

      if (baseObject == null)
        return false;

      return false;
    }
  }
  #endregion
}
