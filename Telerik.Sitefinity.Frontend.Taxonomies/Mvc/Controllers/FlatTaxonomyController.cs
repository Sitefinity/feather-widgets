using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.FlatTaxonomy;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Flat taxonomy widget.
    /// </summary>
    [ControllerToolboxItem(Name = "Tags_MVC",
        Title = "Tags",
        SectionName = "Classifications",
        CssClass = FlatTaxonomyController.WidgetIconCssClass)]
    [Localization(typeof(FlatTaxonomyResources))]
    [IndexRenderMode(IndexRenderModes.NoOutput)]
    public class FlatTaxonomyController : Controller
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the template that widget will be displayed.
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
        /// Gets the Flat taxonomy widget model.
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IFlatTaxonomyModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IFlatTaxonomyModel>(this.GetType());

                return this.model;
            }
        }
        #endregion

        #region Actions
        /// <summary>
        /// Default Action
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var viewModel = this.Model.CreateViewModel();
            
            var fullTemplateName = this.templateNamePrefix + this.TemplateName;
            
            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.Index().ExecuteResult(this.ControllerContext);
        }
       
        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns></returns>
        protected virtual string GetResource(string resourceName)
        {
            return Res.Get(typeof(FlatTaxonomyResources), resourceName);
        }

        #endregion

        #region Private fields and constants
        internal const string WidgetIconCssClass = "sfFlatTaxonIcn sfMvcIcn";
        private IFlatTaxonomyModel model;
        private string templateName = "SimpleList";
        private readonly string templateNamePrefix = "FlatTaxonomy.";
        #endregion
    }
}
