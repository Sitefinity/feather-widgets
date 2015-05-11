using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
    /// This class represents the controller of the Comments widget.
    /// </summary>
    [Localization(typeof(CommentsWidgetResources))]
    [ControllerToolboxItem(Name = "Comments_MVC", Title = "Comments", SectionName = ToolboxesConfig.ContentToolboxSectionName, ModuleName = "Comments", CssClass = CommentsController.WidgetIconCssClass)]
    public class CommentsController : Controller
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

        #endregion

        #region Actions

        /// <summary>
        /// Gets comments view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(CommentsInputModel commentsInputModel)
        {
            var model = this.Model.GetCommentsListViewModel(commentsInputModel);

            if (model.AllowComments)
            {
                return this.View(this.templateNamePrefix + this.TemplateName, model);
            }

            return new EmptyResult();
        }

        /// <summary>
        /// Gets the view for comments count. 
        /// </summary>
        /// <returns></returns>
        public ActionResult Count(CommentsCountInputModel commentsCountInputModel)
        {
            this.ViewBag.ServiceUrl = RouteHelper.ResolveUrl("/RestApi/comments-api/", UrlResolveOptions.Rooted);
            if (this.Model.AllowComments)
            {
                var commentsCountViewModel = this.Model.GetCommentsCountViewModel(commentsCountInputModel);

                return this.View(this.countTemplateName, commentsCountViewModel);
            }

            return new EmptyResult();
        }

        #endregion

        #region Private fields and constants

        internal const string WidgetIconCssClass = "sfCommentsIcn sfMvcIcn";
        private ICommentsModel model;
        private string templateName = "Default";
        private string templateNamePrefix = "Comments.";
        private string countTemplateName = "Count.Default";

        #endregion
    }
}
