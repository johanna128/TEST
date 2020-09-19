using System.Web.Mvc;

namespace TEST.Controllers
{
    [Authorize(Roles = "Admin, Operator")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }        
    }
}