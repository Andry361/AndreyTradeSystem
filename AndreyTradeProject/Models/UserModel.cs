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
    [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessageResourceName = "UserModel__ValidationException_EmailRequired", ErrorMessageResourceType = typeof(AndreyTradeProject.Properties.GeneralResource))]
    public string Email { get; set; }
    [Required(ErrorMessageResourceName = "UserModel__ValidationException_PhoneNUmberRequired", ErrorMessageResourceType = typeof(AndreyTradeProject.Properties.GeneralResource))]
    [RegularExpression(@"^\s*\+?\s*([0-9][\s-]*){9,}$", ErrorMessageResourceName = "UserModel__ValidationException_PhoneNUmberInvalid", ErrorMessageResourceType = typeof
      (AndreyTradeProject.Properties.GeneralResource))]
    public string PhoneNumber { get; set; }
  }
}