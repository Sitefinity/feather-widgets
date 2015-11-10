using FeatherWidgets.TestUtilities.CommonOperations;
using FeatherWidgets.TestUtilities.CommonOperations.Forms;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// PublishUnpublishFormInUseVerifyFrontend arrangement class.
    /// </summary>
    public class PublishUnpublishFormInUseVerifyFrontend : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            var templateId = ServerOperations.Templates().GetTemplateIdByTitle(FeatherGlobals.PageTemplateName);
            var formId = (new FormsOperations()).CreateFormWithWidgets(new FormFieldType[] { FormFieldType.SubmitButton }, FeatherGlobals.FormName);
            ServerOperations.Pages().CreatePage(FeatherGlobals.BootstrapPageName, templateId);
            var pageId = ServerOperations.Pages().GetPageId(FeatherGlobals.BootstrapPageName);
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
    }
}