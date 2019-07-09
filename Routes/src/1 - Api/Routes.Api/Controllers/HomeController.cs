using System.Web.Mvc;

namespace Routes.Api.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return RedirectPermanent("~/sandbox/index");
        }
    }
}