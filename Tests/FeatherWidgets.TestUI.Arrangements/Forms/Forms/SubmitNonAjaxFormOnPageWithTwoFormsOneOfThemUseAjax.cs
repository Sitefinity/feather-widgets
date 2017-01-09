using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatherWidgets.TestUtilities.CommonOperations;
using FeatherWidgets.TestUtilities.CommonOperations.Forms;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// SubmitNonAjaxFormOnPageWithTwoFormsOneOfThemUseAjax arrangement class.
    /// </summary>
    public class SubmitNonAjaxFormOnPageWithTwoFormsOneOfThemUseAjax : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            var formId = (new FormsOperations()).CreateFormWithWidgets(new FormFieldType[] { FormFieldType.TextField, FormFieldType.SubmitButton }, FormName);
            var formId2 = (new FormsOperations()).CreateFormWithWidgets(new FormFieldType[] { FormFieldType.CheckboxesField, FormFieldType.SubmitButton }, FormName2);

            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId, FormName, "Body");
            ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId2, FormName2, "Body");
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
        private const string FormName = "NewForm1";
        private const string FormName2 = "NewForm2";
    }
}
