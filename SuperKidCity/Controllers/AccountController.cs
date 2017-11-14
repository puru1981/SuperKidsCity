using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SuperKidCity.Models;
using System.Collections.Generic;
using SKC.Lib.Data.Models.Entities;
using SuperKidCity.Helpers;

namespace SuperKidCity.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    GetUserForClaims();
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterViewModel();
            model.School = new SchoolViewModel();
            using (var context = new ApplicationDbContext())
            {
                model.School.Address.States = context.States.Where(s => !s.Deleted && s.Active).Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).ToList();
                model.School.Address.State = "0";
                model.School.Address.States.Add(new SelectListItem() { Text = "--Select--", Value = "0" });
            }
            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            int stateId = 0;
            int cityId = 0;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                string msg = string.Empty;

                int.TryParse(model.School.Address.State, out stateId);
                int.TryParse(model.School.Address.District, out cityId);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    await SignInManager.UserManager.AddToRoleAsync(user.Id, "School");
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    if (!RegisterSchoolInfo(model.School, user.Id, ref msg))
                    {
                        GetUserForClaims();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError("", msg);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            using (var context = new ApplicationDbContext())
            {

                model.School.Address.States = context.States.Where(s => !s.Deleted && s.Active).Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }).OrderBy(s => s.Text).ToList();

                model.School.Address.Districts = context.Cities.Where(c => !c.Deleted && c.Active && c.StateId == stateId).Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).OrderBy(c => c.Text).ToList();

                model.School.Address.Localities = context.Localities.Where(c => !c.Deleted && c.Active && c.CityId == cityId).Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).OrderBy(l => l.Text).ToList();
            }
            return View(model);
        }


        [AllowAnonymous]
        public ActionResult RegisterParent()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterViewModel();
            model.Parent = new ParentViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterParent(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                string msg = string.Empty;
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    await SignInManager.UserManager.AddToRoleAsync(user.Id, "Parent");
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    if (!RegisterParentInfo(model.Parent, user.Id, ref msg))
                    {
                        GetUserForClaims();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError("", msg);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        private void GetUserForClaims()
        {
            var AppUser = new ApplicationUser();
            using (var context = new ApplicationDbContext())
            {

                if (context != null)
                    AppUser = context.Users.Find(User.Identity.GetUserId());
                if (AppUser != null && AppUser.Email != null)
                    SetClaims(AppUser);
            }
        }

        #region Helpers

        public void SetClaims(ApplicationUser user)
        {
            var authContext = Request.GetOwinContext();
            if (authContext != null)
            {
                var authManager = authContext.Authentication;
                var userIdentity = new ClaimsIdentity(new[]{
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.GivenName,user.Email),
                new Claim(ClaimTypes.SerialNumber,DateTime.UtcNow.ToString("dd/mm/yy mm:hh:ss tt")),
                new Claim(ClaimTypes.Sid,user.Id),
                new Claim(ClaimTypes.CookiePath,"skc.maestro.cookie")
                });

            }

        }

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        private bool RegisterSchoolInfo(SchoolViewModel model, string userId, ref string msg)
        {
            //string msg = string.Empty;
            bool hasError = false;
            int localityId = 0;
            int.TryParse(model.Address.Locality, out localityId);
            try
            {
                var school = new SKC.Lib.Data.Models.Entities.SchoolDTO()
                {
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    EstablishedAt = model.EstablishedAt,
                    Name = model.Name,
                    RegistrationNumber = model.RegistrationNumber,
                    SessionId = Request.RequestContext.HttpContext.Session.SessionID,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = userId,
                    ShortBio = model.ShortBio,
                    WebUrl = model.Url
                };
                var address = new SKC.Lib.Data.Models.Entities.AddressDTO()
                {
                    Active = true,
                    AddressLine1 = model != null ? model.Address.StreetAddress : string.Empty,
                    AddressLine2 = model != null ? model.Address.StreeetAddress2 : string.Empty,
                    CreatedAt = DateTime.UtcNow,
                    SessionId = Request.RequestContext.HttpContext.Session.SessionID,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = userId,
                    School = school,
                    Locality_Id = CommonHelper.GetLocalityById(localityId).Id
                };
                var contactPerson = new SKC.Lib.Data.Models.Entities.ContactDTO()
                {
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    Email = model.ContactPerson.Contact.Email,
                    Number = model.ContactPerson.Contact.Number,
                    SessionId = Request.RequestContext.HttpContext.Session.SessionID,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = userId,
                    School = school,
                    Type = SKC.Lib.Core.Models.MemberType.ContactPerson
                };
                var principal = new SKC.Lib.Data.Models.Entities.ContactDTO()
                {
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    Email = model.Principal.Contact.Email,
                    Number = model.Principal.Contact.Number,
                    SessionId = Request.RequestContext.HttpContext.Session.SessionID,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = userId,
                    School = school,
                    Type = SKC.Lib.Core.Models.MemberType.Principal
                };
                var member_CP = new SKC.Lib.Data.Models.Entities.MemberDTO()
                {
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    FirtsName = model.ContactPerson.FirstName,
                    LastName = model.ContactPerson.LastName,
                    School = school,
                    SessionId = Request.RequestContext.HttpContext.Session.SessionID,
                    Type = SKC.Lib.Core.Models.MemberType.ContactPerson,
                    UpdatedAt = DateTime.UtcNow,
                    Gender = model.ContactPerson.Gender,
                    UserId = userId
                };
                var member_P = new SKC.Lib.Data.Models.Entities.MemberDTO()
                {
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    FirtsName = model.Principal.FirstName,
                    LastName = model.Principal.LastName,
                    School = school,
                    SessionId = Request.RequestContext.HttpContext.Session.SessionID,
                    Type = SKC.Lib.Core.Models.MemberType.Principal,
                    UpdatedAt = DateTime.UtcNow,
                    Gender = model.Principal.Gender,
                    UserId = userId
                };

                //address.Persons.Add(member_CP);
                //address.Persons.Add(member_P);
                //address.ContactNumbers.Add(contactPerson);
                //address.ContactNumbers.Add(principal);
                //address.School = school;

                using (var dbContext = new ApplicationDbContext())
                {
                    dbContext.Schools.Add(school);
                    dbContext.Addresses.Add(address);
                    if (model.ContactPerson != null && model.ContactPerson.Contact != null)
                    {
                        dbContext.Contacts.Add(contactPerson);
                    }
                    if (model.Principal != null && model.Principal.Contact != null)
                    {
                        dbContext.Contacts.Add(principal);
                    }
                    dbContext.Members.Add(member_CP);
                    dbContext.Members.Add(member_P);
                    dbContext.SaveChanges();
                    msg = "Successfully Added Info";
                    hasError = false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                hasError = true;
            }
            return hasError;
        }

        private bool RegisterParentInfo(ParentViewModel model, string userId, ref string msg)
        {
            //string msg = string.Empty;
            bool hasError = false;
            try
            {

                var contactPerson = new SKC.Lib.Data.Models.Entities.ContactDTO()
                {
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    Email = model.Member.Contact.Email,
                    Number = model.Member.Contact.Number,
                    SessionId = Request.RequestContext.HttpContext.Session.SessionID,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = userId,
                    Type = SKC.Lib.Core.Models.MemberType.Parent
                };

                var member_CP = new SKC.Lib.Data.Models.Entities.MemberDTO()
                {
                    Active = true,
                    CreatedAt = DateTime.UtcNow,
                    FirtsName = model.Member.FirstName,
                    LastName = model.Member.LastName,
                    // School = school,
                    SessionId = Request.RequestContext.HttpContext.Session.SessionID,
                    Type = SKC.Lib.Core.Models.MemberType.Parent,
                    UpdatedAt = DateTime.UtcNow,
                    Gender = model.Member.Gender,
                    UserId = userId
                };

                //address.Persons.Add(member_CP);
                //address.Persons.Add(member_P);
                //address.ContactNumbers.Add(contactPerson);
                //address.ContactNumbers.Add(principal);
                //address.School = school;

                using (var dbContext = new ApplicationDbContext())
                {
                    //dbContext.Schools.Add(school);
                    if (model.Member.Contact != null && model.Member.Contact != null)
                    {
                        contactPerson.School = dbContext.Schools.Find(model.SchoolId);

                        dbContext.Contacts.Add(contactPerson);
                    }
                    member_CP.School = dbContext.Schools.Find(model.SchoolId);
                    dbContext.Members.Add(member_CP);
                    dbContext.SaveChanges();
                    msg = "Successfully Added Info";
                    hasError = false;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                hasError = true;
            }
            return hasError;
        }
        #endregion
    }
}