using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.Lists.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Media.Mvc.Controllers;

namespace FeatherWidgets.TestUnit.DummyClasses.Media.DocumentsList
{
    public class DummyListsController : ListsController
    {
        protected override string GetPageUrl()
        {
            return string.Empty;
        }
    }
}
