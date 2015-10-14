using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.SDK.DevelopersGuide.SitefinityEssentials.Modules.Forms;

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
        public void Model_TrySubmitForm_RaisesValidating_DoesNotContinueOnHandlerException()
        {
            var model = new FormModel();
            model.FormId = this.formId;
        }

        private Guid formId;
    }
}
