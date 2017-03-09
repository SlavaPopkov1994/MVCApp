using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcApplication1.Models
{
    public class ProfileViewModel
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Middle_name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "Длина строки должна быть меньше 100 символов")]
        public string Comments { get; set; }
        public string Password { get; set; }
        public string Upload { get; set; }
        public HttpPostedFileBase Image { get; set; }
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string Repeat_password { get; set; }
       

    }
}