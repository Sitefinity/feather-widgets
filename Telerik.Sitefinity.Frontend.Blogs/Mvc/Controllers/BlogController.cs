using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Blogs.Model;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.Models.Blog;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Blog widget.
    /// </summary>
    [Localization(typeof(BlogListResources))]
    [ControllerToolboxItem(
        Name = BlogController.WidgetName,
        Title = nameof(BlogListResources.BlogsList),
        Description = nameof(BlogListResources.BlogsListDescription),
        ResourceClassId = nameof(BlogListResources),
        SectionName = ToolboxesConfig.ContentToolboxSectionName,
        ModuleName = "Blogs",
        CssClass = BlogController.WidgetIconCssClass)]
    public class BlogController : Controller, IPersonalizable
    {
        #region Properties

        /// <summary>
        /// Gets the Blog widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IBlogModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IBlogModel>(this.GetType());

                return this.model;
            }
        }

        /// <summary>
        /// Gets or sets the name of the template that will be displayed when widget is in List view.
        /// </summary>
        /// <value>
        /// The name of the list template.
        /// </value>
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
        /// <value>
        /// The name of the details template.
        /// </value>
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
        /// Gets or sets a value indicating where detail view for the blog should be opened.
        /// </summary>
        /// <value>
        /// The detail page mode.
        /// </value>
        public BlogDetailLocationMode DetailPageMode { get; set; }

        /// <summary>
        /// Gets or sets the id of the page where will be displayed details view for selected item when <see cref="DetailPageMode"/> is set to SelectedExistingPage.
        /// </summary>
        /// <value>
        /// The details page id.
        /// </value>
        public Guid DetailsPageId { get; set; }

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
            var viewModel = this.Model.CreateListViewModel(page: page ?? 1);

            var redirectPageUrlTemplate = UrlHelpers.GetRedirectPagingUrl();
            this.InitializeListViewBag(redirectPageUrlTemplate);

            var fullTemplateName = this.listTemplateNamePrefix + this.ListTemplateName;

            if (this.ShouldReturnDetails(this.Model.ContentViewDisplayMode, viewModel))
            {
                var itemViewModel = viewModel.Items.FirstOrDefault();

                if (itemViewModel == null)
                    return this.HandleInvalidDetailsAction(Res.Get<BlogListResources>().BlogListDetailViewDesignerResponseMessage);

                return this.Details((Blog)itemViewModel.DataItem);
            }


            if (SystemManager.CurrentHttpContext != null)
            {
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));
                if (viewModel.ContentType != null)
                    this.AddCacheVariations(viewModel.ContentType, viewModel.ProviderName);
            }

            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Renders appropriate list view depending on the <see cref="DetailTemplateName"/>
        /// </summary>
        /// <param name="item">The item which details will be displayed.</param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Details(Blog item)
        { 
            var fullTemplateName = this.detailTemplateNamePrefix + this.DetailTemplateName;

            if (item != null)
                this.ViewBag.Title = item.Title;

            var viewModel = this.Model.CreateDetailsViewModel(item);

            if (SystemManager.CurrentHttpContext != null)
                this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

            return this.View(fullTemplateName, viewModel);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the ListView bag.
        /// </summary>
        /// <param name="redirectPageUrl">The redirect page URL.</param>
        private void InitializeListViewBag(string redirectPageUrl)
        {
            this.ViewBag.CurrentPageUrl = SystemManager.CurrentHttpContext != null ? this.GetCurrentPageUrl() : string.Empty;
 
            var redirectPageUrlTemplate = this.ViewBag.CurrentPageUrl + redirectPageUrl;
            var sanitizer = ObjectFactory.Resolve<IHtmlSanitizer>();
            redirectPageUrlTemplate = sanitizer.SanitizeUrl(redirectPageUrlTemplate);

            this.ViewBag.RedirectPageUrlTemplate = redirectPageUrlTemplate;
            this.ViewBag.ItemsPerPage = this.Model.ItemsPerPage;
            this.ViewBag.DetailPageMode = this.DetailPageMode;
            this.ViewBag.DetailsPageId = this.DetailsPageId;
        }

        #endregion

        #region Private fields and constants

        internal const string WidgetIconCssClass = "sfBlogsListViewIcn sfMvcIcn";
        private IBlogModel model;

        private string listTemplateName = "BlogList";
        private string listTemplateNamePrefix = "List.";
        private string detailTemplateName = "DetailPage";
        private string detailTemplateNamePrefix = "Detail.";
        private const string WidgetName = "Blog_MVC";

        #endregion
    }
}
