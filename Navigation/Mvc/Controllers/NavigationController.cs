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
        protected string LayoutTemplateName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the page links to display selection mode.
        /// </summary>
        /// <value>The page display mode.</value>
        public PageSelectionModes SelectionMode
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

        ///// <summary>
        ///// Gets or sets the starting node URL.
        ///// </summary>
        //public string StartingNodeUrl
        //{
        //    get;
        //    set;
        //}

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
        /// Renders appropriate view depending on the <see cref="LayoutTemplateName"/>
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            this.LayoutTemplateName = "Horizontal";
            return View(this.LayoutTemplateName, this.Model);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
         /// <returns></returns>
        private INavigationModel InitializeModel()
        {
            var assemblies = new List<Assembly>();
            var contentBlockControllerAssembly = typeof(NavigationController).Assembly;
            var currentAssembly = this.GetType().Assembly;

            assemblies.Add(currentAssembly);
            if (!contentBlockControllerAssembly.Equals(currentAssembly))
                assemblies.Add(contentBlockControllerAssembly);

            var constructorParameters = new Dictionary<string, object> 
                         {
                            {"selectionMode", this.SelectionMode},
                            {"levelsToInclude", this.LevelsToInclude},
                            {"showParentPage", this.ShowParentPage},
                            {"cssClass", this.CssClass}
                         };


            return ControllerModelFactory.GetModel<INavigationModel>(assemblies, constructorParameters) as INavigationModel;
        }
 
        #endregion

        #region Private fields and constants

        private INavigationModel model;
        private int? levelsToInclude = 1;

        #endregion
    }
}
