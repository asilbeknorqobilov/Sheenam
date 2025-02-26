using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace Sheenam.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    // GET
    [HttpGet]
    public ActionResult<string> Get() =>
        Ok("Hello Mario");
}