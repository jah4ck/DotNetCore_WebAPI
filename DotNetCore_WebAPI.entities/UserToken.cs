using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore_WebAPI.entities
{
    public class UserToken
    {
        public string username { get; set; }
        public string password { get; set; }
        public string token { get; set; }
    }
}
