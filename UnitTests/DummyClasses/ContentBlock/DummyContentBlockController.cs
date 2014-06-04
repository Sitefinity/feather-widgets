using ContentBlock.Mvc.Controllers;
using ContentBlock.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Web.UI;

namespace UnitTests.DummyClasses.ContentBlock
{
    public class DummyContentBlockController : ContentBlockController
    {
        public IList<WidgetMenuItem> InitializeCommands()
        {
            return base.InitializeCommands();
        }   
    }
}
