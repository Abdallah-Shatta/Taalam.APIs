using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 //   [AllowAnonymous]
    public class APIBaseController : ControllerBase
    {
    }
}
