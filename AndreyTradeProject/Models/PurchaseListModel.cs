using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AndreyTradeProject.Models
{
  public class PurchaseListModel
  {
    public IEnumerable<D_Purchase> Purchases { get; set; }
  }
}