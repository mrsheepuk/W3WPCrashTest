using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Hosting;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace W3WPTest.Controllers
{
    [ResponseCache(NoStore = true)]
    public class HomeController : Controller
    {
        public HomeController(IHostingEnvironment hostingEnv)
        {
        }
        [Route("{*url}", Order = 999)]
        public IActionResult Index()
        {
            return View();
        }

        [Route("api/", Order = 998)]
        public IActionResult ApiDetails()
        {
            return new ObjectResult(new { Hello = "World" });
        }
    }
}
