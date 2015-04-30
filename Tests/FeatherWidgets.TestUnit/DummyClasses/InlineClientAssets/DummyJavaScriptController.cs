using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities.DummyClasses.HttpContext;

namespace FeatherWidgets.TestUnit.DummyClasses.InlineClientAssets
{
    public class DummyJavaScriptController : JavaScriptController
    {
        protected override System.Web.HttpContextBase GetHttpContext()
        {
            return new DummyHttpContext();
        }
    }
}
