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

    public class StateController : BaseController
    {
        // GET: State
        public ActionResult Index()
        {
            var model = new List<StateViewModel>();
            bool Success = true;
            string msg = string.Empty;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    if (context.States.ToList().Count == 0)
                    {
                        var states = SeedModel.PopulateStateList(this.HttpContext.Session.SessionID, this.CurrentUser.UserId);
                        context.States.AddRange(states);
                        context.SaveChanges();
                    }
                    model = context.States.Where(s => !s.Deleted && !string.IsNullOrEmpty(s.Name)).Select(s => new StateViewModel()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Cities = s.Cities.Select(c => new CityViewModel() { Code = c.Code, Name = c.Name }).ToList()
                    }).ToList();
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

        // GET: State/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: State/Create
        public ActionResult Create()
        {
            var model = new StateViewModel();
            ViewBag.Success = true;
            ViewBag.Msg = string.Empty;
            return View(model);
        }

        // POST: State/Create
        [HttpPost]
        public ActionResult Create(IEnumerable<StateViewModel> states)
        {
            var stateList = new List<StateDTO>();
            bool Success = true;
            string msg = "States Saved Successfully";
            try
            {
                foreach (var state in states.ToList())
                {
                    if (!string.IsNullOrEmpty(state.Name))
                    {
                        if (!CommonHelper.StateExist(state.Name))
                        {
                            stateList.Add(new StateDTO()
                            {
                                Active = true,
                                Code = state.Code,
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                Name = state.Name,
                                SessionId = this.Request.RequestContext.HttpContext.Session.SessionID,
                                UserId = this.CurrentUser.UserId
                            });
                        }
                        else
                        {
                            if (!states.ToList().Contains(state))
                            {
                                states.ToList().Add(state);
                            }
                            Success = false;
                            msg = msg + string.Format("State with name {0} exists. Duplicates not allowed", state.Name);
                        }
                    }
                }
                if (stateList.Count > 0 && Success)
                {
                    using (var context = new ApplicationDbContext())
                    {
                        context.States.AddRange(stateList);
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
                return RedirectToAction("Index");
            }
            ViewBag.Success = Success;
            ViewBag.Msg = msg;
            return View(states.FirstOrDefault());
        }

        // GET: State/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: State/Edit/5
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

        // GET: State/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: State/Delete/5
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
    }
}
