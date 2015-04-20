using System;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Blog post widget.
    /// </summary>
    [ControllerToolboxItem(Name = "BlogPost", Title = "Blog post", SectionName = "MvcWidgets", ModuleName = "Blogs", CssClass = BlogPostController.WidgetIconCssClass)]
    public class BlogPostController: Controller
    {
        internal const string WidgetIconCssClass = "sfBlogsViewIcn";
    }
}
