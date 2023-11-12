
namespace veterinaria_yara_core.application.models.exceptions
{
    public class VeterinariaYaraException : BaseCustomException
    {
        public VeterinariaYaraException(string message = "Exception", string desciption = "", int statuscode = 500) : base(message, desciption, statuscode)
        {

        }
    }
}
