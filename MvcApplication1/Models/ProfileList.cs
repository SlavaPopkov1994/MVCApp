using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class ProfileList
    {
        public ProfileList()
        {
            ProfilesList = new List<ProfileViewModel>();
        }

        public List<ProfileViewModel> ProfilesList { get; set; }
    }
}