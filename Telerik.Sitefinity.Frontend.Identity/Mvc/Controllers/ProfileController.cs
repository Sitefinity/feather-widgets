using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Provider;
using System.Linq;
using System.Web.Mvc;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Profile;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Security;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.EmailConfirmationOperations;
using Telerik.Sitefinity.Security.Web.UI;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Profile widget.
    /// </summary>
    [Localization(typeof(ProfileResources))]
    [ControllerToolboxItem(
        Name = ProfileController.WidgetName,
        Title = nameof(ProfileResources.UserProfileViewTitle),
        Description = nameof(ProfileResources.UserProfilesViewDescription),
        ResourceClassId = nameof(ProfileResources),
        SectionName = "Users",
        CssClass = ProfileController.WidgetIconCssClass)]
    public class ProfileController : Controller
    {
        #region Properties

        /// <summary>
        /// Gets the Profile widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IProfileModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = this.InitializeModel();

                return this.model;
            }
        }

        /// <summary>
        /// Gets or sets the name of the edit mode template that widget will be displayed.
        /// </summary>
        /// <value></value>
        public string EditModeTemplateName
        {
            get
            {
                return this.editModeTemplateName;
            }

            set
            {
                this.editModeTemplateName = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the read mode template that widget will be displayed.
        /// </summary>
        /// <value></value>
        public string ReadModeTemplateName
        {
            get
            {
                return this.readModeTemplateName;
            }

            set
            {
                this.readModeTemplateName = value;
            }
        }

        /// <summary>
        /// Gets or sets the widget display mode.
        /// </summary>
        /// <value>
        /// The widget display mode.
        /// </value>
        public ViewMode Mode { get; set; }

        #endregion

        #region Actions

        /// <summary>
        /// Renders appropriate list view depending on the <see cref="TemplateName" />
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index()
        {
            if (this.Mode == ViewMode.EditOnly && !this.Model.CanEdit())
            {
                return this.Content(Res.Get<ProfileResources>().EditNotAllowed);
            }

            this.RegisterCustomOutputCacheVariation();

            this.ViewBag.Mode = this.Mode;
            if (this.Mode == ViewMode.EditOnly)
            {
                return this.EditProfile();
            }
            else
            {
                return this.ReadProfile();
            }
        }

        /// <summary>
        /// Renders profile widget in edit mode.
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProfile()
        {
            if (!this.Model.CanEdit())
            {
                return this.Content(Res.Get<ProfileResources>().EditNotAllowed);
            }

            var fullTemplateName = EditModeTemplatePrefix + this.EditModeTemplateName;
            var viewModel = this.Model.GetProfileEditViewModel();
            if (viewModel == null)
                return null;

            this.SetReadOnlyInfo(viewModel);

            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Edit user's email.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditEmail(ProfileEmailEditViewModel viewModel)
        {
            if (!AntiCsrfHelpers.IsValidCsrfToken(this.Request?.Form))
                return new EmptyResult();

            if (ModelState.IsValid)
            {
                try
                {
                    var success = this.Model.EditUserEmail(viewModel);

                    if (!success)
                    {
                        this.ViewBag.ErrorMessage = Res.Get<ProfileResources>().InvalidPassword;
                    }
                    else
                    {
                        if (this.Model.ChangeEmailConfirmation)
                        {
                            var templateName = ConfirmationEmailSent + this.EditModeTemplateName;
                            return this.View(templateName);
                        }
                        else
                        {
                            switch (this.Model.SaveChangesAction)
                            {
                                case SaveAction.SwitchToReadMode:
                                    return this.ReadProfile();
                                case SaveAction.ShowMessage:
                                    viewModel.ShowProfileChangedMsg = true;
                                    break;
                                case SaveAction.ShowPage:
                                    return this.Redirect(this.Model.GetPageUrl(this.Model.ProfileSavedPageId));
                            }
                        }
                    }
                }
                catch (DuplicateKeyException)
                {
                    this.ViewBag.ErrorMessage = Res.Get<ProfileResources>().EmailExistsMessage;
                }
                catch (ProviderException ex)
                {
                    this.ViewBag.ErrorMessage = ex.Message;
                }
            }

            var fullTemplateName = ConfirmPasswordModeTemplatePrefix + this.EditModeTemplateName;
            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Sends again email change confirmation.
        /// </summary>
        /// <param name="qs">The encoded query string.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SendAgainChangeEmailConfirmation(string qs)
        {
            var sentAgain = this.Model.SendAgainChangeEmailConfirmation(qs);

            this.ViewBag.Email = this.Model.UserName;
            var fullTemplateName = ConfirmationEmailSent + this.EditModeTemplateName;
            return this.View(fullTemplateName);
        }


        /// <summary>
        /// Confirms email change.
        /// </summary>
        /// <param name="qs">The encoded query string</param>
        [HttpGet]
        public ActionResult EditEmail(string qs)
        {
            EmailChangeConfirmationData emailChangeConfirmationData = null;
            ProfileEmailEditViewModel viewModel = new ProfileEmailEditViewModel();

            try
            {
                emailChangeConfirmationData = UserRegistrationEmailGenerator.GetEmailConfirmationData<EmailChangeConfirmationData>(qs);
                viewModel.Email = emailChangeConfirmationData.NewEmail;
                viewModel.UserId = emailChangeConfirmationData.UserId;

                if (emailChangeConfirmationData.Expiration < DateTime.UtcNow)
                {
                    viewModel.ConfirmEmailChangeFailure = ConfirmEmailChangeFailure.Expired;
                }
                else
                {
                    if (this.Model.ConfirmEmailChange(emailChangeConfirmationData))
                    {
                        viewModel.ShowProfileChangedMsg = true;
                    }
                    else
                    {
                        viewModel.ConfirmEmailChangeFailure = ConfirmEmailChangeFailure.Error;
                    }
                }
            }
            catch
            {
                viewModel.ConfirmEmailChangeFailure = ConfirmEmailChangeFailure.Error;
            }

            var fullTemplateName = ConfirmPasswordModeTemplatePrefix + this.EditModeTemplateName;
            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Posts the registration form.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        [HttpPost]
        public ActionResult Index(ProfileEditViewModel viewModel)
        {
            if (!AntiCsrfHelpers.IsValidCsrfToken(this.Request?.Form))
                return new EmptyResult();
            this.Model.ValidateProfileData(viewModel, this.ModelState);
            this.Model.InitializeUserRelatedData(viewModel, false);

            if (ModelState.IsValid)
            {
                try
                {
                    var isUpdated = this.Model.EditUserProfile(viewModel);
                    if (!isUpdated)
                    {
                        return this.Content(Res.Get<ProfileResources>().EditNotAllowed);
                    }

                    if (this.Model.IsEmailChanged(viewModel))
                    {
                        return this.View(ConfirmPasswordModeTemplatePrefix + this.EditModeTemplateName,
                            new ProfileEmailEditViewModel()
                            {
                                UserId = viewModel.User.Id,
                                Email = viewModel.Email
                            });
                    }

                    switch (this.Model.SaveChangesAction)
                    {
                        case SaveAction.SwitchToReadMode:
                            return this.ReadProfile();
                        case SaveAction.ShowMessage:
                            viewModel.ShowProfileChangedMsg = true;
                            break;
                        case SaveAction.ShowPage:
                            return this.Redirect(this.Model.GetPageUrl(this.Model.ProfileSavedPageId));
                    }
                }
                catch (ProviderException ex)
                {
                    this.ViewBag.ErrorMessage = ex.Message;
                }
                catch (DuplicateKeyException)
                {
                    this.ViewBag.ErrorMessage = Res.Get<ProfileResources>().EmailExistsMessage;
                }
                catch (Exception)
                {
                    this.ViewBag.ErrorMessage = Res.Get<ProfileResources>().ChangePasswordGeneralErrorMessage;
                }
            }

            this.ViewBag.HasPasswordErrors = !this.ModelState.IsValidField("OldPassword") ||
                                             !this.ModelState.IsValidField("NewPassword") ||
                                             !this.ModelState.IsValidField("RepeatPassword") ||
                                             !string.IsNullOrEmpty(this.ViewBag.ErrorMessage);

            this.SetReadOnlyInfo(viewModel);

            var fullTemplateName = ProfileController.EditModeTemplatePrefix + this.EditModeTemplateName;
            return this.View(fullTemplateName, viewModel);
        }

        /// <inheritDocs/>
        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="IProfileModel"/>.
        /// </returns>
        private IProfileModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IProfileModel>(this.GetType());
        }

        /// <summary>
        /// Retrieves view for read only mode of Profile widget.
        /// </summary>
        /// <returns></returns>
        private ActionResult ReadProfile()
        {
            var viewModel = this.Model.GetProfilePreviewViewModel();
            if (viewModel == null)
                return null;

            var fullTemplateName = ReadModeTemplatePrefix + this.ReadModeTemplateName;
            return this.View(fullTemplateName, viewModel);
        }

        private void RegisterCustomOutputCacheVariation()
        {
            PageRouteHandler.RegisterCustomOutputCacheVariation(new UserProfileMvcOutputCacheVariation());
        }

        private void SetReadOnlyInfo(ProfileEditViewModel viewModel)
        {
            this.ViewBag.IsEmailReadOnly = false;
            this.ViewBag.ReadOnlyFields = new Dictionary<string, IEnumerable<string>>();

            if (viewModel.User != null && viewModel.User.ExternalProviderName != null)
            {
                foreach (var userProfile in viewModel.SelectedUserProfiles)
                {
                    var profileName = userProfile.UserProfile.GetType().Name;
                    this.ViewBag.ReadOnlyFields[profileName] = UserManager.GetReadOnlyFields(profileName, viewModel.User.ExternalProviderName).ToArray();
                }

                this.ViewBag.IsEmailReadOnly = UserManager.IsEmailMapped(viewModel.User.ExternalProviderName);
            }
        }
        #endregion

        #region Private fields and constants

        internal const string WidgetIconCssClass = "sfProfilecn sfMvcIcn";
        internal const string RegisterOCVariationMethodName = "RegisterCustomOutputCacheVariation";

        private string readModeTemplateName = "ProfilePreview";
        private string editModeTemplateName = "ProfileEdit";

        private const string ReadModeTemplatePrefix = "Read.";
        private const string EditModeTemplatePrefix = "Edit.";
        private const string ConfirmPasswordModeTemplatePrefix = "ConfirmPassword.";
        private const string ConfirmationEmailSent = "ConfirmationEmailSent.";

        private IProfileModel model;
        private const string WidgetName = "Profile_MVC";
        #endregion
    }
}
