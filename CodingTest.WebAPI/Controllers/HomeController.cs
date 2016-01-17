using System.Web.Mvc;

namespace CodingTest.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "The Gilded Rose Online Store";

            return View();
        }
    }
}