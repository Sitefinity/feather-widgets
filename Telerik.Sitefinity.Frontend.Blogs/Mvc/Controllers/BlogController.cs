using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.Models;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Blog widget.
    /// </summary>
    [ControllerToolboxItem(Name = "Blog", Title = "Blog", SectionName = "MvcWidgets", ModuleName = "Blogs", CssClass = BlogController.WidgetIconCssClass)]
    public class BlogController: Controller
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

        #endregion

        #region Actions

        #endregion

        #region Private fields and constants

        internal const string WidgetIconCssClass = "sfBlogsListViewIcn";
        private IBlogModel model;

        #endregion
    }
}
