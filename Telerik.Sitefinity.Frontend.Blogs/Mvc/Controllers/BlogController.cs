using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Blog widget.
    /// </summary>
    [ControllerToolboxItem(Name = "Blog", Title = "Blog", SectionName = "MvcWidgets", ModuleName = "Blogs", CssClass = BlogController.WidgetIconCssClass)]
    public class BlogController: Controller
    {

        internal const string WidgetIconCssClass = "sfBlogsListViewIcn";
    }
}
