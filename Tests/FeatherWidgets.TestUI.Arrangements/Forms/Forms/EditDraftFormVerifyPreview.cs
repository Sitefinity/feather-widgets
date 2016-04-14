using System;
using System.Collections.Generic;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using FeatherWidgets.TestUtilities.CommonOperations.Forms;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Web.Services;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// EditDraftFormVerifyPreview arrangement class.
    /// </summary>
    public class EditDraftFormVerifyPreview : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
           (new FormsOperations()).CreateFormWithWidgets(new FormFieldType[] { FormFieldType.SubmitButton, FormFieldType.Captcha, FormFieldType.DropdownListField, FormFieldType.ParagraphTextField }, FeatherGlobals.FormName, false);
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