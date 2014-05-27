using Navigation.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Mvc;

namespace Navigation.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of Navigation widget.
    /// </summary>
    [ControllerToolboxItem(Name = "Navigation", Title = "Navigation", SectionName = "MvcWidgets")]
    public class NavigationController : Controller
    {
        #region Properties

        /// <summary>
        /// Gets the name of the template that will be displayed.
        /// </summary>
        /// <value></value>
        public string TemplateName
        {
            get;
            set;
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
        /// Gets or sets whether to show parent page
        /// </summary>
        public bool ShowParentPage
        {
            get;
            set;
        }

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
        /// <returns></returns>
        public ActionResult Index()
        {
            if (this.TemplateName.IsNullOrEmpty())
                return Content("TemplateName property should be set to the name of existing template.");
            else 
                return View(this.TemplateName, this.Model);
        }

        /// <summary>
        /// Gets the view with provided name.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ActionResult GetView(string viewName, INavigationModel model)
        {
            return PartialView(viewName, model);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
         /// <returns></returns>
        private INavigationModel InitializeModel()
        {
            var constructorParameters = new Dictionary<string, object> 
                         {
                            {"selectionMode", this.SelectionMode},
                            {"levelsToInclude", this.LevelsToInclude},
                            {"showParentPage", this.ShowParentPage},
                            {"cssClass", this.CssClass}
                         };

            return ControllerModelFactory.GetModel<INavigationModel>(this.GetType(), constructorParameters);
        }
 
        #endregion

        #region Private fields and constants

        private INavigationModel model;
        private int? levelsToInclude = 1;

        #endregion
    }
}
