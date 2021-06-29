using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Another.Api.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int TimeToLive { get; set; }
        public string Appplication { get; set; }
        public string ValidOn { get; set; }
    }
}
