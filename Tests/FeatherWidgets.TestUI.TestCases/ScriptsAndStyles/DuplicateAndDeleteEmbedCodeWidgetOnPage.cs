using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.ScriptsAndStyles
{
    /// <summary>
    /// DuplicateAndDeleteEmbedCodeWidgetOnPage test class.
    /// </summary>
    [TestClass]
    public class DuplicateAndDeleteEmbedCodeWidgetOnPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DuplicateAndDeleteEmbedCodeWidgetOnPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ScriptsAndStyles)]
        public void DuplicateAndDeleteEmbedCodeWidgetOnPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().FillCodeInEditableArea(Script);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ScriptAndStyles().JavaScriptWidgetEditWrapper().FillCodeInEditableArea(SecondScript);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            this.VerifyCodeExistOnTheFrontend(Script, true);
            this.VerifyCodeExistOnTheFrontend(SecondScript);

            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName1);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            this.VerifyCodeExistOnTheFrontend(SecondScript);
            this.VerifyCodeDoesNotExistOnTheFrontend(Script, true);
        }

        /// <summary>
        /// Verify code exists on the frontend
        /// </summary>
        public void VerifyCodeExistOnTheFrontend(string code, bool isVideo = false)
        {
            if (isVideo)
            {
                BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().VerifyYouTubeVideo(Src);
            }
            else
            {
                bool isContainedFirst = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(code);
                Assert.IsTrue(isContainedFirst, string.Concat("Expected ", code, " but the code is not found"));
            }
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        /// 
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(TestArrangement).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(TestArrangement).ExecuteTearDown();
        }

        private void VerifyCodeDoesNotExistOnTheFrontend(string code, bool isVideo = false)
        {
            if (isVideo)
            {
                BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().VerifyYouTubeVideo(Src, false);
            }
            else
            {
                bool isContainedSecond = BATFeather.Wrappers().Frontend().ScriptsAndStyles().ScriptsAndStylesWrapper().IsCodePresentOnFrontend(code);
                Assert.IsFalse(isContainedSecond, string.Concat("Expected not", code, " but the code is found"));
            }
        }

        private const string TestArrangement = "WriteAndEditCssInEmbedCodeWidget";
        private const string PageName = "PageWithEmbedCodeWidget";
        private const string WidgetName = "Embed code";
        private const string Script = "<iframe width=\"420\" height=\"315\" src=\"https://www.youtube.com/embed/VK6vxHAmXGA\" frameborder=\"0\" allowfullscreen></iframe>";
        private const string SecondScript = "<script type=\"text/javascript\">var a = 5;</script>";
        private const string OperationName = "Duplicate";
        private const string OperationName1 = "Delete";
        private const string Src = "https://www.youtube.com/embed/VK6vxHAmXGA";
    }
}
