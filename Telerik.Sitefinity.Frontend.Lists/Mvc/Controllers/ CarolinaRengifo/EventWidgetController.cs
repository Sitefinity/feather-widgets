using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Modules.DynamicModules;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace SitefinityWebApp.Mvc.Controllers
{
    public class EventWidgetController : Controller
    {
        public ActionResult Index()
        {
            var providerName = "OpenAccessProvider"; // Ajusta según tu configuración
            var dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            var eventType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Events.Event");

            var events = dynamicModuleManager.GetDataItems(eventType)
                .Where(e => e.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live)
                .OrderBy(e => e.GetValue<DateTime>("EventDate")) // Ordenar por fecha
                .Select(e => new
                {
                    Title = e.GetValue<string>("Title"),
                    Date = e.GetValue<DateTime>("EventDate"),
                    Description = e.GetValue<string>("Description"),
                    ImageUrl = e.GetRelatedItems<DynamicContent>("EventImage").FirstOrDefault()?.GetValue<string>("MediaUrl")
                })
                .ToList();

            return View(events);
        }
    }
}
