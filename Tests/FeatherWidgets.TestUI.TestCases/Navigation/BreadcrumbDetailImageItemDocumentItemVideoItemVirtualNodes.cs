using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.Navigation
{
    /// <summary>
    /// Test class with tests related to BreadcrumbDetailImageItemDocumentItemVideoItemVirtualNodes.
    /// </summary>
    [TestClass]
    public class BreadcrumbDetailImageItemDocumentItemVideoItemVirtualNodes_ : FeatherTestCase
    {
        /// <summary>
        /// UI test BreadcrumbDetailImageItemDocumentItemVideoItemVirtualNodes.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Navigation)]
        public void BreadcrumbDetailImageItemDocumentItemVideoItemVirtualNodes()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", false));
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ModuleBuilder().DynamicWidgetAdvancedSettingsWrapper().ClickAdvancedSettingsButton();
            BATFeather.Wrappers().Backend().ModuleBuilder().DynamicWidgetAdvancedSettingsWrapper().ClickModelButton();
            BATFeather.Wrappers().Backend().Navigation().NavigationWidgetEditWrapper().SetAllowVirtualNodes("True");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().ClickImage(ImageAltText);
            ActiveBrowser.WaitUntilReady();
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyBreadcrumbInFrontend(PageTitle, ImageTitle);

            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().ClickDocument(DocumentTitle);
            ActiveBrowser.WaitUntilReady();
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyBreadcrumbInFrontend(PageTitle, DocumentTitle);

            BATFeather.Wrappers().Frontend().DocumentsList().DocumentsListWrapper().ClickDocument(VideoTitle);
            ActiveBrowser.WaitUntilReady();
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyBreadcrumbInFrontend(PageTitle, VideoTitle);

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

        private const string PageTitle = "TestPageMediaWidgets";
        private const string WidgetName = "Breadcrumb";
        private const string ImageTitle = "TestImage1";
        private const string DocumentTitle = "TestDocument1";
        private const string VideoTitle = "TestVideo1";
        private const string ImageAltText = "AltText_TestImage1";
    }
}
