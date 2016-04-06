using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Models;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Helpers
{
    public static class EventHelper
    {
        public static MvcHtmlString EventDates(this HtmlHelper helper, ItemViewModel eventItem)
        {
            return MvcHtmlString.Create("i am helper");
        }
    }
}
