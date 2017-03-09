using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entyties;
using Database;


namespace BusinessLogic.Providers
{
    public class UsersProvider : IUsersProvider
    {
        private ChampionshipDatabaseContext _context = new ChampionshipDatabaseContext();


        public User RegisterUser(string surname, string name, string middleName, string email, string phone, string comments, string photoURL, string password)
        {
            var user = new User
            {
                Surname = surname,
                Name = name,
                MiddleName = middleName,
                Email = email,
                Phone = phone,
                Comments = comments,
                PhotoURL = photoURL,
                Password = password
            };

            _context.Users.InsertOnSubmit(user);
            _context.SubmitChanges();
            return user;
        }
        public User LoginUser(string login, string password)
        {
            var user = _context.Users.FirstOrDefault(u => string.Equals(u.Email, login));
            if (user != null && string.Equals(user.Password, password))
            {
                return user;
            }
            return null;
        }

        public User EditingProfile(string surname, string name, string middleName, string phone, string email, string comments, string password, string photoURL)
        {
            var user = _context.Users.FirstOrDefault(u => int.Equals(u.Email, email));
            user.Surname = surname;
            user.Name = name;
            user.MiddleName = middleName;
            user.Phone = phone;
            user.Comments = comments;
            user.Password = password;
            user.PhotoURL = photoURL;
            _context.SubmitChanges();
            return user;

        }

         public IEnumerable<User> GetAllProfiles()
        {
            return _context.Users.OrderBy(t => t.Email);
        }

         public IEnumerable<User> GetAllProfilesOderBy()
         {
             return _context.Users.OrderBy(t => t.Surname);
         }

    }
}
