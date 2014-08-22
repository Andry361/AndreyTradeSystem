using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
      void Send(string body);
  }

  public class Email : IEmailSender
  {

    private const string _SendMailAdress = "Hsmel73@mail.ru";
    private readonly string _EmailAddress;

    public Email(string sendAddress)
    {
      _EmailAddress = sendAddress;
    }

    public void Send(string body)
    {
        using (MailMessage mm = new MailMessage(_SendMailAdress, _EmailAddress))
        {
            mm.Subject = "Номер сертификата!";
            mm.Body = "Услуга оказывается ЗАО\"Интеренэшнл Маркетинг Групп Фарм\", \nОГРН 1037789083133\nwww.jobagent.msk.ru\nСтоймость индевидуального номера сертификата 5000р, в т. ч. НДС 18%\nАктивировать до 31.12.2015 г.\nВаш индивидуальный номер: " + body + ". Не показываете его третим лицам!\nДля активации услуги необходимо направить письмо по адресу info@jobagent.msk.ru или\nпозвонить по телефону: 8 (495) 998-59-99, сообщить дату выдачи сертификата, его номер и период\nактивации.";
            mm.IsBodyHtml = false;

            using (SmtpClient sc = new SmtpClient("smtp.mail.ru", 25))
            {
                sc.EnableSsl = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential(_SendMailAdress, "180992nen2011djnf");
                sc.Send(mm);          
            }
        }
    }
  }
}