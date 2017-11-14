using SKC.Lib.Data.Models.Entities;
using SKC.Lib.Service.Managers;
using SuperKidCity.Helpers;
using SuperKidCity.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperKidCity.Controllers
{
    [Authorize]
    [Route("School")]
    public class SchoolController : BaseController
    {
        private string connStr;

        public SchoolController()
        {
            this.connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        // GET: SchoolInfo
        public ActionResult Index()
        {
            var model = new SchoolViewModel();
            string msg = string.Empty;
            bool Success = true;
            int tempId = 0;
            try
            {
                using (var manager = new ManagerBase<SchoolDTO>(connStr))
                {
                    var school = manager.Context.GetSchoolByUserId(this.CurrentUser.UserId);
                    if (school != null)
                    {
                        model.Name = school.Name;
                        model.EstablishedAt = school.EstablishedAt;
                        model.ShortBio = school.ShortBio;
                        model.Url = school.WebUrl;
                        model.RegistrationNumber = school.RegistrationNumber;
                        model.Id = school.Id;
                        model.CreatedAt = school.CreatedAt;
                        model.UpdatedAt = school.UpdatedAt;
                        model.Active = school.Active;
                    }
                }
                using (var manager = new ManagerBase<AddressDTO>(connStr))
                {
                    var schoolAddress = manager.Context.GetSchoolAddressBySchoolId(model.Id);
                    if (schoolAddress != null)
                    {
                        model.Address.Id = schoolAddress.Id;
                        model.Address.Active = schoolAddress.Active;
                        model.Address.CreatedAt = schoolAddress.CreatedAt;
                        model.Address.StreetAddress = schoolAddress.AddressLine1;
                        model.Address.StreeetAddress2 = schoolAddress.AddressLine2;
                        model.Address.UpdatedAt = schoolAddress.UpdatedAt;
                        tempId = schoolAddress.Locality_Id;
                    }
                }

                using (var manager = new ManagerBase<LocalityDTO>(connStr))
                {
                    var locality = manager.Context.GetById(tempId);
                    if (locality != null)
                    {
                        model.Address.Locality = string.Format("{0} [ {1} ]- {2}", locality.Name, locality.District, locality.Code);
                        model.Address.District = locality.CityId.ToString();
                    }
                }

                using (var manager = new ManagerBase<CityDTO>(connStr))
                {
                    int.TryParse(model.Address.District, out tempId);
                    var city = manager.Context.GetById(tempId);
                    if (city != null)
                    {
                        model.Address.District = city.Name;
                        model.Address.State = city.StateId.ToString();
                    }
                }
                using (var manager = new ManagerBase<StateDTO>(connStr))
                {
                    int.TryParse(model.Address.State, out tempId);
                    var state = manager.Context.GetById(tempId);
                    if (state != null)
                    {
                        model.Address.State = state.Name;
                    }
                }

                using (var manager = new ManagerBase<MemberDTO>(connStr))
                {
                    var members = manager.Context.GetSchoolMembersBySchoolId(model.Id);
                    if (members.Count > 0)
                    {
                        model.ContactPerson.Active = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().Active;
                        model.ContactPerson.CreatedAt = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().CreatedAt;
                        model.ContactPerson.UpdatedAt = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().UpdatedAt;
                        model.ContactPerson.Type = SKC.Lib.Core.Models.MemberType.ContactPerson;
                        model.ContactPerson.Name = string.Format("{0} {1}", members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().FirtsName, members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().LastName);
                        model.ContactPerson.FirstName = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().FirtsName;
                        model.ContactPerson.LastName = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().LastName;
                        model.ContactPerson.Gender = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().Gender;
                        model.ContactPerson.Id = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().Id;

                        model.Principal.Active = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().Active;
                        model.Principal.CreatedAt = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().CreatedAt;
                        model.Principal.UpdatedAt = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().UpdatedAt;
                        model.Principal.Type = SKC.Lib.Core.Models.MemberType.Principal;
                        model.Principal.Name = string.Format("{0} {1}", members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().FirtsName, members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().LastName);
                        model.Principal.FirstName = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().FirtsName;
                        model.Principal.LastName = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().LastName;
                        model.Principal.Gender = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().Gender;
                        model.Principal.Id = members.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().Id;
                    }
                }
                using (var manager = new ManagerBase<ContactDTO>(connStr))
                {
                    var contactInfo = manager.Context.GetSchoolMembersContactBySchoolId(model.Id);
                    if (contactInfo.Count > 0)
                    {
                        model.ContactPerson.Contact.Active = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().Active;
                        model.ContactPerson.Contact.CreatedAt = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().CreatedAt;
                        model.ContactPerson.Contact.Email = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().Email;
                        model.ContactPerson.Contact.Id = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().Id;
                        model.ContactPerson.Contact.Number = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().Number;
                        model.ContactPerson.Contact.UpdatedAt = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.ContactPerson).FirstOrDefault().UpdatedAt;

                        model.Principal.Contact.Active = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().Active;
                        model.Principal.Contact.CreatedAt = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().CreatedAt;
                        model.Principal.Contact.Email = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().Email;
                        model.Principal.Contact.Id = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().Id;
                        model.Principal.Contact.Number = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().Number;
                        model.Principal.Contact.UpdatedAt = contactInfo.Where(m => m.Type == SKC.Lib.Core.Models.MemberType.Principal).FirstOrDefault().UpdatedAt;
                    }
                }
            }


            catch (Exception ex)
            {
                Success = false;
                CommonHelper.GetErrorMessage(ex, ref msg);


            }
            ViewBag.Msg = msg;
            ViewBag.Success = Success;
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Config()
        {
            var model = new List<FacilityGroupViewModel>();
            using (var context = new ApplicationDbContext())
            {
                model = context.FacilityGroups.Select(fg => new FacilityGroupViewModel()
                {
                    GroupId = fg.Id,
                    GroupName = fg.GroupName,
                    Members = fg.Members.Select(fgm => new FacilityGroupMemberViewModel()
                    {
                        GUID = fgm.GUID,
                        Name = fgm.Name,
                        Required = fgm.Required,
                        Type = fgm.Type,
                        ValueType = fgm.ValueType
                    }).ToList()

                }).ToList();
            }
            return View(model);
        }
    }
}