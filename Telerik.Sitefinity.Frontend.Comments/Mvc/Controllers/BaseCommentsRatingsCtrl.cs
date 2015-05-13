using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Comments.Mvc.Models;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Comments.Mvc.Controllers
{
    /// <summary>
    /// This class represents the base controller of the Comments and Ratings widget.
    /// </summary>
    public abstract class BaseCommentsRatingsCtrl : Controller
    {
        #region Properties

        /// <summary>
        /// Gets the Comments widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public abstract ICommentsModel Model { get; }

        /// <summary>
        /// Gets or sets the name of the template.
        /// </summary>
        /// <value>
        /// The name of the template.
        /// </value>
        public abstract string TemplateName { get; set; }
        
        /// <summary>
        /// Gets or sets the template name prefix.
        /// </summary>
        /// <value>
        /// The template name prefix.
        /// </value>
        [Browsable(false)]
        public abstract string TemplateNamePrefix { get; set; }

        /// <summary>
        /// Gets or sets the name of the count template.
        /// </summary>
        /// <value>
        /// The name of the count template.
        /// </value>
        [Browsable(false)]
        public abstract string CountTemplateName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use reviews].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use reviews]; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public abstract bool UseReviews { get; }

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
                var model = this.Model.GetCommentsListViewModel(commentsInputModel, this.UseReviews);

                if (model != null)
                {
                    return this.View(this.TemplateNamePrefix + this.TemplateName, model);
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
                var commentsCountViewModel = this.Model.GetCommentsCountViewModel(commentsCountInputModel, this.UseReviews);

                if (commentsCountViewModel != null)
                {
                    this.ViewBag.ServiceUrl = RouteHelper.ResolveUrl("~/RestApi/comments-api/", UrlResolveOptions.Rooted);
                    return this.View(this.CountTemplateName, commentsCountViewModel);
                }
            }

            return new EmptyResult();
        }

        #endregion
    }
}
