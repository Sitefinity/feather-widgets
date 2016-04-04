using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatherWidgets.TestUtilities.CommonOperations;
using FeatherWidgets.TestUtilities.CommonOperations.Forms;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// ChangePageBreakTemplate arrangement class.
    /// </summary>
    public class ChangePageBreakTemplate : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            string filePath = FileInjectHelper.GetDestinationFilePath(this.viewPath);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            Stream destination = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            var assembly = ServerOperationsFeather.Pages().GetTestUtilitiesAssembly();
            Stream source = assembly.GetManifestResourceStream(FileResource);
            FileInjectHelper.CopyStream(source, destination);
            source.Close();
            destination.Close();

            List<FormFieldType> formHeaderControls = new List<FormFieldType>() { };
            List<FormFieldType> formBodyControls = new List<FormFieldType>() { FormFieldType.TextField, FormFieldType.PageBreak, FormFieldType.MultipleChoiceField, FormFieldType.SubmitButton };
            List<FormFieldType> formFooterControls = new List<FormFieldType>() { };

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidgets(formHeaderControls, formBodyControls, formFooterControls, FormName);

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
            ServerOperations.Widgets().DeleteWidgetTemplate(TemplateName);
        }

        private const string PageName = "FormPage";
        private const string FormName = "MultiPageForm";
        private const string PageTemplateName = "Bootstrap.default";
        private const string TemplateName = "Read.DefaultNew";
        private const string FileResource = "FeatherWidgets.TestUtilities.Data.Forms.Read.DefaultFile.cshtml";
        private string viewPath = Path.Combine("Mvc", "Views", "PageBreak", ViewName);
        private const string ViewName = "Read.DefaultFile.cshtml";       
    }
}
