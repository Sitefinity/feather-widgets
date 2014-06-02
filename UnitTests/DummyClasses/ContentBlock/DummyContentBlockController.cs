using ContentBlock.Mvc.Controllers;
using ContentBlock.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.DummyClasses.ContentBlock
{
    public class DummyContentBlockController : ContentBlockController
    {
        protected override System.Collections.Generic.IList<Telerik.Sitefinity.Web.UI.WidgetMenuItem> InitializeCommands()
        {
            return null;
        }


    }
}
