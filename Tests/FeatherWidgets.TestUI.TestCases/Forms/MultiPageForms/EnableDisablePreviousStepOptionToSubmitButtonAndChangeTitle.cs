﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Forms.MultiPageForms
{
    /// <summary>
    /// EnableDisablePreviousStepOptionToSubmitButtonAndChangeTitle test class.
    /// </summary>
    [TestClass]
    public class EnableDisablePreviousStepOptionToSubmitButtonAndChangeTitle_ : FeatherTestCase
    {
        /// <summary>
        /// UI test EnableDisablePreviousStepOptionToSubmitButtonAndChangeTitle
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.Forms)]
        public void EnableDisablePreviousStepOptionToSubmitButtonAndChangeTitle()
        {
            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(SubmitButton);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickAllowUsersToStepBackwardCheckBox();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ChangePreviousStepText(PreviousStepNew);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyPreviousStepText(PreviousStepNew);
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
            
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickNextButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyPreviousStepText(PreviousStepNew);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickPreviousButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickNextButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyPreviousStepText(PreviousStepNew);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SelectCheckbox(Choice);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickSubmit();

            BAT.Macros().NavigateTo().Modules().Forms(this.Culture);
            BAT.Wrappers().Backend().Forms().FormsDashboard().OpenFormFromTheGrid(FormName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(SubmitButton);
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickAllowUsersToStepBackwardCheckBox();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyPreviousStepText(PreviousStepNew, false);
            BAT.Wrappers().Backend().Forms().FormsContentScreen().PublishForm();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickNextButton();
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().VerifyPreviousStepText(PreviousStepNew, false);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().SelectCheckbox(Choice);
            BATFeather.Wrappers().Frontend().Forms().FormsWrapper().ClickSubmit();
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
        private const string SubmitButton = "SubmitButtonController";
        private const string PreviousStepOld = "Previous step";
        private const string PreviousStepNew = "Previous page";
        private const string Choice = "Second Choice";
        private const string PageName = "FormPage";
    }
}
