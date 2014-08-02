using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace AndreyTradeProject.Lib
{
  /// <summary>
  /// Интерфейс отправки email
  /// </summary>
  public interface IEmailSender
  {
    /// <summary>
    /// Отправить сообщение
    /// </summary>
    /// <returns>True - в случаи успешной отправки, false - в ином случаи</returns>
    bool Send();
  }

  public class Email : IEmailSender
  {
    public Email(string sendAddress)
    {
      _EmailAddress = sendAddress;
    }

    private const string _SendMailAdress = "yourmail@mail.ru";
    private readonly string _EmailAddress;

    public bool Send()
    {
      return false;
      //TODO:Андрюха Логика отправки email

      //using (MailMessage mail = new MailMessage(_SendMailAdress, _EmailAddress))
      //{
      //  SmtpClient client = new SmtpClient();
      //  client.Port = 25;
      //  client.DeliveryMethod = SmtpDeliveryMethod.Network;
      //  client.UseDefaultCredentials = false;
      //  client.Host = "smtp.google.com";
      //  mail.Subject = "this is a test email.";
      //  mail.Body = "this is my test email body";
      //  client.Send(mail);
      //}
    }
  }
}