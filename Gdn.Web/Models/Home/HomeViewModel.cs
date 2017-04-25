using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gdn.Web.Models.Home
{
    public class HomeViewModel
    {
        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}