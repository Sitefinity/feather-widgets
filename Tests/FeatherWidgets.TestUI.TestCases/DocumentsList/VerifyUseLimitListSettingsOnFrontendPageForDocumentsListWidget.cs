using System;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.DocumentsList
{
    /// <summary>
    /// VerifyUseLimitListSettingsOnFrontendPageForDocumentsListWidget_ test class.
    /// </summary>
    [TestClass]
    public class VerifyUseLimitListSettingsOnFrontendPageForDocumentsListWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyUseLimitListSettingsOnFrontendPageForDocumentsListWidget
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        TestCategory(FeatherTestCategories.PagesAndContent), 
        TestCategory(FeatherTestCategories.DocumentsList)]
        public void VerifyUseLimitListSettingsOnFrontendPageForDocumentsListWidget()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", true, null, new HtmlFindExpression("class=~sfMain")));
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("20", "Paging");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("20", "Limit");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectRadioButtonOption(WidgetDesignerRadioButtonIds.UseLimit);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.UseLimit);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ChangePagingOrLimitValue("3", "Limit");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("20", "Paging");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("3", "Limit");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.UseLimit);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("20", "Paging");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("3", "Limit");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().PressCancelButton();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().CommonWrapper().AreTitlesPresentOnThePageFrontend(new string[] { DocumentTitles5, DocumentTitles4, DocumentTitles3 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().CommonWrapper().AreTitlesPresentOnThePageFrontend(new string[] { DocumentTitles2, DocumentTitles1 }));
            BAT.Macros().NavigateTo().Pages(this.Culture);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageName = "DocumentsPage";
        private const string WidgetName = "Documents list";
        private const string DocumentTitles1 = "Document1";
        private const string DocumentTitles2 = "Document2";
        private const string DocumentTitles3 = "Document3";
        private const string DocumentTitles4 = "Document4";
        private const string DocumentTitles5 = "Document5";
    }
}
