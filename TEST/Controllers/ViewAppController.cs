using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEST.Helpers;
using TEST.ViewModel;

namespace TEST.Controllers
{
    [Authorize(Roles = "Admin, Operator")]
    public class ViewAppController : Controller
    {
        private AppHelper _appHelper = new AppHelper();

        // GET: ViewApp
        public ActionResult Index(AppViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult ViewApps()
        {
            AppViewModel viewModel = new AppViewModel();

            if (ModelState.IsValid)
            {
                viewModel = _appHelper.ViewApps(viewModel);
            }

            return View("Index", viewModel);
        }
    }
}