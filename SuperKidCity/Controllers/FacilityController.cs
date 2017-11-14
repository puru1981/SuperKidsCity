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
    public class FacilityController : BaseController
    {
        private string connStr;

        public FacilityController()
        {
            this.connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        // GET: Facility
        public ActionResult Index()
        {
            var model = new List<FacilityGroupViewModel>();
            bool Success = true;
            string msg = string.Empty;
            try
            {
                using (var manager = new ManagerBase<FacilityGroupDTO>(connStr))
                {
                    model = manager.Context.Get().Select(g => new FacilityGroupViewModel()
                    {
                        GroupId = g.Id,
                        GroupName = g.GroupName,
                        Active = g.Active,
                        CreatedAt = g.CreatedAt,
                        UpdatedAt = g.UpdatedAt
                    }).ToList();
                }
                //foreach (var group in model.ToList())
                //{
                //    using (var manager = new ManagerBase<FacilityGroupMemberDTO>(connStr))
                //    {
                //        group.Members = manager.Context.Get().Select(gm => new FacilityGroupMemberViewModel()
                //       {
                //           GUID = gm.GUID,
                //           Name = gm.Name,
                //           Required = gm.Required,
                //           Type = gm.Type,
                //           ValueType = gm.ValueType
                //       }).ToList();
                //    }
                //}
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

        [Authorize(Roles = "School")]
        public ActionResult Configure()
        {
            var model = new List<FacilityGroupViewModel>();
            bool Success = true;
            string msg = string.Empty;
            int schoolId = 0;
            try
            {
                using (var manager = new ManagerBase<SchoolDTO>(connStr))
                {
                    schoolId = manager.Context.GetSchoolByUserId(this.CurrentUser.UserId).Id;
                }

                using (var manager = new ManagerBase<FacilityGroupDTO>(connStr))
                {

                    model = manager.Context.Get().Select(g => new FacilityGroupViewModel()
                    {
                        Id = g.Id,
                        GroupId = g.Id,
                        GroupName = g.GroupName,
                        Active = g.Active,
                        CreatedAt = g.CreatedAt,
                        UpdatedAt = g.UpdatedAt,
                    }).ToList();
                }
                foreach (var group in model.ToList())
                {
                    using (var manager = new ManagerBase<FacilityGroupMemberDTO>(connStr))
                    {
                        group.Members = manager.Context.GetFacilityGroupMemberByFacilityGroupId(group.Id).Select(gm =>
                        new FacilityGroupMemberViewModel()
                       {
                           GUID = gm.GUID,
                           Name = gm.Name,
                           Required = gm.Required,
                           Type = gm.Type,
                           ValueType = gm.ValueType,
                           GroupId = group.GroupId,
                           Id = gm.Id,
                           SchoolId = schoolId
                       }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Success = false;
                CommonHelper.GetErrorMessage(ex, ref msg);
            }
            ViewBag.SchoolId = schoolId;
            ViewBag.Success = Success;
            ViewBag.Msg = msg;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "School")]
        public ActionResult Configure(IEnumerable<FacilityGroupMemberViewModel> members)
        {
            return Content("Data with the count:" + members.ToList().Count + " received");
        }
    }
}