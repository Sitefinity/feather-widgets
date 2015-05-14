using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Comments.Mvc.Models;
using Telerik.Sitefinity.Frontend.Comments.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Comments.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Reviews widget.
    /// </summary>
    [Localization(typeof(CommentsWidgetResources))]
    [ControllerToolboxItem(Name = "Reviews_MVC", Title = "Reviews", SectionName = ToolboxesConfig.ContentToolboxSectionName, ModuleName = "Comments", CssClass = ReviewsController.WidgetIconCssClass)]
    public class ReviewsController : Controller
    {
        #region Properties

        /// <summary>
        /// Gets the Comments widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual ICommentsModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<ICommentsModel>(this.GetType());

                return this.model;
            }
        }

        /// <summary>
        /// Gets or sets the name of the template that widget will be displayed.
        /// </summary>
        /// <value>
        /// The name of the template
        /// </value>
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

        #endregion

        #region Actions

        /// <summary>
        /// Gets comments view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(CommentsInputModel commentsInputModel)
        {
            if (commentsInputModel == null || !commentsInputModel.AllowComments.HasValue || commentsInputModel.AllowComments.Value)
            {
                var model = this.Model.GetCommentsListViewModel(commentsInputModel, true);

                if (model != null)
                {
                    return this.View(this.templateNamePrefix + this.TemplateName, model);
                }
            }

            return new EmptyResult();
        }

        /// <summary>
        /// Gets the view for comments count. 
        /// </summary>
        /// <returns></returns>
        public ActionResult Count(CommentsCountInputModel commentsCountInputModel)
        {
            if (!commentsCountInputModel.AllowComments.HasValue || commentsCountInputModel.AllowComments.Value)
            {
                var commentsCountViewModel = this.Model.GetCommentsCountViewModel(commentsCountInputModel);

                if (commentsCountViewModel != null)
                {
                    this.ViewBag.ServiceUrl = RouteHelper.ResolveUrl("~/RestApi/comments-api/", UrlResolveOptions.Rooted);
                    return this.View(this.countTemplateName, commentsCountViewModel);
                }
            }

            return new EmptyResult();
        }

        #endregion

        #region Private fields and constants

        internal const string WidgetIconCssClass = "sfCommentsIcn sfMvcIcn";

        private ICommentsModel model;

        private string templateNamePrefix = "Reviews.";
        private string templateName = "Default";
        private string countTemplateName = "ReviewsCount.Default.Default";

        #endregion
    }
}