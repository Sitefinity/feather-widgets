using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.HierarchicalTaxonomy;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.StringResources;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Hierarchical taxonomy widget.
    /// </summary>
    [ControllerToolboxItem(Name = "Categories_MVC",
        Title = "Categories",
        SectionName = "Classifications",
        CssClass = HierarchicalTaxonomyController.WidgetIconCssClass)]
    [Localization(typeof(HierarchicalTaxonomyResources))]
    public class HierarchicalTaxonomyController : Controller
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
        /// Gets the Hierarchical taxonomy widget model.
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IHierarchicalTaxonomyModel Model
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

        #endregion

        #region Private methods
        private IHierarchicalTaxonomyModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IHierarchicalTaxonomyModel>(this.GetType());
        }
        #endregion

        #region Private fields and constants
        internal const string WidgetIconCssClass = "sfHierarchicalTaxonIcn sfMvcIcn";
        private IHierarchicalTaxonomyModel model;
        private string templateName = "CategoriesList";
        private string templateNamePrefix = "HierarchicalTaxonomy.";
        #endregion
    }
}
