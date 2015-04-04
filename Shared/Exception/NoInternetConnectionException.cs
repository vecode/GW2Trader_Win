using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exception
{
    public class NoInternetConnectionException : System.Exception
    {
        public override string Message
        {
            get
            {
                return "No internet connection available.";
            }
        }
    }
}
