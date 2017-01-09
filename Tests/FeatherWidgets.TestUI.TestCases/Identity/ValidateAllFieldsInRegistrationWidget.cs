using System;
using System.Linq;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.Identity
{
    /// <summary>
    /// ValidateAllFieldsInRegistrationWidget_ test class.
    /// </summary>
    [TestClass]
    public class ValidateAllFieldsInRegistrationWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ValidateAllFieldsInRegistrationWidget
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam4),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Registration),
        TestCategory(FeatherTestCategories.Bootstrap),
        Telerik.TestUI.Core.Attributes.KnownIssue(BugId = 206479), Ignore]
        public void ValidateAllFieldsInRegistrationWidget()
        {
            ////Verify all required fields message

            BAT.Macros().NavigateTo().CustomPage("~/" + RegistrationPage.ToLower(), false, this.Culture, new HtmlFindExpression("tagname=button", "InnerText=Register"));
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().RegisterButton();
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().AssertEmptyEmailFieldMessage();
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().AssertEmptyPasswordFieldMessage();

            ////Verify existing user name validation message

            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillEmail(EditorUserEmail);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillPassword(Password);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillRetypePassword(Password);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().RegisterButton();
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().AssertExistingUserNameFieldMessage();

            ////Verify existing email validation message

            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillEmail(EditorUserEmail);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillPassword(Password);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillRetypePassword(Password);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().RegisterButton();
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().AssertExistingEmailAddressMessage();

            ////Verify wrong email format validation message 

            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillEmail(WrongEmail);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillPassword(Password);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillRetypePassword(Password);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().RegisterButton();
            Assert.IsFalse(ActiveBrowser.ContainsText(SuccessfullyRegisteredUserMessage), "Successfully registered message was found on the page");
            
            ////Verify emtpy re-type password validation message

            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillEmail(Email);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillPassword(Password);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillRetypePassword(string.Empty);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().RegisterButton();
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().AssertReTypePasswordFieldMessage();

            ////Verify wrong re-type password validation message

            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillEmail(Email);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillPassword(Password);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillRetypePassword(WrongPassword);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().RegisterButton();
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().AssertReTypePasswordFieldMessage();

            ////Verify short password validation message

            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillEmail(Email);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillPassword(ShortPassword);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().FillRetypePassword(ShortPassword);
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().RegisterButton();
            BATFeather.Wrappers().Frontend().Identity().RegistrationWrapper().AssertShortPasswordFieldMessage();
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string RegistrationPage = "RegistrationPage";
        private const string Email = "user@test.test";
        private const string WrongEmail = "email";
        private const string EditorUserEmail = "editor@test.test";
        private const string Password = "password";
        private const string WrongPassword = "password1";
        private const string ShortPassword = "pass";
        private const string SuccessfullyRegisteredUserMessage = "Thank you!You are successfully registered.";
    }
}
