using Signature.Core.AppleDeveloperManager.Impl;
using Signature.Core.SignatureManager.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Signature.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

			// var apple = new AppleDeveloperManager();
			// apple.GetDevices();
			var signature = new SignatureManager();
			signature.GetMobileConfig();

			return View();
        }
    }
}
