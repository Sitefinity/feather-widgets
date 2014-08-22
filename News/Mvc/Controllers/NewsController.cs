using System.Collections.Generic;
using System.Web.Mvc;
using News.Mvc.Models;
using News.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;
using System.ComponentModel;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Routing;
using Telerik.Sitefinity.Taxonomies.Model;

namespace News.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of News widget.
    /// </summary>
    [ControllerToolboxItem(Name = "News", Title = "News", SectionName = "MvcWidgetsNews")]
    [Localization(typeof(NewsWidgetResources))]
    public class NewsController : Controller, IUrlMappingController
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the template that will be displayed when widget is in List view.
        /// </summary>
        /// <value></value>
        public string ListTemplateName
        {
            get
            {
                return this.listTemplateName;
            }

            set
            {
                this.listTemplateName = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the template that will be displayed when widget is in Detail view.
        /// </summary>
        /// <value></value>
        public string DetailTemplateName
        {
            get
            {
                return this.detailTemplateName;
            }

            set
            {
                this.detailTemplateName = value;
            }
        }

        /// <summary>
        /// Gets or sets the template for the URL of the List view that will be used with higher priority.
        /// </summary>
        /// <value>
        /// The master route template.
        /// </value>
        public string MasterRouteTemplate
        {
            get 
            {
                return this.masterRouteTemplate;
            }
            set 
            {
                this.masterRouteTemplate = value;
            }
        }

        /// <inheritdoc />
        public IUrlParamsMapper UrlParamsMapper
        {
            get 
            {
                if (this.urlParamsMapper == null)
                {
                    this.urlParamsMapper = new DetailActionParamsMapper<NewsItem>(this);

                    this.urlParamsMapper
                        .SetNext(new CustomActionParamsMapper(this, () => this.MasterRouteTemplate, "ListByTaxon"))
                        .SetNext(new CustomActionParamsMapper(this, () => "/{taxonFilter:category,tag}/{page}", "ListByTaxon"))
                        .SetNext(new CustomActionParamsMapper(this, () => "/{taxonFilter:category,tag}", "ListByTaxon"))
                        .SetNext(new CustomActionParamsMapper(this, () => "/{page}", "Index"))
                        .SetNext(new DefaultUrlParamsMapper(this));
                }

                return this.urlParamsMapper;
            }
        }

        /// <summary>
        /// Gets the News widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public INewsModel Model 
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
        /// Renders appropriate list view depending on the <see cref="ListTemplateName" />
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index(int? page)
        {
            var fullTemplateName = this.listTemplateNamePrefix + this.ListTemplateName;
            this.ViewBag.RedirectPageUrlTemplate = "/{0}";

            this.Model.PopulateNews(null, page);

            return this.View(fullTemplateName, this.Model);
        }

        /// <summary>
        /// Renders appropriate list view depending on the <see cref="ListTemplateName" />
        /// </summary>
        /// <param name="taxonFilter">The taxonomy filter.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult ListByTaxon(ITaxon taxonFilter, int? page)
        {
            var fullTemplateName = this.listTemplateNamePrefix + this.ListTemplateName;
            this.ViewBag.RedirectPageUrlTemplate = "/" + taxonFilter.Name + "/{0}";
            this.Model.PopulateNews(taxonFilter, page);

            return this.View(fullTemplateName, this.Model);
        }

        /// <summary>
        /// Renders appropriate list view depending on the <see cref="DetailTemplateName"/>
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Details(NewsItem newsItem)
        {
            var fullTemplateName = this.detailTemplateNamePrefix + this.DetailTemplateName;
            this.Model.DetailNews = newsItem;
            this.ViewBag.Title = newsItem.Title;

            return this.View(fullTemplateName, this.Model);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="INewsModel"/>.
        /// </returns>
        private INewsModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<INewsModel>(this.GetType());
        }
 
        #endregion

        #region Private fields and constants

        private INewsModel model;
        private string listTemplateName = "NewsList";
        private string listTemplateNamePrefix = "List.";
        private string detailTemplateName = "DetailPage";
        private string detailTemplateNamePrefix = "Detail.";

        private string masterRouteTemplate = "/{taxonFilter:category,tag}/{page}";
        private IUrlParamsMapper urlParamsMapper;

        #endregion
    }
}
