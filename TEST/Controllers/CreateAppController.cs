using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEST.Helpers;
using TEST.Models;
using TEST.ViewModel;

namespace TEST.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CreateAppController : Controller
    {
        private AppHelper _appHelper = new AppHelper();

        // GET: CreateApp
        public ActionResult Index(AppViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateApp()
        {
            var viewModel = new AppViewModel();
            AppData appData = new AppData();

            if (ModelState.IsValid)
            {
                appData.name = Request.Form["name"];
                appData.chrome_web_origin = Request.Form["chrome_web_origin"];

                viewModel = _appHelper.CreateApp(viewModel, appData);
            }
            return View("Index", viewModel);
        }
    }
}