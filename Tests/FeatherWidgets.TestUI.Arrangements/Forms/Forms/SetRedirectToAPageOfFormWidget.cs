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
    /// SetRedirectToAPageOfFormWidget arrangement class.
    /// </summary>
    public class SetRedirectToAPageOfFormWidget : TestArrangementBase
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

            Guid page1Id = ServerOperations.Pages().CreatePage(PageNameRedirect);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(page1Id, Content);
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
        private const string PageNameRedirect = "RedirectPage";
        private const string Content = "Successfully submitted form. Thank you!";
        private const string FormName = "NewForm";
        private const string PageTemplateName = "Bootstrap.default";
    }
}
