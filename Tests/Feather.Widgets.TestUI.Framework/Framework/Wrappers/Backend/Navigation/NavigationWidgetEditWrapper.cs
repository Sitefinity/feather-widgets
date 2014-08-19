using ArtOfTest.WebAii.Controls.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    public class NavigationWidgetEditWrapper : BaseWrapper
    {
        public void SelectNavigationWidgetDisplayMode(string mode)
        {
            HtmlUnorderedList optionsList = EM.Navigation.NavigationWidgetEditScreen.DislayModeList 
                .AssertIsPresent("Display options list");

            HtmlListItem option = optionsList.AllItems.Where(a => a.InnerText.Contains(mode)).FirstOrDefault()
                .AssertIsPresent("Display option" + mode);

            HtmlInputRadioButton optionButton = option.Find.ByExpression<HtmlInputRadioButton>("tagname=input")
                .AssertIsPresent("Display option radio button");

            optionButton.Click();
        }

        public void SaveChanges()
        {
            HtmlButton saveButton = EM.Navigation.NavigationWidgetEditScreen.SaveChangesButton
            .AssertIsPresent("Save button");
            saveButton.Click();
        }
    }
}
