using SKC.Lib.Data.Models.Entities;
using SKC.Lib.Service.Managers;
using SuperKidCity.Helpers;
using SuperKidCity.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperKidCity.Controllers
{
    public class LocalityController : BaseController
    {
        private string connStr;
        public LocalityController()
        {
            this.connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        // GET: Locality
        public ActionResult Index(int Id)
        {
            var model = new List<LocalityViewModel>();
            bool success = true;
            ViewBag.CityId = Id;
            string msg = string.Empty;
            try
            {
                ViewBag.City = CommonHelper.GetCityById(Id).Name;
                using (var context = new ApplicationDbContext())
                {
                    model = context.Localities.Where(l => !l.Deleted && l.CityId == Id).Select(l => new LocalityViewModel()
                    {
                        Code = l.Code,
                        Id = l.Id,
                        Name = l.Name,
                        District = l.District
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                success = false;
                CommonHelper.GetErrorMessage(ex, ref msg);
            }

            ViewBag.Success = success;
            ViewBag.Msg = msg;
            return View(model);
        }

        // GET: Locality/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Locality/Create/Id

        public ActionResult Create(int Id)
        {
            var model = new CityViewModel();
            bool Success = true;
            string msg = string.Empty;
            model.Id = Id;
            var sessionId = Guid.NewGuid().ToString();
            var userId = "0";
            var city = new CityDTO();
            int cityId = 0;
            try
            {

                using (var context = new ApplicationDbContext())
                {
                    city = context.Cities.Find(Id);
                    model.Name = city.Name;

                }
                if (city.Name.Equals("Delhi") || city.Name.Equals("DELHI"))
                {
                    cityId = 307;
                }
                if (city.Name.Equals("New Delhi") || city.Name.Equals("New DELHI"))
                {
                    cityId = 306;
                }
                model.Localities = SeedModel.PopulateLocalityList(sessionId, userId).Where(c => c.CityId == cityId).Select(l => new LocalityViewModel()
                {
                    City = model.Name,
                    Code = l.Code,
                    District = l.District,
                    Id = l.Id,
                    Name = l.Name,
                    CityId = l.CityId

                }).ToList();

                if (model.Localities.Any(l => l.CityId != Id) && city.Name.Equals("Delhi") || city.Name.Equals("DELHI"))
                {
                    foreach (var c in model.Localities.Where(ct => ct.CityId == 307))
                    {
                        c.CityId = Id;
                    }
                }
                if (model.Localities.Any(l => l.CityId != Id) && city.Name.Equals("New Delhi") || city.Name.Equals("New DELHI"))
                {
                    foreach (var c in model.Localities.Where(ct => ct.CityId == 306))
                    {
                        c.CityId = Id;
                    }
                }
            }
            catch (Exception ex)
            {
                Success = false;
                CommonHelper.GetErrorMessage(ex, ref msg);
            }
            ViewBag.Success = Success;
            ViewBag.Msg = msg;
            return View(model);
        }

        // POST: Locality/Create
        [HttpPost]
        public ActionResult Create(int Id, IEnumerable<LocalityViewModel> localities)
        {
            bool Success = true;
            string msg = string.Empty;
            var model = new CityViewModel();
            model.Id = Id;
            if (localities != null)
            {
                var localityList = new List<LocalityDTO>();
                try
                {
                    var sesionid = this.Request.RequestContext.HttpContext.Session.SessionID;
                    model.Name = CommonHelper.GetCityById(Id).Name;
                    foreach (var locality in localities)
                    {
                        if (!string.IsNullOrEmpty(locality.Name))
                        {
                            localityList.Add(new LocalityDTO()
                            {
                                Name = locality.Name,
                                Code = locality.Code,
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                Active = true,
                                SessionId = sesionid,
                                UserId = "0",
                                CityId = Id,
                                District = locality.District
                            });
                        }
                    }


                    if (localityList.Count > 0)
                    {
                        model.Localities = localities.ToList();
                        using (var context = new ApplicationDbContext())
                        {
                            context.Database.Log = s => System.Diagnostics.Debug.Write(s);
                            context.Localities.AddRange(localityList);
                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Success = false;
                    msg = CommonHelper.GetErrorMessage(ex, ref msg);
                }

                ViewBag.Success = Success;
                ViewBag.Msg = msg;
            }
            return View(model);
        }

        // GET: Locality/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Locality/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Locality/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Locality/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetByCity(int cityId)
        {
            JsonResult res = new JsonResult();

            res.ContentType = "json";
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    res.Data = context.Localities.Where(c => !c.Deleted && c.Active && c.CityId == cityId).Select(c => new
                    {
                        name = c.Name + " [ " + c.District + " ]",
                        value = c.Id
                    }).OrderBy(c=>c.name).ToList();
                }
            }
            catch (Exception ex)
            {
                res.Data = new { msg = ex.Message };
            }

            return res;
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetByState(int stateId)
        {
            JsonResult res = new JsonResult();

            res.ContentType = "json";
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                using (var manager = new ManagerBase<LocalityDTO>(this.connStr))
                {
                    res.Data = manager.Context.GetLocalityByStateId(stateId).Select(c => new
                    {
                        name = c.Name,
                        value = c.Id
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                res.Data = new { msg = ex.Message };
            }

            return res;
        }
    }
}
