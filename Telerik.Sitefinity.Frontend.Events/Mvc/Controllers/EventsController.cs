using System.Web.Mvc;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "Events_MVC", Title = "Events", SectionName = ToolboxesConfig.ContentToolboxSectionName, ModuleName = EventsModule.ModuleName, CssClass = EventsController.WidgetIconCssClass)]
    public class EventsController : Controller
    {
        public ActionResult Index()
        {
            return this.Content("This widget is under construction!");
        }

        private const string WidgetIconCssClass = "sfEventsViewIcn sfMvcIcn";
    }
}
