using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherRoutingBackend.Model.Request
{
    public class AuthoriseRequest
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
