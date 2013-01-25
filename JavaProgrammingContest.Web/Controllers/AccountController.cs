using System;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using System.Web.Security;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Web.Models;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;

namespace JavaProgrammingContest.Web.Controllers{
    /// <summary>
    ///     Authorization Controller
    /// </summary>
    [Authorize]
    public class AccountController : Controller{
        /// <summary>
        ///     Database Context
        /// </summary>
        private readonly IDbContext _context;
       
        /// <summary>
        ///     Controller Constructor
        /// </summary>
        /// <param name="context">Database Context to use.</param>
        public AccountController(IDbContext context){
            _context = context;
        }

        public AccountController(IDbContext context, IWebSecurity webSecurity)
        {
            _context = context; 
        }

        /// <summary>
        ///     When a user is logged in and visits Logon page
        /// </summary>
        /// <param name="returnUrl">Url to redirect to.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Logon(string returnUrl){
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Logon(LogonModel model, string returnUrl){
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, model.RememberMe))
                return RedirectToLocal(returnUrl);

            ModelState.AddModelError(string.Empty, "De gebruikersnaam of wachtwoord is niet correct.");
            return View(model);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff(){
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Register(){
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model){
            if (ModelState.IsValid)
                try{
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    WebSecurity.Login(model.UserName, model.Password);
                    return RedirectToAction("Index", "Editor");
                } catch (MembershipCreateUserException e){
                    ModelState.AddModelError(string.Empty, ErrorCodeToString(e.StatusCode));
                }

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="providerUserId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId){
            var ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            if (ownerAccount == User.Identity.Name)
                using (
                    var scope = new TransactionScope(TransactionScopeOption.Required,
                        new TransactionOptions{IsolationLevel = IsolationLevel.Serializable})){
                    var hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1){
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLogonSuccess;
                    }
                }

            return RedirectToAction("Manage", new{Message = message});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ActionResult Manage(ManageMessageId? message){
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess
                    ? "Uw wachtwoord is veranderd."
                    : message == ManageMessageId.SetPasswordSuccess
                          ? "Uw wachtwoord is opgeslagen."
                          : message == ManageMessageId.RemoveLogonSuccess
                                ? "De externe login is verwijderd."
                                : string.Empty;

            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model){
            var hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount){
                if (ModelState.IsValid){
                    bool changePasswordSucceeded;
                    try{
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    } catch (Exception){
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                        return RedirectToAction("Manage", new{Message = ManageMessageId.ChangePasswordSuccess});
                    ModelState.AddModelError(string.Empty, "Het huidige wachtwoord is niet correct of het nieuwe wachtwoord klopt niet.");
                }
            } else{
                var state = ModelState["OldPassword"];
                if (state != null)
                    state.Errors.Clear();

                if (ModelState.IsValid)
                    try{
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new{Message = ManageMessageId.SetPasswordSuccess});
                    } catch (Exception e){
                        ModelState.AddModelError(string.Empty, e);
                    }
            }

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogon(string provider, string returnUrl){
            return new ExternalLogonResult(provider, Url.Action("ExternalLogonCallback", new{ReturnUrl = returnUrl}));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ExternalLogonCallback(string returnUrl){
            var result =
                OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLogonCallback", new{ReturnUrl = returnUrl}));
            if (!result.IsSuccessful)
                return RedirectToAction("ExternalLogonFailure");

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
                return RedirectToLocal(returnUrl);

            if (User.Identity.IsAuthenticated){
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }

            var logonData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
            ViewBag.Name = result.UserName;
            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View("ExternalLogonConfirmation",
                new RegisterExternalLogonModel { UserName = result.UserName, Name = result.UserName, Functie = result.ExtraData["headline"], ExternalLogonData = logonData });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogonConfirmation(RegisterExternalLogonModel model, string returnUrl){
            string provider;
            string providerUserId;

            if (User.Identity.IsAuthenticated ||
                !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLogonData, out provider, out providerUserId))
                return RedirectToAction("Manage");

            if (ModelState.IsValid){
                var user = _context.Participants.FirstOrDefault(u => u.Email.ToLower() == model.UserName.ToLower());
                if (user == null){
                    _context.Participants.Add(new Participant{Email = model.UserName, Name = model.Name, Functie = model.Functie});
                    _context.SaveChanges();

                    OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
                    OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError("UserName", "Deze naam bestaat al. Kies een andere naam.");
            }
            ViewBag.Name = model.Name;
            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ExternalLogonFailure(){
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLogonsList(string returnUrl){
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLogonsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult RemoveExternalLogons(){
            var accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);

            var externalLogons = (from account in accounts
                let clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider)
                select new ExternalLogon{
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                }).ToList();

            ViewBag.ShowRemoveButton = externalLogons.Count > 1 ||
                                       OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLogonsPartial", externalLogons);
        }

        #region Helpers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        private ActionResult RedirectToLocal(string returnUrl){
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 
        /// </summary>
        public enum ManageMessageId{
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLogonSuccess,
        }

        /// <summary>
        /// 
        /// </summary>
        internal class ExternalLogonResult : ActionResult{
            /// <summary>
            /// 
            /// </summary>
            /// <param name="provider"></param>
            /// <param name="returnUrl"></param>
            public ExternalLogonResult(string provider, string returnUrl){
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            /// <summary>
            /// 
            /// </summary>
            public string Provider { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            public string ReturnUrl { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="context"></param>
            public override void ExecuteResult(ControllerContext context){
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="createStatus"></param>
        /// <returns></returns>
        private static string ErrorCodeToString(MembershipCreateStatus createStatus){
            switch (createStatus){
                case MembershipCreateStatus.DuplicateUserName:
                    return "Deze naam bestaat al. Kies een andere naam.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Dit email adres bestaat al. Kies een ander email adres.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Dit wachtwoord klopt niet. Voer een correcte wachtwoord in.";

                case MembershipCreateStatus.InvalidEmail:
                    return "Dit email adres klopt niet. Voer een correct email adres in.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "Het wachtwoord herstel antwoord klopt niet. Voer een correct antwoord in.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "De wachtwoord herstel vraag klopt niet. Voer een correcte vraag in.";

                case MembershipCreateStatus.InvalidUserName:
                    return "De user naam klopt niet. Voer een correcte naam in.";

                case MembershipCreateStatus.ProviderError:
                    return
                        "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return
                        "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return
                        "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}