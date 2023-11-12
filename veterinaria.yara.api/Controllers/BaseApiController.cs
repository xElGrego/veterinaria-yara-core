using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace veterinaria_yara_core.api.Controllers
{
    //[Authorize]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
