using Navigation.Mvc.Models;
using Navigation.Mvc.StringResources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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
        /// Gets the name of the template that will be displayed.
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
        /// Gets the Navigation widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public INavigationModel Model 
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
        /// Renders appropriate view depending on the <see cref="TemplateName" />
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var fullTemplateName = templateNamePrefix + this.TemplateName;
            return View(fullTemplateName, this.Model);
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
            return ControllerModelFactory.GetModel<INavigationModel>(this.GetType());
        }
 
        #endregion

        #region Private fields and constants

        private INavigationModel model;
        private string templateName = "Horizontal";
        private string templateNamePrefix = "NavigationView.";

        #endregion
    }
}
