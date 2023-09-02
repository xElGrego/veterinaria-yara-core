using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veterinaria.yara.application.models.exceptions
{
    public class VeterinariaYaraException : BaseCustomException
    {
        public VeterinariaYaraException(string message = "Exception", string desciption = "", int statuscode = 500) : base(message, desciption, statuscode)
        {

        }
    }
}
