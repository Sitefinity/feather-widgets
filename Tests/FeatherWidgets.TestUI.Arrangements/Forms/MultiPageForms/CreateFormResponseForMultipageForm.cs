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
    /// CreateFormResponseForMultipageForm arrangement class.
    /// </summary>   
    public class CreateFormResponseForMultipageForm : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            List<FormFieldType> formHeaderControls = new List<FormFieldType>() { FormFieldType.NavigationField };
            List<FormFieldType> formBodyControls = new List<FormFieldType>() { FormFieldType.TextField, FormFieldType.PageBreak, FormFieldType.CheckboxesField, FormFieldType.SubmitButton };
            List<FormFieldType> formFooterControls = new List<FormFieldType>() { };

            ServerOperationsFeather.Forms().CreateFormWithWidgets(formHeaderControls, formBodyControls, formFooterControls, FormName1);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Forms().DeleteAllForms();
        }

        private const string FormName1 = "MultiPageForm1";
    }
}
