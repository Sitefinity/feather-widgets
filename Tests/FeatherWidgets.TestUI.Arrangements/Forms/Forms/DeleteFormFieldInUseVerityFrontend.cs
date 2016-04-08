using System;
using System.Collections.Generic;
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
    /// DeleteFormFieldInUseVerityFrontend arrangement class.
    /// </summary>
    public class DeleteFormFieldInUseVerityFrontend : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            var templateId = ServerOperations.Templates().GetTemplateIdByTitle(FeatherGlobals.PageTemplateName);
            var formId = (new FormsOperations()).CreateFormWithWidgets(new FormFieldType[] { FormFieldType.SubmitButton, FormFieldType.CheckboxesField, FormFieldType.DropdownListField }, FeatherGlobals.FormName);
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