using System;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Frontend.Comments.Mvc.Models;
using Telerik.Sitefinity.Frontend.Comments.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Comments.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Ratings widget.
    /// </summary>
    [Localization(typeof(CommentsWidgetResources))]
    [ControllerToolboxItem(Name = "Reviews_MVC", Title = "Reviews", SectionName = ToolboxesConfig.ContentToolboxSectionName, ModuleName = "Comments", CssClass = ReviewsController.WidgetIconCssClass)]
    public class ReviewsController : BaseCommentsRatingsCtrl
    {
        /// <inheritDoc/>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public override Models.ICommentsModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<ICommentsModel>(this.GetType());

                return this.model;
            }
        }
        
        /// <inheritDoc/>
        public override string TemplateName
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

        /// <inheritDoc/>
        [Browsable(false)]
        public override string TemplateNamePrefix
        {
            get
            {
                return this.templateNamePrefix;
            }
            set
            {
                this.templateNamePrefix = value;
            }
        }

        /// <inheritDoc/>
        [Browsable(false)]
        public override string CountTemplateName
        {
            get
            {
                return this.countTemplateName;
            }
            set
            {
                this.countTemplateName = value;
            }
        }

        /// <inheritDoc/>
        [Browsable(false)]
        public override bool UseReviews
        {
            get { return true; }
        }

        #region Private fields and constants

        private ICommentsModel model;

        private string templateName = "Default";
        private string templateNamePrefix = "Reviews.";
        private string countTemplateName = "ReviewsCount.Default";

        internal const string WidgetIconCssClass = "sfCommentsIcn sfMvcIcn";

        #endregion
    }
}