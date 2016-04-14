using System;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using FeatherWidgets.TestUtilities.CommonOperations.Forms;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Web.Services;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// SetLabelPlaceholderInstructionalTextPredefinedValueToTextboxField arrangement class.
    /// </summary>
    public class SetLabelPlaceholderInstructionalTextPredefinedValueToTextboxField : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            var fields = new FormFieldType[] 
            { 
                FormFieldType.TextField,
                FormFieldType.SubmitButton
            };

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidgets(fields, FormName);

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
        private const string FormName = "NewForm";
        private const string PageTemplateName = "Bootstrap.default";
    }
}
