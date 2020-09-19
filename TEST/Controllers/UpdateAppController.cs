using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TEST.Helpers;
using TEST.ViewModel;

namespace TEST.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UpdateAppController : Controller
    {
        private AppHelper _appHelper = new AppHelper();

        // GET: UpdateApp
        public ActionResult Index(AppViewModel viewModel)
        {
            viewModel = _appHelper.ViewApps(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UpdateApp(string id, string webAddress)
        {
            var viewModel = new AppViewModel();
            AppData appData = new AppData();

            if (ModelState.IsValid)
            {
                appData.name = Request.Form[id];
                appData.chrome_web_origin = webAddress;
                appData.id = id;

                _appHelper.UpdateApp(appData);
                viewModel = _appHelper.ViewApps(viewModel);
            }

            return View("Index", viewModel);
        }
    }
}