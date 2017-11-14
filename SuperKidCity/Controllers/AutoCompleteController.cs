using SuperKidCity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperKidCity.Controllers
{
    public class AutoCompleteController : Controller
    {
        [Obsolete]
        // GET: AutoComplete
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Schools(string prefix)
        {
            var result = new JsonResult();
            var schools = new List<SchoolInfoLite>();
            using (var context = new ApplicationDbContext())
            {
                schools = context.Schools.Where(s => s.Active).Select(s => new SchoolInfoLite
                {
                    Id = s.Id,
                    Name = s.Name
                    //address = string.Format("{0},{1}", context.Addresses.Where(a => !a.Deleted && a.Active).Select(a => new
                    //{
                    //    streetAddress1 = a.AddressLine1,
                    //    streetAddress2 = a.AddressLine2
                    //}))
                }).ToList();
            }

            result.ContentType = "application/json";
            result.Data = schools;
            return result;
        }
    }
}