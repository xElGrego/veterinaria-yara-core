using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace veterinaria.yara.api.Controllers
{
    //[Authorize]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
