using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.DynamicWidgets
{
    [TestClass]
    public class EditDynamicWidgetViaInlineEditing_ : FeatherTestCase
    {
        /// <summary>
        /// UI test EditDynamicWidgetViaInlineEditing
        /// The test is ignored, because it is unstable.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.DynamicWidgets),
        Ignore]
        public void EditDynamicWidgetViaInlineEditing()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower() + "/SomeUrlName");
            BAT.Wrappers().Frontend().InlineEditing().InlineEditingWrapper().OpenPageForEdit();
            BAT.Wrappers().Frontend().InlineEditing().InlineEditingWrapper().VerifyEditIsOn(PageName);

            BATFeather.Wrappers().Frontend().ModuleBuilder().InlineEditingWrapper().EditControlOfDynamicContentViaInlineEditing(NewTitle, Title);
            BATFeather.Wrappers().Frontend().ModuleBuilder().InlineEditingWrapper().EditControlOfDynamicContentViaInlineEditing(NewShortText, ShortText);
            BATFeather.Wrappers().Frontend().ModuleBuilder().InlineEditingWrapper().EditDropDownControlOfDynamicContentViaInlineEditing(choices[2], Choices);  
            BATFeather.Wrappers().Frontend().ModuleBuilder().InlineEditingWrapper().EditRadioButtonControlOfDynamicContentViaInlineEditing(choices[0], ChoicesRadioButtons);
            BATFeather.Wrappers().Frontend().ModuleBuilder().InlineEditingWrapper().EditDropDownControlOfDynamicContentViaInlineEditing(choices[0], Choices);
            BATFeather.Wrappers().Frontend().ModuleBuilder().InlineEditingWrapper().EditRadioButtonControlOfDynamicContentViaInlineEditing(choices[1], ChoicesDropDown);            
            BATFeather.Wrappers().Frontend().ModuleBuilder().InlineEditingWrapper().EditYesNoControlOfDynamicContentViaInlineEditing(YesNo);
            BATFeather.Wrappers().Frontend().ModuleBuilder().InlineEditingWrapper().EditDateTimeControlOfDynamicContentViaInlineEditing(DateTime, Date);            
            BATFeather.Wrappers().Frontend().ModuleBuilder().InlineEditingWrapper().EditControlOfDynamicContentViaInlineEditing(NewLongText, LongText);
            BATFeather.Wrappers().Frontend().ModuleBuilder().InlineEditingWrapper().EditCategoryControlOfDynamicContentViaInlineEditing(Category, NewCategory);            
            BATFeather.Wrappers().Frontend().ModuleBuilder().InlineEditingWrapper().EditTagControlOfDynamicContentViaInlineEditing(Tags, NewTags);

            BAT.Wrappers().Frontend().InlineEditing().InlineEditingWrapper().PublishPage();
            BAT.Wrappers().Frontend().InlineEditing().InlineEditingWrapper().VerifyEditIsOff();
            Assert.IsTrue(BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().VerifyDynamicContentPresentOnTheFrontend(this.dynamicContent));
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

        private const string PageName = "TestPage";
        private const string ModuleName = "Module builder";
        private const string Title = "Title";
        private const string NewTitle = "New Title";
        private static string[] choices = { "First Choice", "Second Choice", "Third Choice" };
        private const string Choices = "Choices";
        private const string ChoicesRadioButtons = "ChoicesRadioButtons";
        private const string ChoicesDropDown = "ChoicesDropDown";
        private const string YesNo = "YesNo";
        private const string YesNoValue = "No";
        private const string DateTime = "DateTime";
        private const string Date = "1/5/2015 10:00 PM";
        private const string DateValue = "Jan 5, 2015, 22:00 PM";
        private const string ShortText = "ShortText";
        private const string NewShortText = "New short text";
        private const string LongText = "LongText";
        private const string NewLongText = "New long text";
        private const string Category = "Category";
        private const string NewCategory = "New category(0 items)";
        private const string NewCategoryName = "New category";
        private const string Tags = "Tags";
        private const string NewTags = "New tag";
        private string[] dynamicContent = { NewTitle, NewShortText, choices[2], choices[0], YesNoValue, DateValue, NewLongText, NewCategoryName, NewTags };
    }
}
