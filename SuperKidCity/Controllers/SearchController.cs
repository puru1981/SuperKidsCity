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
    public class SearchController : BaseController
    {
        private string connStr;
        public SearchController()
        {
            this.connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        [ChildActionOnly]
        // GET: Search
        public ActionResult Index()
        {
            var model = new SearchViewModel();
            using (var manager = new ManagerBase<StateDTO>(this.connStr))
            {
                model.States = manager.Context.Get().Where(s => !s.Deleted && s.Active).Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();
            }
            return View(model);
        }

        public ActionResult Advance()
        {
            var model = new SearchViewModel();
            model.Facilities = new List<FacilityGroupViewModel>();
            using (var manager = new ManagerBase<StateDTO>(this.connStr))
            {
                model.States = manager.Context.Get().Where(s => !s.Deleted && s.Active).Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();
            }

            using (var manager = new ManagerBase<FacilityGroupDTO>(connStr))
            {
                model.Facilities = manager.Context.Get().Select(g => new FacilityGroupViewModel()
                {
                    GroupId = g.Id,
                    GroupName = g.GroupName

                }).ToList();


            }
            foreach (var group in model.Facilities.ToList())
            {
                using (var manager = new ManagerBase<FacilityGroupMemberDTO>(connStr))
                {
                    group.Members = manager.Context.Get().Select(gm => new FacilityGroupMemberViewModel()
                    {
                        GUID = gm.GUID,
                        Name = gm.Name,
                        Required = gm.Required,
                        Type = gm.Type,
                        ValueType = gm.ValueType
                    }).ToList();
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult GetSchool()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult GetSchool(SearchViewModel searchmodel)
        {
            var model = new List<SchoolViewModel>();
            var list = new List<SchoolViewModel>();
            string msg = string.Empty;
            bool Success = true;
            int schoolId = 0;
            int.TryParse(searchmodel.SchoolId, out schoolId);
            int locationId = 0;
            int.TryParse(searchmodel.LocalityId, out locationId);
            SchoolViewModel result = null;
            try
            {
                if (schoolId > 0)
                {
                    result = GetSchoolInfo(model, schoolId, result);
                }
                else
                {
                    result = GetAddressFromLocationId(model, locationId, result);
                }

                msg = string.Format("Fetched {0} result(s).", model.Count);
            }
            catch(Exception ex)
            {
                Success = false;
                msg = CommonHelper.GetErrorMessage(ex, ref msg);
            }
            ViewBag.Success = Success;
            ViewBag.Msg = msg;
            return View(model);
        }

        private SchoolViewModel GetSchoolInfo(List<SchoolViewModel> model, int schoolId, SchoolViewModel result)
        {
            int tempId = 0;
            using (var manager = new ManagerBase<SchoolDTO>(connStr))
            {
                var school = manager.Context.GetSchoolBySchoolId(schoolId);
                if (school != null)
                {
                    result = new SchoolViewModel();
                    result.Name = school.Name;
                    result.EstablishedAt = school.EstablishedAt;
                    result.ShortBio = school.ShortBio;
                    result.Url = school.WebUrl;
                    result.RegistrationNumber = school.RegistrationNumber;
                    result.Id = school.Id;
                    result.CreatedAt = school.CreatedAt;
                    result.UpdatedAt = school.UpdatedAt;
                    result.Active = school.Active;
                    result.Id = school.Id;
                    tempId = GetAddressInfo(tempId, result);

                    GetLocality(tempId, result);
                    tempId = GetCityId(tempId, result);

                    tempId = GetStateId(tempId, result);

                    GetMemberInfo(result);
                    GetContactInfo(result);
                    model.Add(result);
                }
            }
            return result;
        }

        private SchoolViewModel GetAddressFromLocationId(List<SchoolViewModel> model, int locationId, SchoolViewModel result)
        {
            int tempId = 0;
            using (var manager = new ManagerBase<AddressDTO>(connStr))
            {
                var schoolLocation = manager.Context.GetSchoolsByLocationId(locationId);
                if (schoolLocation != null && schoolLocation.Count > 0)
                {
                    foreach (var school in schoolLocation)
                    {
                        result = new SchoolViewModel();
                        result.Name = school.School.Name;
                        result.EstablishedAt = school.School.EstablishedAt;
                        result.ShortBio = school.School.ShortBio;
                        result.Url = school.School.WebUrl;
                        result.RegistrationNumber = school.School.RegistrationNumber;
                        result.Id = school.Id;
                        result.CreatedAt = school.CreatedAt;
                        result.UpdatedAt = school.UpdatedAt;
                        result.Active = school.Active;
                        result.Id = school.Id;
                        tempId = GetAddressInfo(tempId, result);

                        GetLocality(tempId, result);
                        tempId = GetCityId(tempId, result);

                        tempId = GetStateId(tempId, result);

                        GetMemberInfo(result);
                        GetContactInfo(result);

                        model.Add(result);
                    }

                }
            }
            return result;
        }

        private int GetAddressInfo(int tempId, SchoolViewModel result)
        {
            using (var manager = new ManagerBase<AddressDTO>(connStr))
            {

                var schoolAddress = result != null ? manager.Context.GetSchoolAddressBySchoolId(result.Id) : null;
                if (schoolAddress != null)
                {
                    result.Address.Id = schoolAddress.Id;
                    result.Address.Active = schoolAddress.Active;
                    result.Address.CreatedAt = schoolAddress.CreatedAt;
                    result.Address.StreetAddress = schoolAddress.AddressLine1;
                    result.Address.StreeetAddress2 = schoolAddress.AddressLine2;
                    result.Address.UpdatedAt = schoolAddress.UpdatedAt;
                    tempId = schoolAddress.Locality_Id;
                }
            }
            return tempId;
        }

        private void GetLocality(int tempId, SchoolViewModel result)
        {
            using (var manager = new ManagerBase<LocalityDTO>(connStr))
            {
                var locality = manager.Context.GetById(tempId);
                if (locality != null)
                {
                    result.Address.Locality = string.Format("{0} [ {1} ]- {2}", locality.Name, locality.District, locality.Code);
                    result.Address.District = locality.CityId.ToString();
                }
            }
        }

        private int GetCityId(int tempId, SchoolViewModel result)
        {

            using (var manager = new ManagerBase<CityDTO>(connStr))
            {
                int.TryParse(result.Address.District, out tempId);
                var city = manager.Context.GetById(tempId);
                if (city != null)
                {
                    result.Address.District = city.Name;
                    result.Address.State = city.StateId.ToString();
                }
            }
            return tempId;
        }

        private int GetStateId(int tempId, SchoolViewModel result)
        {
            using (var manager = new ManagerBase<StateDTO>(connStr))
            {
                int.TryParse(result.Address.State, out tempId);
                var state = manager.Context.GetById(tempId);
                if (state != null)
                {
                    result.Address.State = state.Name;
                }
            }
            return tempId;
        }

        private void GetMemberInfo(SchoolViewModel result)
        {
            using (var manager = new ManagerBase<MemberDTO>(connStr))
            {
                var members = result != null ? manager.Context.GetSchoolMembersBySchoolId(result.Id) : new List<MemberDTO>();
                if (members.Count > 0)
                {
                    result.ContactPerson.Active = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().Active;
                    result.ContactPerson.CreatedAt = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().CreatedAt;
                    result.ContactPerson.UpdatedAt = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().UpdatedAt;
                    result.ContactPerson.Type = SKC.Lib.Core.Models.MemberType.ContactPerson;
                    result.ContactPerson.Name = string.Format("{0} {1}", members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().FirtsName, members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().LastName);
                    result.ContactPerson.FirstName = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().FirtsName;
                    result.ContactPerson.LastName = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().LastName;
                    result.ContactPerson.Gender = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().Gender;
                    result.ContactPerson.Id = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().Id;

                    result.Principal.Active = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().Active;
                    result.Principal.CreatedAt = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().CreatedAt;
                    result.Principal.UpdatedAt = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().UpdatedAt;
                    result.Principal.Type = SKC.Lib.Core.Models.MemberType.Principal;
                    result.Principal.Name = string.Format("{0} {1}", members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().FirtsName, members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().LastName);
                    result.Principal.FirstName = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().FirtsName;
                    result.Principal.LastName = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().LastName;
                    result.Principal.Gender = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().Gender;
                    result.Principal.Id = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().Id;
                }
            }
        }

        private void GetContactInfo(SchoolViewModel result)
        {
            using (var manager = new ManagerBase<ContactDTO>(connStr))
            {
                var contactInfo = result != null ? manager.Context.GetSchoolMembersContactBySchoolId(result.Id) : new List<ContactDTO>();
                if (contactInfo.Count > 0)
                {
                    result.ContactPerson.Contact.Active = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().Active;
                    result.ContactPerson.Contact.CreatedAt = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().CreatedAt;
                    result.ContactPerson.Contact.Email = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().Email;
                    result.ContactPerson.Contact.Id = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().Id;
                    result.ContactPerson.Contact.Number = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().Number;
                    result.ContactPerson.Contact.UpdatedAt = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().UpdatedAt;

                    result.Principal.Contact.Active = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().Active;
                    result.Principal.Contact.CreatedAt = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().CreatedAt;
                    result.Principal.Contact.Email = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().Email;
                    result.Principal.Contact.Id = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().Id;
                    result.Principal.Contact.Number = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().Number;
                    result.Principal.Contact.UpdatedAt = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().UpdatedAt;
                }
            }
        }
    }
}