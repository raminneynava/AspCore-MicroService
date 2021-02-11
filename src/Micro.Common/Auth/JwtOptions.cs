using System;
using System.Collections.Generic;
using System.Text;

namespace Micro.Common.Auth
{
   public class JwtOptions
    {
        public string Secretkey { get; set; }
        public int ExpireMinutes { get; set; }
        public string Issuer { get; set; }   
    }
}
