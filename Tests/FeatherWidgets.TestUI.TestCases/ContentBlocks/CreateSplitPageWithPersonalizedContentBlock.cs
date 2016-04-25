using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks
{
    /// <summary>
    /// CreateSplitPageWithPersonalizedContentBlock test class.
    /// </summary>
    [TestClass]
    public class CreateSplitPageWithPersonalizedContentBlock_ : FeatherTestCase
    {
        /// <summary>
        /// UI test CreateSplitPageWithPersonalizedContentBlock
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void CreateSplitPageWithPersonalizedContentBlock()
        {
            if (this.Culture != null)
            {
                BAT.Macros().NavigateTo().Pages();
                BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
                BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
                BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().FillContentToContentBlockWidget(NotPersonalizedWidgetContent);
                BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
                BAT.Wrappers().Backend().Personalization().PersonalizationInPages().ExpandMoreInWidgetByIndex();
                BAT.Wrappers().Backend().Personalization().PersonalizationInPages().AssertAddPersonalizedVersionLink();
                BAT.Wrappers().Backend().Personalization().PersonalizationInPages().SelectAddPersonalizedVersionLink();
                BATFeather.Wrappers().Backend().Card().CardWrapper().ClickAddButton();
                BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().DeleteAllContentInEditableArea();
                BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().FillContentToContentBlockWidget(PersonalizedWidgetContent);
                BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
                BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

                BAT.Wrappers().Backend().Pages().PagesWrapper().AddPageTranslationForSpecificPageML(PageName);
                BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().SetPageTitle(PageNameBG);
                BAT.Wrappers().Backend().Pages().PagesWrapper().ClickCreateAndGoToAddContentButton();
                BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().ClickCopyFromAnotherLanguageLinkML();
                BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().ClickDoneFromCopyAnotherLangScreenML();
                BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

                BAT.Macros().NavigateTo().CustomPage("~/" + PageNameBG.ToLower(), false, this.Culture);            
                BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(PersonalizedWidgetContent);
                Assert.IsTrue(BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent().InnerText.Contains(PostTitle), "Blog post is not presented");
                BAT.Macros().User().LogOut();
                BAT.Macros().NavigateTo().CustomPage("~/" + PageNameBG.ToLower(), false, this.Culture);
                Assert.IsTrue(BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent().InnerText.Contains(PostTitle), "Blog post is not presented");
                BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(NotPersonalizedWidgetContent);
                BAT.Macros().User().EnsureAdminLoggedIn();
            }            
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            if (this.Culture != null)
            {
                BAT.Arrange(this.TestName).ExecuteSetUp();
            }
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            if (this.Culture != null)
            {
                BAT.Arrange(this.TestName).ExecuteTearDown();
            }
        }

        private const string PageName = "ContentBlockEN";
        private const string PageNameBG = "ContentBlockBG";
        private const string WidgetName = "ContentBlock";
        private const string NotPersonalizedWidgetContent = "Not personalized widget";
        private const string PersonalizedWidgetContent = "Personalized widget";
        private const string SegmentName = "Test segment";
        private const string PostTitle = "post1";
        private readonly string expectedUrl = string.Format("/DetailPage/TestBlog/{0}/{1:00}/{2:00}/post1", DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
    }
}
