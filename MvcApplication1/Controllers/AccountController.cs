using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Interfaces;
using BusinessLogic.Providers;
using MvcApplication1.Helpers;
using System.Globalization;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MvcApplication1.Controllers
{
    public class AccountController : Controller
    {
        private IUsersProvider _usersProvider = new UsersProvider();

        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            //_usersProvider.LoginUser(model.Login, model.Password);

            var user = _usersProvider.LoginUser(model.Login, model.Password);
            if (user == null)
            {
                model.UserNotExist = true;
            }
            else
            {
                UserContext.SetUser(user);
                return RedirectToAction("Profile");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult NewLogin()
        {
            return View(new NewLoginViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> NewLogin(NewLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string photoURL = "";
                if (model.Upload != null)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(model.Upload.FileName);
                    photoURL = "/Content/UserImages/" + fileName;
                    // сохраняем файл в папку Files в проекте
                    model.Upload.SaveAs(Server.MapPath(photoURL));
                }
                var user = _usersProvider.RegisterUser(model.Surname, model.Name, model.Middle_name, model.Email, model.Phone, model.Comments, photoURL, model.Password);
                UserContext.SetUser(user);

          //       // наш email с заголовком письма
          //       MailAddress from = new MailAddress("slava.popkov.1994@gmail.com", "Web Registration");
          //       // кому отправляем
          //       MailAddress to = new MailAddress(model.Email);
          //       // создаем объект сообщения
          //       MailMessage m = new MailMessage(from, to);
          //       // тема письма
          //       m.Subject = "Email confirmation";
          //       // текст письма - включаем в него ссылку
          //       m.Body = string.Format("Для завершения регистрации перейдите по ссылке:" +
          //                       "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>",
          //           Url.Action("ConfirmEmail", "Account", new {  Email = model.Email }, Request.Url.Scheme));
          //       m.IsBodyHtml = true;
          //       // адрес smtp-сервера, с которого мы и будем отправлять письмо
          //       SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 25);
          //       // логин и пароль
          //       smtp.Credentials = new System.Net.NetworkCredential("slava.popkov.1994@gmail.com", "ckfdbr16");
          //       smtp.Send(m);
          //       return RedirectToAction("Confirm", "Account", new { Email = model.Email });
                return RedirectToAction("Profile");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Profile()
        {
            var User = UserContext.GetCurrentUser();
            if (User != null)
            {
                var model = new ProfileViewModel
                {
                    Surname = User.Surname,
                    Name = User.Name,
                    Middle_name = User.MiddleName,
                    Email = User.Email,
                    Comments = User.Comments,
                    Phone = User.Phone,
                    Password = User.Password,
                    Repeat_password = User.Password,
                    Upload = User.PhotoURL
                };
                return View(model);
            }
            else { return View();  }
            
        }

        [HttpPost]
        public ActionResult Profile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                string photoURL = model.Upload;
                if (model.Image != null)
                {
                    // получаем имя файла
                    string fileName = System.IO.Path.GetFileName(model.Image.FileName);
                    photoURL = "/Content/UserImages/" + fileName;
                    // сохраняем файл в папку Files в проекте
                    model.Image.SaveAs(Server.MapPath(photoURL));
                }
                var user = _usersProvider.EditingProfile(model.Surname, model.Name, model.Middle_name, model.Phone, model.Email, model.Comments, model.Password, photoURL);
                UserContext.SetUser(user);

            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditingOfAllProfiles(string surname, string name, string middle_name, string email, string comments, string phone, string password, string photo)
        {

            var model = new ProfileViewModel
            {
                Surname = surname,
                Name = name,
                Middle_name = middle_name,
                Email = email,
                Comments = comments,
                Phone = phone,
                Password = password,
                Repeat_password = password,
                Upload = photo
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult AllProfiles()
        {
            var model = new ProfileList();
            var profiles = _usersProvider.GetAllProfiles();
            foreach (var profile in profiles)
            {
                var profileViewModel = new ProfileViewModel
                {
                    Surname = profile.Surname,
                    Name = profile.Name,
                    Middle_name = profile.MiddleName,
                    Email = profile.Email,
                    Comments = profile.Comments,
                    Phone = profile.Phone,
                    Password = profile.Password,
                    Upload = profile.PhotoURL
                };
                model.ProfilesList.Add(profileViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AllProfiles(ProfileList model)
        {
            var profiles = _usersProvider.GetAllProfilesOderBy();
            foreach (var profile in profiles)
            {
                var profileViewModel = new ProfileViewModel
                {
                    Surname = profile.Surname,
                    Name = profile.Name,
                    Middle_name = profile.MiddleName,
                    Email = profile.Email,
                    Comments = profile.Comments,
                    Phone = profile.Phone,
                    Password = profile.Password,
                    Upload = profile.PhotoURL
                };
                model.ProfilesList.Add(profileViewModel);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            UserContext.SetUser(null);
            return RedirectToAction("Index", "Information");
        }
    }
}
