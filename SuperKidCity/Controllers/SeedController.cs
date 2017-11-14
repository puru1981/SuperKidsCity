using Microsoft.AspNet.Identity.EntityFramework;
using SKC.Lib.Core.Models;
using SKC.Lib.Data.Models.Entities;
using SuperKidCity.Helpers;
using SuperKidCity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperKidCity.Controllers
{
    //[Authorize]
    public class SeedController : BaseController
    {
        // GET: Seed
        [Obsolete]
        public ActionResult Index()
        {
            var msg = string.Empty;
            var states = new List<StateViewModel>();
            Seed();
            //{

            //    using (var context = new ApplicationDbContext())
            //    {
            //        states = context.States.Select(s => new StateViewModel()
            //        {
            //            Name = s.Name,
            //            Id = s.Id,
            //            Cities = s.Cities.Select(c =>
            //                new CityViewModel()
            //                {
            //                    Name = c.Name,
            //                    Code = c.Code
            //                }).ToList()
            //        }).ToList();

            //    }
            //    ViewBag.Success = true;
            //}
            //else
            //{
            //    ViewBag.Success = false;
            //}

            ViewBag.Msg = msg;
            return View(states);
        }


        public ActionResult SeedCities(int Id)
        {
            var model = new StateViewModel();
            bool Success = true;
            string msg = string.Empty;
            model.Id = Id;
            try
            {
                var cities = SeedModel.PopulateCityList(Guid.NewGuid().ToString(), "0");
                using (var context = new ApplicationDbContext())
                {
                    model.Name = context.States.Find(Id).Name;

                }

                model.Cities = cities.Where(c => c.StateId == Id).Select(c => new CityViewModel() { Code = c.Code, Name = c.Name }).ToList();
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

        //[Obsolete]
        [HttpPost]
        public ActionResult SeedCities(int Id, IEnumerable<CityViewModel> cities)
        {
            bool Success = true;
            string msg = string.Empty;
            var model = new StateViewModel();
            if (cities != null)
            {
                var citiList = new List<CityDTO>();
                try
                {
                    foreach (var city in cities)
                    {
                        citiList.Add(new CityDTO()
                        {
                            Name = city.Name,
                            Code = city.Code,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow,
                            Active = true,
                            SessionId = this.HttpContext.Session.SessionID,
                            UserId = this.CurrentUser.UserId,
                            StateId = Id
                        });
                    }


                    if (citiList.Count > 0)
                    {
                        model.Cities = (IList<CityViewModel>)citiList.ToList();
                        using (var context = new ApplicationDbContext())
                        {
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

                ViewBag.Success = Success;
                ViewBag.Msg = msg;
            }
            return View(cities);
        }



        private async Task<string> Seed()
        {
            bool success = true;

            string msg = "Data Seeded Successfully";
            try
            {
                string sessionId = this.HttpContext.Session.SessionID;

                using (var context = new ApplicationDbContext())
                {
                    if (context.Roles.ToList().Count == 0)
                    {
                        context.Roles.Add(new IdentityRole { Name = "Sa" });
                        context.Roles.Add(new IdentityRole { Name = "Admin" });
                        context.Roles.Add(new IdentityRole { Name = "School" });
                        context.Roles.Add(new IdentityRole { Name = "Parent" });
                    }

                    if (context.Users.ToList().Count == 0)
                    {
                        var store = new UserStore<ApplicationUser>(context);
                        var manager = new ApplicationUserManager(store);
                        var admin = new ApplicationUser() { Email = "admin@skc.com", UserName = "admin@skc.com" };
                        var sa = new ApplicationUser() { Email = "sa@skc.com", UserName = "sa@skc.com" };
                        await manager.CreateAsync(admin, "Admin@12345");
                        await manager.CreateAsync(sa, "Admin@12345");
                        await manager.AddToRoleAsync(admin.Id, "Admin");
                        await manager.AddToRoleAsync(sa.Id, "Sa");
                    }
                    if (context.FacilityGroups.ToList().Count == 0)
                    {
                        string userId = this.CurrentUser.UserId;
                        context.FacilityGroups.AddRange(
                            new List<FacilityGroupDTO>()
                            {
                            new FacilityGroupDTO
                            {
                                Id = 1,
                                Active = true,
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                SessionId = sessionId,
                                UserId = userId,
                                GroupName = "Common",
                                Members = new List<FacilityGroupMember>
                    {
                        new FacilityGroupMemberDTO
                {
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    GUID = Guid.NewGuid(),
                    Name = "Total Classrooms",
                    Id = 1,
                    Required = true,
                    SessionId = sessionId,
                    UpdatedAt = DateTime.UtcNow,
                    Type = SKC.Lib.Core.Models.FacilityGroupMemberType.Textbox,
                    UserId=userId,
                    ValueType = "integer",
                    
                },
                new FacilityGroupMemberDTO
                {
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    GUID = Guid.NewGuid(),
                    Name = "Capacity Per Class Room",
                    Id = 2,
                    Required = true,
                    SessionId = sessionId,
                    UpdatedAt = DateTime.UtcNow,
                    Type = SKC.Lib.Core.Models.FacilityGroupMemberType.Textbox,
                    UserId=userId,
                    ValueType = "integer",
                    
                },
                new FacilityGroupMemberDTO
                {
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    GUID = Guid.NewGuid(),
                    Name = "No Of Support Staff",
                    Id = 3,
                    Required = true,
                    SessionId = sessionId,
                    UpdatedAt = DateTime.UtcNow,
                    Type = SKC.Lib.Core.Models.FacilityGroupMemberType.Textbox,
                    UserId=userId,
                    ValueType = "integer",
                    
                },
                new FacilityGroupMemberDTO
                {
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    GUID = Guid.NewGuid(),
                    Name = "Support Staff Gender",
                    Id = 4,
                    Required = true,
                    SessionId = sessionId,
                    UpdatedAt = DateTime.UtcNow,
                    Type = SKC.Lib.Core.Models.FacilityGroupMemberType.Dropdown,
                    UserId=userId,
                    ValueType = "dropdown",
                    
                },
                new FacilityGroupMemberDTO
                {
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    GUID = Guid.NewGuid(),
                    Name = "School Transport",
                    Id = 5,
                    Required = true,
                    SessionId = sessionId,
                    UpdatedAt = DateTime.UtcNow,
                    Type = SKC.Lib.Core.Models.FacilityGroupMemberType.Checkbox,
                    UserId=userId,
                    ValueType = "bool",
                    
                },
                new FacilityGroupMemberDTO
                {
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    GUID = Guid.NewGuid(),
                    Name = "Food Preference Of Child",
                    Id = 6,
                    Required = true,
                    SessionId = sessionId,
                    UpdatedAt = DateTime.UtcNow,
                    Type = SKC.Lib.Core.Models.FacilityGroupMemberType.Checkbox,
                    UserId=userId,
                    ValueType = "bool",
                   
                },
                new FacilityGroupMemberDTO
                {
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    GUID = Guid.NewGuid(),
                    Name = "Complaint Book",
                    Id = 7,
                    Required = true,
                    SessionId = sessionId,
                    UpdatedAt = DateTime.UtcNow,
                    Type = SKC.Lib.Core.Models.FacilityGroupMemberType.Checkbox,
                    UserId=userId,
                    ValueType = "bool",
                    
                },
                new FacilityGroupMemberDTO
                {
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    GUID = Guid.NewGuid(),
                    Name = "Other",
                    Id = 1,
                    Required = true,
                    SessionId = sessionId,
                    UpdatedAt = DateTime.UtcNow,
                    Type = SKC.Lib.Core.Models.FacilityGroupMemberType.TextArea,
                    UserId=userId,
                    ValueType = "text",
                    
                }
                    }
                            },
                             new FacilityGroupDTO
                             {
                                 Id = 2,
                                 Active = true,
                                 CreatedAt = DateTime.UtcNow,
                                 UpdatedAt = DateTime.UtcNow,
                                 SessionId = sessionId,
                                 UserId = userId,
                                 GroupName = "Kitchen"
                             },
                              new FacilityGroupDTO
                              {
                                  Id = 3,
                                  Active = true,
                                  CreatedAt = DateTime.UtcNow,
                                  UpdatedAt = DateTime.UtcNow,
                                  SessionId = sessionId,
                                  UserId = userId,
                                  GroupName = "Entertainment"
                              },
                               new FacilityGroupDTO
                               {
                                   Id = 4,
                                   Active = true,
                                   CreatedAt = DateTime.UtcNow,
                                   UpdatedAt = DateTime.UtcNow,
                                   SessionId = sessionId,
                                   UserId = userId,
                                   GroupName = "Sanitation"
                               },
                               new FacilityGroupDTO
                               {
                                   Id = 5,
                                   Active = true,
                                   CreatedAt = DateTime.UtcNow,
                                   UpdatedAt = DateTime.UtcNow,
                                   SessionId = sessionId,
                                   UserId = userId,
                                   GroupName = "Health"
                               },
                               new FacilityGroupDTO
                               {
                                   Id = 6,
                                   Active = true,
                                   CreatedAt = DateTime.UtcNow,
                                   UpdatedAt = DateTime.UtcNow,
                                   SessionId = sessionId,
                                   UserId = userId,
                                   GroupName = "Security"
                               }
                            }
                            );
                    }
                    if (context.States.ToList().Count == 0)
                    {
                        context.States.Add(
                            new StateDTO
                            {
                                Active = true,
                                Code = "UP",
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                Name = "Uttar Pradesh",
                                SessionId = sessionId,
                                UserId = this.CurrentUser.UserId
                            });
                        context.States.Add(
                            new StateDTO
                            {
                                Active = true,
                                Code = "HR",
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                Name = "Haryana",
                                SessionId = sessionId,
                                UserId = this.CurrentUser.UserId
                            });
                        context.States.Add(
                            new StateDTO
                            {
                                Active = true,
                                Code = "DL",
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                Name = "Delhi",
                                SessionId = sessionId,
                                UserId = this.CurrentUser.UserId
                            });
                        context.States.Add(
                            new StateDTO
                            {
                                Active = true,
                                Code = "BR",
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                Name = "Bihar",
                                SessionId = sessionId,
                                UserId = this.CurrentUser.UserId
                            });
                        context.States.Add(
                            new StateDTO
                            {
                                Active = true,
                                Code = "PB",
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                Name = "Punjab",
                                SessionId = sessionId,
                                UserId = this.CurrentUser.UserId
                            });
                    }
                    context.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                success = false;
                msg = CommonHelper.GetErrorMessage(ex, ref msg);
            }

            return msg;
        }


    }

}