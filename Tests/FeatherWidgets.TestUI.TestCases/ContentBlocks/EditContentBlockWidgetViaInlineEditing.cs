using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.WebAii.Controls;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks
{
    /// <summary>
    /// EditContentBlockWidgetViaInlineEditing test class.
    /// </summary>
    [TestClass]
    public class EditContentBlockWidgetViaInlineEditing_ : FeatherTestCase
    {
        /// <summary>
        /// UI test EditContentBlockWidgetViaInlineEditing 
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.ContentBlock)]
        public void EditContentBlockWidgetViaInlineEditing()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(ContentBlockContent);
            BAT.Wrappers().Frontend().InlineEditing().InlineEditingWrapper().OpenPageForEdit();
            BAT.Wrappers().Frontend().InlineEditing().InlineEditingWrapper().VerifyEditIsOn(PageName);
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().EditContentBlock();
            BAT.Wrappers().Frontend().InlineEditing().InlineEditingWrapper().PublishPage();
            BAT.Wrappers().Frontend().InlineEditing().InlineEditingWrapper().VerifyEditIsOff();
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(EditedContentBlockContent);
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

        private const string PageName = "ContentBlock";
        private const string ContentBlockContent = "Test content";
        private const string EditedContentBlockContent = "edited content block";
    }
}
