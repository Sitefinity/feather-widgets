using System;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    public class WidgetDecTrackingWrapper : BaseWrapper
    {
        public void SetDecDataToBeChecked(string predicate)
        {
            string jsScript = System.IO.File.ReadAllText(@"..\..\..\..\Feather.Widgets.TestUI.Framework\Framework\SciptHelpers\set-dec-test-data.js");
            ActiveBrowser.Actions.InvokeScript(jsScript);
            ActiveBrowser.Actions.InvokeScript("document.setDecTestData('{0}');".Arrange(predicate));
        }
    }
}
