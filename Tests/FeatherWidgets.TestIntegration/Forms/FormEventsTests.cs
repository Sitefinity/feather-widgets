using System;
using System.Collections.Specialized;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Forms.Events;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.TestIntegration.SDK.DevelopersGuide.SitefinityEssentials.Modules.Forms;

namespace FeatherWidgets.TestIntegration.Forms
{
    /// <summary>
    /// This class contains integration tests of form events.
    /// </summary>
    [TestFixture]
    public class FormEventsTests
    {
        /// <summary>
        /// Sets ups this test fixture.
        /// </summary>
        [FixtureSetUp]
        public void Setup()
        {
            var control = new MvcControllerProxy();
            control.ControllerName = typeof(TextFieldController).FullName;
            var controller = new TextFieldController();
            controller.MetaField.FieldName = FormEventsTests.FieldName;
            controller.MetaField.DefaultValue = "My default text";
            control.Settings = new ControllerSettings(controller);

            FormEventsTests.formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);
        }

        /// <summary>
        /// Tears down this test fixture.
        /// </summary>
        [FixtureTearDown]
        public void Teardown()
        {
            FormsModuleCodeSnippets.DeleteForm(FormEventsTests.formId);
        }

        /// <summary>
        /// Ensures that form events are risen on submittion.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that form events are risen on submittion.")]
        public void Model_TrySubmitForm_RaisesValidating_Saving_Saved()
        {
            var validatingRisen = false;
            var fieldValidatingRisen = false;
            var savingRisen = false;
            var savedRisen = false;

            SitefinityEventHandler<IFormValidatingEvent> validatingHandler = (IFormValidatingEvent @event) =>
            {
                validatingRisen = true;
            };

            SitefinityEventHandler<IFormFieldValidatingEvent> fieldValidatingHandler = (IFormFieldValidatingEvent @event) =>
            {
                fieldValidatingRisen = true;
            };

            SitefinityEventHandler<FormSavingEvent> savingHandler = (FormSavingEvent @event) =>
            {
                savingRisen = true;
            };

            SitefinityEventHandler<FormSavedEvent> savedHandler = (FormSavedEvent @event) =>
            {
                savedRisen = true;
            };

            EventHub.Subscribe<IFormValidatingEvent>(validatingHandler);
            EventHub.Subscribe<IFormFieldValidatingEvent>(fieldValidatingHandler);
            EventHub.Subscribe<FormSavingEvent>(savingHandler);
            EventHub.Subscribe<FormSavedEvent>(savedHandler);

            try
            {
                var model = new FormModel();
                model.FormId = FormEventsTests.formId;

                var values = new NameValueCollection();
                values.Add(FormEventsTests.FieldName, "text");

                model.TrySubmitForm(new System.Web.Mvc.FormCollection(values), null, string.Empty);

                Assert.IsTrue(validatingRisen, "Form Validating event was not risen.");
                Assert.IsTrue(fieldValidatingRisen, "Form Field validating event was not risen.");
                Assert.IsTrue(savingRisen, "Form saving event was not risen.");
                Assert.IsTrue(savedRisen, "Form saved event was not risen.");
            }
            finally
            {
                EventHub.Unsubscribe<IFormValidatingEvent>(validatingHandler);
                EventHub.Unsubscribe<IFormFieldValidatingEvent>(fieldValidatingHandler);
                EventHub.Unsubscribe<FormSavingEvent>(savingHandler);
                EventHub.Unsubscribe<FormSavedEvent>(savedHandler);
            }
        }

        private static Guid formId;
        private const string FieldName = "TestFieldName";
    }
}
