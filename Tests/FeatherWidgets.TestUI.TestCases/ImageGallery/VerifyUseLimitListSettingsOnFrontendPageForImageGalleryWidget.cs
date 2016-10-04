using System;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.ImageGallery
{
    /// <summary>
    /// VerifyUseLimitListSettingsOnFrontendPageForImageGalleryWidget_ test class.
    /// </summary>
    [TestClass]
    public class VerifyUseLimitListSettingsOnFrontendPageForImageGalleryWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyUseLimitListSettingsOnFrontendPageForImageGalleryWidget
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        TestCategory(FeatherTestCategories.PagesAndContent), 
        TestCategory(FeatherTestCategories.ImageGallery)]
        public void VerifyUseLimitListSettingsOnFrontendPageForImageGalleryWidget()
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
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImageIsPresent(ImageAltText5);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImageIsPresent(ImageAltText4);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImageIsPresent(ImageAltText3);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImageIsNotPresent(ImageAltText2);
            BATFeather.Wrappers().Frontend().ImageGallery().ImageGalleryWrapper().VerifyImageIsNotPresent(ImageAltText1);
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

        private const string PageName = "ImagesPage";
        private const string WidgetName = "Image gallery";
        private const string ImageAltText = "AltText_";
        private const string ImageAltText1 = ImageAltText + ImageTitles1;
        private const string ImageAltText2 = ImageAltText + ImageTitles2;
        private const string ImageAltText3 = ImageAltText + ImageTitles3;
        private const string ImageAltText4 = ImageAltText + ImageTitles4;
        private const string ImageAltText5 = ImageAltText + ImageTitles5;
        private const string ImageTitles1 = "Image1";
        private const string ImageTitles2 = "Image2";
        private const string ImageTitles3 = "Image3";
        private const string ImageTitles4 = "Image4";
        private const string ImageTitles5 = "Image5";
    }
}
