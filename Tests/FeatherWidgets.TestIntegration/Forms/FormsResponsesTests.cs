using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.Services;
using Telerik.Sitefinity.Modules.Forms.Web.UI;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.SDK.DevelopersGuide.SitefinityEssentials.Modules.Forms;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions;
using Telerik.WebTestRunner.Server.Attributes;

namespace FeatherWidgets.TestIntegration.Forms
{
    /// <summary>
    /// This is a class with Forms widget tests.
    /// </summary>
    [Ignore("Ignore until Sitefinity 9.0 is released")]
    [TestFixture]
    public class FormsResponsesTests
    {
        private const string FieldText = "FieldText";
        private const string OtherFieldText = "OtherText";
        private const string NonExistingFieldText = "NonExistingFieldText";

        #region Single field single entry

        /// <summary>
        /// Ensures that when searching for existing text in single field in one entry one result is returned.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures that when searching for existing text in single field in one entry one result is returned.")]
        public void FormResponses_SearchingForExistingTextInSingleFieldInSingleEntry_GetsResults()
        {
            var fields = new string[,] { { FieldText } };
            this.ExecuteResponsesSearchTextTest(fields, FieldText, 1);
        }

        /// <summary>
        /// Ensures that when searching for non present text in single field in one entry no results are returned.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures that when searching for non present text in single field in one entry no results are returned.")]
        public void FormResponses_SearchingForNonExistingTextInSingleFieldInSingleEntry_GetsNoResults()
        {
            var fields = new string[,] { { FieldText } };
            this.ExecuteResponsesSearchTextTest(fields, NonExistingFieldText, 0);
        }

        #endregion

        #region Multi field single entry

        /// <summary>
        /// Ensures that when searching for present text all multiple fields in one entry all results are returned.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures that when searching for present text all multiple fields in one entry 1 result are returned.")]
        public void FormResponses_SearchingForExistingTextInMultipleFieldsInSingleEntry_GetsAllResults()
        {
            var fields = new string[,] { { FieldText, FieldText, FieldText } };
            this.ExecuteResponsesSearchTextTest(fields, FieldText, 1);
        }

        /// <summary>
        /// Ensures that when searching for specific text in multiple fields in one entry only specific results are returned.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures that when searching for specific text in multiple fields in one entry only specific results are returned.")]
        public void FormResponses_SearchingForSpecificTextInMultipleFieldsInSingleEntry_GetsSpecificResults()
        {
            var fields = new string[,] { { FieldText, OtherFieldText, OtherFieldText } };
            this.ExecuteResponsesSearchTextTest(fields, FieldText, 1);
        }

        /// <summary>
        /// Ensures that when searching for non present text in multiple fields in one entry no results are returned.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures that when searching for non present text in multiple fields in one entry no results are returned.")]
        public void FormResponses_SearchingForNonExistingTextInMultipleFieldsInSingleEntry_GetsNoResults()
        {
            var fields = new string[,] { { FieldText, FieldText, FieldText } };
            this.ExecuteResponsesSearchTextTest(fields, NonExistingFieldText, 0);
        }

        #endregion
        
        #region Single field multi entry

        /// <summary>
        /// Ensures that when searching for existing text in single field in multi entry one result is returned.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures that when searching for existing text in single field in multi entry one result is returned.")]
        public void FormResponses_SearchingForExistingTextInSingleFieldInMultyEntry_GetsResults()
        {
            var fields = new string[,] { { FieldText }, { OtherFieldText }, { FieldText } };
            this.ExecuteResponsesSearchTextTest(fields, FieldText, 2);
        }

        /// <summary>
        /// Ensures that when searching for non present text in single field in multi entry no results are returned.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures that when searching for non present text in single field in multi entry no results are returned.")]
        public void FormResponses_SearchingForNonExistingTextInSingleFieldInMultyEntry_GetsNoResults()
        {
            var fields = new string[,] { { FieldText }, { FieldText }, { FieldText } };
            this.ExecuteResponsesSearchTextTest(fields, NonExistingFieldText, 0);
        }

        #endregion

        #region Multi field multi entry

        /// <summary>
        /// Ensures that when searching for present text all multiple fields in multi entry all results are returned.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures that when searching for present text all multiple fields in multi entry 1 result are returned.")]
        public void FormResponses_SearchingForExistingTextInMultipleFieldsInMultyEntry_GetsAllResults()
        {
            var fields = new string[,] { { FieldText, FieldText, FieldText }, { FieldText, FieldText, FieldText }, { FieldText, FieldText, FieldText } };
            this.ExecuteResponsesSearchTextTest(fields, FieldText, 3);
        }

        /// <summary>
        /// Ensures that when searching for specific text in multiple fields in multi entry only specific results are returned.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures that when searching for specific text in multiple fields in multi entry only specific results are returned.")]
        public void FormResponses_SearchingForSpecificTextInMultipleFieldsInMultyEntry_GetsSpecificResults()
        {
            var fields = new string[,] { { FieldText, OtherFieldText, OtherFieldText }, { FieldText, OtherFieldText, OtherFieldText }, { OtherFieldText, OtherFieldText, OtherFieldText } };
            this.ExecuteResponsesSearchTextTest(fields, FieldText, 2);
        }

        /// <summary>
        /// Ensures that when searching for non present text in multiple fields in multi entry no results are returned.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures that when searching for non present text in multiple fields in multi entry no results are returned.")]
        public void FormResponses_SearchingForNonExistingTextInMultipleFieldsInMultyEntry_GetsNoResults()
        {
            var fields = new string[,] { { FieldText, FieldText, FieldText }, { FieldText, FieldText, FieldText }, { FieldText, FieldText, FieldText } };
            this.ExecuteResponsesSearchTextTest(fields, NonExistingFieldText, 0);
        }

        #endregion

        private void ExecuteResponsesSearchTextTest(string[,] responsesFieldsValues, string searchedText, int expectedResultsCount)
        {
            var fieldsPerResponse = responsesFieldsValues.GetLength(1);
            var controls = new MvcWidgetProxy[fieldsPerResponse];
            for (int i = 0; i < controls.Length; i++)
            {
                var control = new MvcWidgetProxy();
                control.ControllerName = typeof(TextFieldController).FullName;

                var controller = new TextFieldController();
                control.Settings = new ControllerSettings(controller);
                controls[i] = control;
            }

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidgets(controls);

            var manager = FormsManager.GetManager();
            var form = manager.GetForm(formId);

            var masterDetailView = new FormsMasterDetailView();
            masterDetailView.FormDescription = form;
            var masterDetailDefinition = new MasterGridViewDefinition();
            typeof(FormsMasterDetailView).GetMethod("ConfigureSearchFields", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(masterDetailView, new object[] { masterDetailDefinition });
            var fields = masterDetailDefinition.SearchFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            Assert.AreEqual(fieldsPerResponse, fields.Length, string.Format(CultureInfo.InvariantCulture, "The produced search fields count ({0}) is not equal to the actual fields count on the form ({1})", fields.Length, fieldsPerResponse));

            var service = new FormsService();
            try
            {
                using (var fluent = App.WorkWith())
                {
                    for (int responseIndex = 0; responseIndex < responsesFieldsValues.GetLength(0); responseIndex++)
                    {
                        var formEntry = fluent.Forms().Form(form.Name).Entry().CreateNew(Guid.NewGuid());

                        for (int fieldValueIndex = 0; fieldValueIndex < responsesFieldsValues.GetLength(1); fieldValueIndex++)
                        {
                            formEntry.SetFieldValue(fields[fieldValueIndex], responsesFieldsValues[responseIndex, fieldValueIndex]);
                        }

                        formEntry.SaveChanges();
                    }
                }

                var filterClauses = fields.Select(f => string.Format(CultureInfo.InvariantCulture, "{0}.ToUpper().Contains(\"{1}\".ToUpper())", f, searchedText));
                var filter = "(" + string.Join(" OR ", filterClauses) + ")";
                var entries = service.GetFormEntries(form.Name, null, "Title ASC", 0, 50, filter, manager.Provider.FormsNamespace + "." + form.Name, null, Guid.Empty);

                Assert.AreEqual(expectedResultsCount, entries.TotalCount, string.Format(CultureInfo.InvariantCulture, "The returned search results ({0}) do not match the expected ones ({1})", entries.TotalCount, expectedResultsCount));
            }
            finally
            {
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }
    }
}
