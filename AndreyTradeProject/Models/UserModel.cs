using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace AndreyTradeProject.Models
{
  public class UserModel
  {
    [Required(ErrorMessageResourceName = "UserModel__ValidationException_NameRequired", ErrorMessageResourceType = typeof(AndreyTradeProject.Properties.GeneralResource))]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public string Patronimic { get; set; }
  }
}