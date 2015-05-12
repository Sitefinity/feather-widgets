using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.SiteSelector;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.StringResources;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Site selector widget.
    /// </summary>
    [ControllerToolboxItem(Name = "SiteSelector_MVC",
       Title = "Site selector",
       SectionName = ToolboxesConfig.NavigationControlsSectionName,
       CssClass = SiteSelectorController.WidgetIconCssClass)]
    [Localization(typeof(SiteSelectorResources))]
    public class SiteSelectorController : Controller
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the template that will be displayed.
        /// </summary>
        /// <value></value>
        public string TemplateName
        {
            get
            {
                return this.templateName;
            }

            set
            {
                this.templateName = value;
            }
        }

        /// <summary>
        /// Gets the Site selector widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual ISiteSelectorModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = this.InitializeModel();

                return this.model;
            }
        }
        #endregion

        #region Actions
        /// <summary>
        /// Renders appropriate view depending on the <see cref="TemplateName"/>
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var fullTemplateName = this.templateNamePrefix + this.TemplateName;

            var viewModel = this.Model.CreateViewModel();

            return this.View(fullTemplateName, viewModel);
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="ISiteSelectorModel"/>.
        /// </returns>
        private ISiteSelectorModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<ISiteSelectorModel>(this.GetType());
        }
        #endregion

        #region Private fields and constants

        private ISiteSelectorModel model;
        internal const string WidgetIconCssClass = "sfSiteSelectorIcn sfMvcIcn";
        private string templateNamePrefix = "SiteSelector.";
        private string templateName = "SiteLinks";
        #endregion
    }
}
