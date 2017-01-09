using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using FeatherWidgets.TestUtilities.CommonOperations.Forms;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Restriction;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// DeactivateFeatherAndVerifyFormsMvcOption arrangement class.
    /// </summary>
    public class DeactivateFeatherAndVerifyFormsMvcOption : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            var templateId = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            ServerOperations.Pages().CreatePage(PageName, templateId);
            var pageId = ServerOperations.Pages().GetPageId(PageName);
            
            List<FormFieldType> formBodyControls = new List<FormFieldType>() { FormFieldType.TextField, FormFieldType.SubmitButton };

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidgets(formBodyControls, FormName);
            
            ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId, FormName);            
        }

        /// <summary>
        /// Deactivating Feather Module.
        /// </summary>
        [ServerArrangement]
        public void DeactivateModule()
        {
            using (new UnrestrictedModeRegion())
            {
                ServerOperations.StaticModules().DeactivateModule(FeatherModuleName);
            }         
        }

        /// <summary>
        /// Activating Feather Module.
        /// </summary>
        [ServerArrangement]
        public void ActivateModule()
        {
            using (new UnrestrictedModeRegion())
            {
                ServerOperations.StaticModules().ActivateModule(FeatherModuleName);
            }
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeletePage(PageName);
            ServerOperations.Forms().DeleteAllForms();
            using (new UnrestrictedModeRegion())
            {
                ServerOperations.StaticModules().ActivateModule(FeatherModuleName);
            }
        }

        private const string PageName = "FormPage";
        private const string PageTemplateName = "Bootstrap.default";
        private const string FormName = "Form";
        private const string FeatherModuleName = "Feather";
    }
}
