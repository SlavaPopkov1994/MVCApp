using Database.Entyties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Helpers
{
    public class UserContext
    {
        private const string UserSessionKey = "CurrentUser";

        public static bool IsUserAuthenticated
        {
            get
            {
                return GetCurrentUser() != null;
            }
        }

        public static void SetUser(User user)
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session[UserSessionKey] = user;
            }
        }
        public static User GetCurrentUser()
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session[UserSessionKey] != null)
            {
                return HttpContext.Current.Session[UserSessionKey] as User;
            }
            return null;
        }

        public static bool IsAdmin
        {
            get 
            {
                return IsUserAuthenticated && GetCurrentUser().IsAdmin;
            }
        }

    }
}