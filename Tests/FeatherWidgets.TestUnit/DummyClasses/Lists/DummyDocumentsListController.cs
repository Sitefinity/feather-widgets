using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.Lists.Mvc.Controllers;

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
