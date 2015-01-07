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
        public override int GetMinSuggestLength()
        {
            return 3;
        }

        protected override string GetCurrentUILanguage()
        {
            return "en";
        }
    }
}
