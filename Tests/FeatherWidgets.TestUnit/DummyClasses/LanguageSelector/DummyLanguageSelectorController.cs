using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities.DummyClasses.HttpContext;

namespace FeatherWidgets.TestUnit.DummyClasses.LanguageSelector
{
    /// <summary>
    /// This class creates dummy <see cref="Navigation.Mvc.Controllers.LanguageSelectorController"/>
    /// </summary>
    public class DummyLanguageSelectorController : LanguageSelectorController
    {
        protected override System.Web.HttpContextBase CurrentHttpContext
        {
            get
            {
                return new DummyHttpContext();
            }
        }
    }
}
