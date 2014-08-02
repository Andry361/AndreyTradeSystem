using AndreyTradeProject.Controllers;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AndreyTradeProject.Models
{
  public class TradeBasketModel
  {
    public D_User User { get; set; }
    public IEnumerable<D_Purchase> Purchases { get; set; }
  }
}