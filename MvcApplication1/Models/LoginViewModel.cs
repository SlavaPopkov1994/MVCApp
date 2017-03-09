using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class LoginViewModel
    {
        public bool UserNotExist { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class NewLoginViewModel
    {
        public HttpPostedFileBase Upload { get; set; }
        [Required(ErrorMessage = "Ошибка, поле «Имя» должно быть обязательно заполненным!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Ошибка, поле «Фамилия» должно быть обязательно заполненным!")]
        public string Surname { get; set; }
        public string Middle_name { get; set; }
        [Required(ErrorMessage = "Ошибка, поле «Емейл» должно быть обязательно заполненным!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }
        public string Phone { get; set; }
        [StringLength(50, MinimumLength = 0, ErrorMessage = "Длина строки должна быть меньше 100 символов")]
        public string Comments { get; set; }
        [Required(ErrorMessage = "Ошибка, поле «Пароль» должно быть обязательно заполненным!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Ошибка, поле «Повторный ввод пароля» должно быть обязательно заполненным!")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string Repeat_password { get; set; }
    }
}