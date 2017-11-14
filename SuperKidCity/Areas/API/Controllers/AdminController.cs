using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperKidCity.Areas.API.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: API/Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}