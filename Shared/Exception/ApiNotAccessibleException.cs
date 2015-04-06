using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exception
{
    public class ApiNotAccessibleException : System.Exception
    {
        private string _apiUrl;

        public ApiNotAccessibleException(string ApiUrl)
        {
            _apiUrl = ApiUrl;
        }

        public override string Message
        {
            get
            {
                return String.Format("Api endpoint {0} is not accessible.", _apiUrl);
            }
        }
    }
}
