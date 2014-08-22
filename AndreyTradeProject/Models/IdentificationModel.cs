using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AndreyTradeProject.Models
{
    public class IdentificationModel
    {
        [Required(ErrorMessageResourceName = "UserModel__ValidationException_Login", ErrorMessageResourceType = typeof(AndreyTradeProject.Properties.GeneralResource))]
        [Display(Name = "Логин")]
        public string Login { get; set; }
        [Required(ErrorMessageResourceName = "UserModel__ValidationException_Password", ErrorMessageResourceType = typeof(AndreyTradeProject.Properties.GeneralResource))]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}