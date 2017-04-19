using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// WriteEditDeleteCancelNoteMvcPageTemplateVersionInRevisionHistoryWindow arrangement class.
    /// </summary>
    public class WriteEditDeleteCancelNoteMvcPageTemplateVersionInRevisionHistoryWindow : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperations.Templates().CreatePureMVCPageTemplate(TemplateTitle);        
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Templates().DeletePageTemplate(TemplateTitle);
        }

        private const string TemplateTitle = "TestLayout";
    }
}
