using FeatherWidgets.TestUtilities.CommonOperations;
using FeatherWidgets.TestUtilities.CommonOperations.Forms;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// DeletePageBreak arrangement class.
    /// </summary>
    public class DeletePageBreak : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            var formId = ServerOperationsFeather.Forms().CreateFormWithWidgets(new FormFieldType[] { FormFieldType.TextField }, FormName);

            var templateId = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            ServerOperations.Pages().CreatePage(PageName, templateId);
            var pageId = ServerOperations.Pages().GetPageId(PageName);
            ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Forms().DeleteAllForms();
            ServerOperations.Pages().DeleteAllPages();
        }

        private const string PageName = "FormPage";
        private const string FormName = "MultiPageForm";
        private const string PageTemplateName = "Bootstrap.default";
    }
}
