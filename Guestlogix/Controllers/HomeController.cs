using Guestlogix.resources;
using System.Web.Mvc;

namespace Guestlogix.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = Resource.STR_GET_SHORTEST_ROUTE_TITLE;
            ViewBag.BtnLabel = Resource.STR_GET_SHORTEST_ROUTE_LABEL;
            return View();
        }
    }
}
