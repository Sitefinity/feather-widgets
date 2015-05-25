using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.FlatTaxonomy;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.StringResources;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Flat taxonomy widget.
    /// </summary>
    [ControllerToolboxItem(Name = "FlatTaxonomy_MVC",
        Title = "FlatTaxonomy", 
        SectionName = "Classifications",
        CssClass = FlatTaxonomyController.WidgetIconCssClass)]
    [Localization(typeof(FlatTaxonomyResources))]
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

        #endregion

        #region Private methods
       
        #endregion

        #region Private fields and constants
        internal const string WidgetIconCssClass = "sfFlatTaxonIcn sfMvcIcn";
        private IFlatTaxonomyModel model;
        private string templateName = "SimpleList";
        private string templateNamePrefix = "FlatTaxonomy.";
        #endregion
    }
}
