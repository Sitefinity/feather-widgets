using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Profile;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Profile widget.
    /// </summary>
    [Localization(typeof(ProfileResources))]
    [ControllerToolboxItem(Name = "ProfileMVC", Title = "Profile", SectionName = "MvcWidgets")]
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
            if (ModelState.IsValid)
            {
                var isUpdated = this.Model.EditUserProfile(viewModel);
                if (!isUpdated)
                    return this.Content(Res.Get<ProfileResources>().EditNotAllowed);

                switch (this.Model.SaveChangesAction)
                {
                    case SaveAction.SwitchToReadMode:
                        return this.ReadProfile();
                    case SaveAction.ShowMessage:
                        viewModel.ShowProfileChangedMsg = true;
                        var templateName = ProfileController.EditModeTemplatePrefix + this.EditModeTemplateName;
                        return this.View(templateName, viewModel);
                    case SaveAction.ShowPage:
                        return this.Redirect(this.Model.GetPageUrl(this.Model.ProfileSavedPageId));
                }
            }

            var fullTemplateName = ProfileController.EditModeTemplatePrefix + this.EditModeTemplateName;
            return this.View(fullTemplateName, viewModel);
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

        #endregion

        #region Private fields and constants

        private string readModeTemplateName = "ProfilePreview";
        private string editModeTemplateName = "ProfileEdit";

        private const string ReadModeTemplatePrefix = "Read.";
        private const string EditModeTemplatePrefix = "Edit.";

        private IProfileModel model;

        #endregion
    }
}
