using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace AndreyTradeProject.Models
{
  public class UserModel
  {
    [Required(ErrorMessageResourceName = "UserModel__ValidationException_Login", ErrorMessageResourceType = typeof(AndreyTradeProject.Properties.GeneralResource))]
    [Display(Name= "Логин")]
    public string Login { get; set; }
    [Required(ErrorMessageResourceName = "UserModel__ValidationException_NameRequired", ErrorMessageResourceType = typeof(AndreyTradeProject.Properties.GeneralResource))]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public string Patronimic { get; set; }
    [Required(ErrorMessageResourceName = "UserModel__ValidationException_EmailRequired", ErrorMessageResourceType = typeof(AndreyTradeProject.Properties.GeneralResource))]
    public string Email { get; set; }
    [Required(ErrorMessageResourceName = "UserModel__ValidationException_PhoneNUmberRequired", ErrorMessageResourceType = typeof(AndreyTradeProject.Properties.GeneralResource))]
    public string PhoneNumber { get; set; }
  }
}