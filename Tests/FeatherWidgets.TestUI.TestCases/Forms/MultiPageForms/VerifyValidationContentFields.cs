using System;
using System.Collections.Generic;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Forms.MultiPageForms
{
    /// <summary>
    /// VerifyValidationContentFields test class.
    /// </summary>
    [TestClass]
    public class VerifyValidationContentFields_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyValidationContentFields
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void VerifyValidationContentFields()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            foreach (var fieldName in this.fieldNames)
            {
                BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(fieldName);
                BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().CheckRequiredFieldCheckbox();
                BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            }

            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNextStepText();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickNextButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyNextStepText();
            foreach (var fieldName in this.fieldNames)
            {
                BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyRequiredFields(fieldName);
            }
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string FormName = "MultiPageForm";
        private const string PageName = "FormPage";

        private List<string> fieldNames = new List<string>() 
        { 
            "TextFieldController", 
            "MultipleChoiceFieldController", 
            "DropdownListFieldController", 
            "ParagraphTextFieldController", 
            "CheckboxesFieldController" 
        };
    }
}
