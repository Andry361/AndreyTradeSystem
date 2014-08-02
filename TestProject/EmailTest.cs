using AndreyTradeProject.Lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
  //TODO:Андрей Реализивать функционал данного теста
  [TestClass]
  public class EmailTest
  {
    [TestMethod]
    public void SendMail()
    {
      IEmailSender mail = new Email(null);
      bool isSend = mail.Send();

      Assert.IsTrue(isSend, "Отправка не удалась");
    }
  }
}
