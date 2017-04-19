using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// DeleteDraftAndPublishVersionInDetailRevisionHistoryPageOnMvcPageTemplate arrangement class.
    /// </summary>
    public class DeleteDraftAndPublishVersionInDetailRevisionHistoryPageOnMvcPageTemplate : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            var templateId = ServerOperations.Templates().CreatePureMVCPageTemplate(TemplateTitle);
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Templates().DeletePageTemplate(TemplateTitle);
        }

        private const string TemplateTitle = "TestLayout";
        private const string PageName = "TestPage";
    }
}
