using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.Packaging.StaticContent
{
    /// <summary>
    /// Import edited widget template for news structure
    /// </summary>
    [TestClass]
    public class ImportEditedWidgetTemplateNewsStructure_ : FeatherTestCase
    {
        /// <summary>
        /// Import edited widget template for news structure
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Packaging)]
        public void ImportEditedWidgetTemplateNewsStructure()
        {
            BAT.Macros().NavigateTo().Design().WidgetTemplates();
            this.VerifyWidgetTemplates(this.widgetTemplatesNames, EditedWidgetTemplate, AreaName);
        }

        /// <summary>
        /// Forces calling initialize methods that will prepare test with data and resources. This method must be overridden if you want
        /// in your test case.
        /// </summary>
        protected override void ServerSetup()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(200000, () => BAT.Macros().User().EnsureAdminLoggedIn());
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Forces cleanup of the test data. This method is thrown if test setup fails. This method must be overridden in your test case.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        /// <summary>
        /// Verifies the widget templates.
        /// </summary>
        /// <param name="widgetTemplatesName">Name of the widget templates.</param>
        /// <param name="content">The content.</param>
        /// <param name="areaName">Name of the area.</param>
        private void VerifyWidgetTemplates(string[] widgetTemplatesName, string content, string areaName)
        {
            for (int i = 0; i < widgetTemplatesName.Length; i++)
            {
                BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().OpenWidgetTemplateByAreaAndName(widgetTemplatesName[i], areaName);
                BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().VerifyWidgetTemplateContent(content);
                BAT.Wrappers().Backend().ModuleBuilder().ContentTypePageActionsWrapper().ClickSaveChangesLink();
            }  
        }

        private const string EditedWidgetTemplate = "EDITED";
        private const string AreaName = @"News:";
        private string[] widgetTemplatesNames = new string[] 
                                                   { 
                                                        "List.NewsListNew", "Detail.DetailPageNewNews"
                                                    };
    }
}
