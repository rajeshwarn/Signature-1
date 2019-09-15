using Signature.Core.AppleDeveloperManager.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Signature.Controllers
{
    public class IosController : Controller
    {
        // GET: Ios
        public ActionResult Index()
        {

			return View();
        }

        //public ActionResult UDID()
        //{

        //}

        public ActionResult GetMobileConfig()
        {

            return View();
        }

    }
}