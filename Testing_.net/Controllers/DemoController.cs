using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Testing_.net.MyLogging;

namespace Testing_.net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IMyLogger _myLogger;

        public DemoController(IMyLogger myLogger) 
        { 
            _myLogger = myLogger; 
        }

        [HttpGet]
        public ActionResult Index()
        {
            _myLogger.Log("This is a log message from DemoController.");
            return Ok("Log message has been logged.");
        }
    }
}
