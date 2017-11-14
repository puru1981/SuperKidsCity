using SuperKidCity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperKidCity.Controllers
{
    //[Authorize]
    public class SettingController : Controller
    {
        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Cam()
        {
            var model = new SettingViewModel();
            model.SettingTypeId = 1;
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult RFID()
        {
            var model = new SettingViewModel();
            model.SettingTypeId = 2;
            return View(model);
        }
    }
}