using System.Collections.Generic;
using System.Web.Mvc;

using Navigation.Mvc.Models;
using Navigation.Mvc.StringResources;

using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;

namespace Navigation.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of Navigation widget.
    /// </summary>
    [ControllerToolboxItem(Name = "Navigation", Title = "Navigation", SectionName = "MvcWidgets")]
    [Localization(typeof(NavigationResources))]
    public class NavigationController : Controller
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
        /// Gets or sets the page links to display selection mode.
        /// </summary>
        /// <value>The page display mode.</value>
        public PageSelectionMode SelectionMode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show parent page].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show parent page]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowParentPage { get; set; }

        /// <summary>
        /// Gets or sets the levels to include.
        /// </summary>
        public virtual int? LevelsToInclude
        {
            get 
            {
                return this.levelsToInclude;
            }

            set 
            {
                this.levelsToInclude = value;
            }
        }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the NavigationWidget (if such is presented).
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the Navigation widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        protected INavigationModel Model 
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
            return this.View(fullTemplateName, this.Model);
        }

        /// <summary>
        /// Gets the view with provided name.
        /// </summary>
        /// <param name="viewName">
        /// Name of the view.
        /// </param>
        /// <param name="viewModel">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult GetView(string viewName, INavigationModel viewModel)
        {
            return this.PartialView(viewName, viewModel);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="INavigationModel"/>.
        /// </returns>
        private INavigationModel InitializeModel()
        {
            var constructorParameters = new Dictionary<string, object> 
                         {
                            { "selectionMode", this.SelectionMode }, 
                            { "levelsToInclude", this.LevelsToInclude }, 
                            { "showParentPage", this.ShowParentPage }, 
                            { "cssClass", this.CssClass }
                         };

            return ControllerModelFactory.GetModel<INavigationModel>(this.GetType(), constructorParameters);
        }
 
        #endregion

        #region Private fields and constants

        private INavigationModel model;
        private int? levelsToInclude = 1;
        private string templateName = "Horizontal";
        private string templateNamePrefix = "NavigationView.";

        #endregion
    }
}
