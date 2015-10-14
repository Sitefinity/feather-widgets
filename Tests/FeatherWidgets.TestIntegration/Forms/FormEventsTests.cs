using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.SDK.DevelopersGuide.SitefinityEssentials.Modules.Forms;
using System.Collections.Specialized;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Modules.Forms.Events;
using Telerik.Sitefinity.Services.Events;

namespace FeatherWidgets.TestIntegration.Forms
{
    [TestFixture]
    public class FormEventsTests
    {
        [FixtureSetUp]
        public void Setup()
        {
            var control = new MvcControllerProxy();
            control.ControllerName = typeof(TextFieldController).FullName;
            var controller = new TextFieldController();
            controller.MetaField.FieldName = FormEventsTests.FieldName;
            controller.MetaField.DefaultValue = "My default text";
            control.Settings = new ControllerSettings(controller);

            this.formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);
        }

        [FixtureTearDown]
        public void Teardown()
        {
            FormsModuleCodeSnippets.DeleteForm(this.formId);
        }

        [Test]
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
                model.FormId = this.formId;

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

        private Guid formId;
        private const string FieldName = "TestFieldName";
    }
}
