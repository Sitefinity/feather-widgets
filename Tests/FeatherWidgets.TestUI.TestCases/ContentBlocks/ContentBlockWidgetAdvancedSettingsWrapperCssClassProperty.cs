using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Framework.Wrappers.Backend.PageTemplates;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks
{
    /// <summary>
    /// ContentBlockWidgetAdvancedSettingsWrapperCssClassProperty test class.
    /// </summary>
    [TestClass]
    public class ContentBlockWidgetAdvancedSettingsWrapperCssClassProperty_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ContentBlockWidgetAdvancedSettingsWrapperCssClassProperty
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.ContentBlock2)]
        public void ContentBlockWidgetAdvancedSettingsWrapperCssClassProperty()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().User().EnsureAdminLoggedIn());
            //Verify added Css class in Content Block widget in Page with Hybrid template
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyApplyCssClass(CssClassesToApply);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(ActiveBrowser.ContainsText(CssClassesToApply), "Css class was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(ContentBlockText), "Text was not found on the page");
            //Verify change css class in Content Block widget in Page with Hybrid template
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().ChangeCssClassInAdvancedSettings(CssClassesToApply, CssClassesChange);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(ActiveBrowser.ContainsText(CssClassesChange), "Css class was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(ContentBlockText), "Text was not found on the page");

            //Verify added Css class in Content Block widget in Page with MVC template
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().VerifyApplyCssClass(CssClassesToApply);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            Assert.IsTrue(ActiveBrowser.ContainsText(CssClassesToApply), "Css class was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(ContentBlockText1), "Text was not found on the page");
            //Verify change css class in Content Block widget in Page with MVC template
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().ChangeCssClassInAdvancedSettings(CssClassesToApply, CssClassesChange);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            Assert.IsTrue(ActiveBrowser.ContainsText(CssClassesChange), "Css class was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(ContentBlockText1), "Text was not found on the page");
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// 
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageName = "TestPageHybrid";
        private const string ContentBlockText = "TestContent";
        private const string ContentBlockText1 = "TestContent1";
        private const string WidgetName = "ContentBlock";
        private const string PageTitle = "FeatherBootstrapPage";
        private const string CssClassesToApply = "Css to apply";
        private const string CssClassesChange = "new Css class";
    }
}
