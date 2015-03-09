﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm
{
    /// <summary>
    /// This class is used as a model for the <see cref="LoginFormController"/>.
    /// </summary>
    public class LoginFormModel : ILoginFormModel
    {
        #region Constructors

        public LoginFormModel()
        {
            this.userManager = UserManager.GetManager(this.MembershipProvider);
        }

        #endregion
        
        #region Properties

        /// <inheritDoc/>
        public string ServiceUrl
        {
            get
            {
                this.serviceUrl = this.serviceUrl ?? this.GetClaimsIssuer();
                return this.serviceUrl;
            }
            set
            {
                this.serviceUrl = value;
            }
        }
        
        /// <inheritDoc/>
        public bool AllowResetPassword { get; set; }

        /// <inheritDoc/>
        public bool ShowRememberMe { get; set; }

        /// <inheritDoc/>
        public string MembershipProvider
        {
            get
            {
                this.membershipProvider = this.membershipProvider ?? UserManager.GetDefaultProviderName();
                return this.membershipProvider;
            }
            set
            {
                this.membershipProvider = value;
            }
        }

        /// <inheritdoc />
        public string CssClass { get; set; }

        /// <inheritDoc/>
        public Guid? LoginRedirectPageId { get; set; }

        /// <inheritDoc/>
        public Guid? RegisterRedirectPageId { get; set; }

        /// <inheritDoc/>
        public bool EnablePasswordRetrieval 
        {
            get
            {
                return this.userManager.EnablePasswordRetrieval;
            }

            set
            {
            }
        }

        /// <inheritDoc/>
        public bool EnablePasswordReset
        {
            get
            {
                return this.userManager.EnablePasswordReset;
            }

            set
            {
            }
        }

        #endregion 

        #region Public Methods

        /// <inheritDoc/>
        public LoginFormViewModel GetLoginFormViewModel()
        {
            return new LoginFormViewModel() 
            {
                ServiceUrl = this.ServiceUrl,
                MembershipProvider = this.MembershipProvider,
                RedirectUrlAfterLogin = this.GetPageUrl(this.LoginRedirectPageId),
                RegisterPageUrl = this.GetPageUrl(this.RegisterRedirectPageId),
                ShowRegistrationLink = this.RegisterRedirectPageId.HasValue,
                ShowForgotPasswordLink = this.AllowResetPassword && (this.EnablePasswordReset || this.EnablePasswordRetrieval),
                Realm = SitefinityClaimsAuthenticationModule.Current.GetRealm(),
                CssClass = this.CssClass,
                ShowRememberMe = this.ShowRememberMe
            };
        }

        /// <inheritDoc/>
        public ResetPasswordViewModel GetResetPasswordViewModel()
        {
            return new ResetPasswordViewModel()
            {
                CssClass = this.CssClass,
                LoginPageUrl = this.GetPageUrl(null),
                RequiresQuestionAndAnswer = this.userManager.RequiresQuestionAndAnswer
            };
        }

        /// <inheritDoc/>
        public ForgotPasswordViewModel GetForgotPasswordViewModel()
        {
            return new ForgotPasswordViewModel()
            {
                CssClass = this.CssClass,
                LoginPageUrl = this.GetPageUrl(null)
            };
        }

        /// <inheritDoc/>
        public void ResetUserPassword(string newPassword, string answer)
        {
            var userId = this.GetUserId();

            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException("User could not be retrieved.");
            }

            using (new ElevatedModeRegion(this.userManager))
            {
                var resetPassword = this.userManager.ResetPassword(userId, answer);
                this.userManager.ChangePassword(userId, resetPassword, newPassword);
                this.userManager.SaveChanges();
            }
        }

        /// <inheritDoc/>
        public ForgotPasswordViewModel SendResetPasswordEmail(string email)
        {
            var viewModel = new ForgotPasswordViewModel();
            viewModel.Email = email;
            viewModel.EmailSent = false;


            var manager = UserManager.GetManager(this.MembershipProvider);
            var user = manager.GetUserByEmail(email);

            if (user != null)
            {
                if (!UserManager.ShouldSendPasswordEmail(user, this.MembershipProvider.GetType()))
                {
                    viewModel.Error = "Not supported";
                }
                else
                {
                    var resetPassUrl = Url.Combine(SiteMapBase.GetActualCurrentNode().Url, "resetpassword");

                    try
                    {
                        UserManager.SendRecoveryPasswordMail(UserManager.GetManager(user.ProviderName), email, resetPassUrl);

                        viewModel.EmailSent = true;
                    }
                    catch (Exception ex)
                    {
                        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
                            throw ex;

                        viewModel.Error = ex.Message;
                    }
                }
            }
            else
            {
                viewModel.Error = "Invalid data";
            }

            return viewModel;
        }
        
        /// <inheritDoc/>
        public string GetErrorFromViewModel(System.Web.Mvc.ModelStateDictionary modelStateDict)
        {
            var firstErrorValue = modelStateDict.Values.FirstOrDefault(v => v.Errors.Any());
            if (firstErrorValue != null)
            {
                var firstError = firstErrorValue.Errors.FirstOrDefault();
                if (firstError != null)
                {
                    return firstError.ErrorMessage;
                }
            }

            return null;
        }
        
        /// <inheritDoc/>
        public string GetPageUrl(Guid? pageId)
        {
            if (pageId.HasValue)
            {
                return HyperLinkHelpers.GetFullPageUrl(pageId.Value);
            }
            else
            {
                return HyperLinkHelpers.GetFullPageUrl(SiteMapBase.GetActualCurrentNode().Id);
            }
        }

        #endregion

        #region Private Fields and methods

        /// <summary>
        /// Gets the claims issuer.
        /// </summary>
        /// <returns></returns>
        private string GetClaimsIssuer()
        {
            var claimsModule = SitefinityClaimsAuthenticationModule.Current;

            if (claimsModule != null)
            {
                return claimsModule.GetIssuer();
            }
            else
            {
                return LoginFormModel.DefaultRealmConfig;
            }
        }
        
        /// <summary>
        /// Inners the get user identifier.
        /// </summary>
        /// <returns>
        /// The user id or null.
        /// </returns>
        private Guid GetUserId()
        {
            var cip = SecurityManager.GetManager().GetPasswordRecoveryUser();
            if (cip != null)
            {
                return cip.UserId;
            }

            return Guid.Empty;
        }

        private string serviceUrl;
        private const string DefaultRealmConfig = "http://localhost";
        private string membershipProvider;
        private readonly UserManager userManager;

        #endregion
    }
}
