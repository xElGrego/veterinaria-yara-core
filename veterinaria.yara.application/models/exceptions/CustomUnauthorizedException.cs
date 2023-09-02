using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veterinaria.yara.application.models.exceptions
{
    public class CustomUnauthorizedException : BaseCustomException
    {
        public CustomUnauthorizedException(string message = "Unauthorized", string desciption = "", int statuscode = 401) : base(message, desciption, statuscode)
        {

        }

    }
}
