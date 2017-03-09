using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entyties;

namespace BusinessLogic.Interfaces
{
    public interface IUsersProvider
    {
        User RegisterUser(string Surname, string Name, string Middle_name, string Email, string Phone, string Comments, string PhotoURL, string Password);
        User LoginUser(string Login, string Password);
        User EditingProfile(string Surname, string Name, string MiddleName, string Phone, string Email, string Comments, string Password, string PhotoURL);
        IEnumerable<User> GetAllProfiles();
        IEnumerable<User> GetAllProfilesOderBy();
    }
}
