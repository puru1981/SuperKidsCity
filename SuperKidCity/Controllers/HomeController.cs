using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperKidCity.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //if (this.User.Identity.IsAuthenticated)
            //{
            //    if (this.User.IsInRole("School"))
            //    {
            //        return RedirectToAction("Index", "School");
            //    }

            //    if (this.User.IsInRole("Parent"))
            //    {
            //        return RedirectToAction("Index", "Parent");
            //    }
            //}
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Mission(bool layout = false)
        {

            if (layout == true)
                ViewBag.Layout = "~/Views/Shared/_Layout.cshtml";
            else
                ViewBag.Layout = "";
            return View();
        }

        public ActionResult Vision(bool layout = false)
        {
            if (layout == true)
                ViewBag.Layout = "~/Views/Shared/_Layout.cshtml";
            else
                ViewBag.Layout = "";
            return View();
        }


        public ActionResult Profile(bool layout = false)
        {
            if (layout == true)
                ViewBag.Layout = "~/Views/Shared/_Layout.cshtml";
            else
                ViewBag.Layout = "";
            return View();
        }

        public ActionResult Parent(bool layout = false)
        {
            if (layout == true)
                ViewBag.Layout = "~/Views/Shared/_Layout.cshtml";
            else
                ViewBag.Layout = "";
            return View();
        }

        public ActionResult School(bool layout = false)
        {
            if (layout == true)
                ViewBag.Layout = "~/Views/Shared/_Layout.cshtml";
            else
                ViewBag.Layout = "";
            return View();
        }

        public ActionResult Product(bool layout = false)
        {
            if (layout == true)
                ViewBag.Layout = "~/Views/Shared/_Layout.cshtml";
            else
                ViewBag.Layout = "";
            return View();
        }
        public ActionResult FAQ(bool layout = false)
        {
            if (layout == true)
                ViewBag.Layout = "~/Views/Shared/_Layout.cshtml";
            else
                ViewBag.Layout = "";
            return View();
        }

        public ActionResult CommingSoon()
        {
            return View();
        }
    }
}