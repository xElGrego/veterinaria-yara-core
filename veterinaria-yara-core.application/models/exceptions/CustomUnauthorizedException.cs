namespace veterinaria_yara_core.application.models.exceptions
{
    public class CustomUnauthorizedException : BaseCustomException
    {
        public CustomUnauthorizedException(string message = "Unauthorized", string desciption = "", int statuscode = 401) : base(message, desciption, statuscode)
        {

        }
    }
}
