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
    public class FlatTaxonomyController : Controller, ICustomWidgetVisualizationExtended
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

        /// <summary>
        /// Gets or sets the Cascading Style Sheet (CSS) class to visualize the widget.
        /// </summary>
        [Browsable(false)]
        public string WidgetCssClass
        {
            get
            {
                return FlatTaxonomyController.WidgetIconCssClass;
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
        /// Indicates if the control is empty.
        /// </summary>
        /// <value></value>
        public bool IsEmpty
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the text to be shown when the box in the designer is empty
        /// </summary>
        /// <value></value>
        [Browsable(false)]
        public string EmptyLinkText
        {
            get
            {
                string value = this.GetResource("Tags");

                return string.Concat("Set ", value);
            }
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
