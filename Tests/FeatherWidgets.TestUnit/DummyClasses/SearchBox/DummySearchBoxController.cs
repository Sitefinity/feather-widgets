using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.Search.Mvc.Controllers;

namespace FeatherWidgets.TestUnit.DummyClasses.SearchBox
{
    public class DummySearchBoxController : SearchBoxController
    {
        protected override int GetMinSuggestLength()
        {
            return 3;
        }

        protected override string GetCurrentUILanguage()
        {
            return "en";
        }

        protected override bool IsSearchModuleActivated()
        {
            return true;
        }
    }
}
