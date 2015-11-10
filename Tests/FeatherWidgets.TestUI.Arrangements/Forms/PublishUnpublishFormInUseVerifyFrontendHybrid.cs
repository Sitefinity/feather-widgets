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
    /// PublishUnpublishFormInUseVerifyFrontendHybrid arrangement class.
    /// </summary>
    public class PublishUnpublishFormInUseVerifyFrontendHybrid : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            var formId = (new FormsOperations()).CreateFormWithWidgets(new FormFieldType[] { FormFieldType.SubmitButton }, FeatherGlobals.FormName);
            var pageId = ServerOperations.Pages().CreatePage(FeatherGlobals.HybridPageName);
            ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId, FeatherGlobals.FormName, "Body");
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