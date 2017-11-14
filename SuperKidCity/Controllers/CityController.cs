using SKC.Lib.Data.Models.Entities;
using SuperKidCity.Helpers;
using SuperKidCity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperKidCity.Controllers
{
    public class CityController : BaseController
    {
        // GET: City
        public ActionResult Index(int? Id)
        {
            var model = new List<CityViewModel>();
            bool success = true;
            string msg = string.Empty;
            string state = string.Empty;
            ViewBag.StateId = Id;
            try
            {
                if (Id.HasValue)
                {
                    ViewBag.State = CommonHelper.GetStateById(Id.Value).Name;
                    using (var context = new ApplicationDbContext())
                    {
                        model = context.Cities.Where(c =>
                            c.Active && c.StateId == Id).Select(c =>
                                new CityViewModel()
                                {
                                    Name = c.Name,
                                    Code = c.Code,
                                    Id = c.Id,
                                    Localities = c.Localities.Select(cl =>
                                            new LocalityViewModel()
                                            {
                                                City = c.Name,
                                                Name = cl.Name,
                                                Id = cl.Id,
                                                Code = cl.Code
                                            }).ToList()
                                }).ToList();
                    }
                }
                else
                {
                    ViewBag.State = "Including All";
                    using (var context = new ApplicationDbContext())
                    {
                        model = context.Cities.Where(c =>
                            c.Active).Select(c =>
                                new CityViewModel()
                                {
                                    Name = c.Name,
                                    Code = c.Code,
                                    Id = c.Id,
                                    State = CommonHelper.GetStateById(c.StateId).Name,
                                    Localities = c.Localities.Select(cl =>
                                            new LocalityViewModel()
                                            {
                                                City = c.Name,
                                                Name = cl.Name,
                                                Id = cl.Id,
                                                Code = cl.Code
                                            }).ToList()
                                }).ToList();
                    }
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

        public ActionResult Create(int Id)
        {
            var model = new StateViewModel();
            bool Success = true;
            string msg = string.Empty;
            model.Id = Id;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    model.Name = context.States.Find(Id).Name;
                }

                model.Cities = SeedModel.PopulateCityList(Guid.NewGuid().ToString(), "0").Where(c => c.StateId == Id).Select(c => new CityViewModel() { Code = c.Code, Name = c.Name }).ToList();
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

        [HttpPost]
        public ActionResult Create(int Id, IEnumerable<CityViewModel> cities)
        {
            bool Success = true;
            string msg = string.Empty;
            var model = new StateViewModel();
            model.Id = Id;
            if (cities != null)
            {
                var citiList = new List<CityDTO>();
                try
                {
                    var sesionid = this.Request.RequestContext.HttpContext.Session.SessionID;
                    model.Name = CommonHelper.GetStateById(Id).Name;
                    foreach (var city in cities)
                    {
                        if (!string.IsNullOrEmpty(city.Name))
                        {
                            if (!CommonHelper.CityExist(city.Name))
                            {
                                citiList.Add(new CityDTO()
                                {
                                    Name = city.Name,
                                    Code = city.Code,
                                    CreatedAt = DateTime.UtcNow,
                                    UpdatedAt = DateTime.UtcNow,
                                    Active = true,
                                    SessionId = sesionid,
                                    UserId = "0",
                                    StateId = Id
                                });
                            }
                            else
                            {
                                model.Cities.Add(city);
                                Success = false;
                                msg = msg + string.Format("City with name {0} exists. Duplicates not allowed.\n", city.Name);
                            }
                        }
                    }


                    if (citiList.Count > 0 && Success)
                    {
                        model.Cities = cities.ToList();
                        using (var context = new ApplicationDbContext())
                        {
                            context.Database.Log = s => System.Diagnostics.Debug.Write(s);
                            context.Cities.AddRange(citiList);
                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Success = false;
                    msg = CommonHelper.GetErrorMessage(ex, ref msg);
                }
                if (Success)
                {
                    return RedirectToAction("Index", new { Id = model.Id });
                }
                ViewBag.Success = Success;
                ViewBag.Msg = msg;
            }
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetByState(int stateId)
        {
            JsonResult res = new JsonResult();

            res.ContentType = "json";
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    res.Data = context.Cities.Where(c => !c.Deleted && c.Active && c.StateId == stateId).Select(c => new
                    {
                        name = c.Name,
                        value = c.Id
                    }).OrderBy(c => c.name).ToList();
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