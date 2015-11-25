using System.Collections.Generic;
using FeatherWidgets.TestUtilities.CommonOperations;
using FeatherWidgets.TestUtilities.CommonOperations.Forms;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// AddFormNavigationField arrangement class.
    /// </summary>
    public class AddFormNavigationField : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            List<FormFieldType> formHeaderControls = new List<FormFieldType>() { };
            List<FormFieldType> formBodyControls = new List<FormFieldType>() { FormFieldType.TextField, FormFieldType.PageBreak, FormFieldType.TextField, FormFieldType.SubmitButton };
            List<FormFieldType> formFooterControls = new List<FormFieldType>() { };

            var formId = (new FormsOperations()).CreateFormWithWidgets(formHeaderControls, formBodyControls, formFooterControls, FormName);

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
