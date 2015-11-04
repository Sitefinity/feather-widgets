using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Mvc.Proxy;

namespace FeatherWidgets.TestIntegration.Forms
{
    /// <summary>
    /// This class contains integration tests of form backend configurator.
    /// </summary>
    [TestFixture]
    public class BackendConfiguratorTests
    {
        /// <summary>
        /// Ensures that checkboxes field backend cofigurator returns textbox when HasOtherChoice is enabled.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that checkboxes field backend cofigurator returns textbox when HasOtherChoice is enabled.")]
        public void CheckboxesConfigure_WithOtherChoice_RenderTextBox()
        {
            var fieldTitle = "Checkboxes1";
            var formControl = new MvcControllerProxy();
            formControl.ControllerName = typeof(CheckboxesFieldController).FullName;
            var controller = new CheckboxesFieldController();
            
            controller.Model.HasOtherChoice = true;
            controller.Model.MetaField = new MetaField();
            controller.Model.MetaField.Title = fieldTitle;
            formControl.Settings = new ControllerSettings(controller);
            ((CheckboxesFieldController)formControl.Controller).Model.HasOtherChoice = true;
            ((CheckboxesFieldController)formControl.Controller).Model.MetaField = new MetaField();
            ((CheckboxesFieldController)formControl.Controller).Model.MetaField.Title = fieldTitle;

            var configurator = new BackendFieldFallbackConfigurator();
            var backendControl = configurator.ConfigureFormControl(formControl, Guid.NewGuid());

            Assert.AreEqual(typeof(FormTextBox), backendControl.GetType(), "Backend control is not with correct type!");
            Assert.AreEqual(fieldTitle, ((FormTextBox)backendControl).Title, "The title of the field is not correctly set!");
        }

        /// <summary>
        /// Ensures that checkboxes field backend cofigurator returns checkboxes when HasOtherChoice is disabled.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that checkboxes field backend cofigurator returns checkboxes when HasOtherChoice is disabled.")]
        public void CheckboxesConfigure_WithoutOtherChoice_RenderCheckboxes()
        {
            var fieldTitle = "Checkboxes2";
            var formControl = new MvcControllerProxy();
            formControl.ControllerName = typeof(CheckboxesFieldController).FullName;
            var controller = new CheckboxesFieldController();

            controller.Model.HasOtherChoice = false;
            controller.Model.MetaField = new MetaField();
            controller.Model.MetaField.Title = fieldTitle;
            formControl.Settings = new ControllerSettings(controller);
            ((CheckboxesFieldController)formControl.Controller).Model.HasOtherChoice = false;
            ((CheckboxesFieldController)formControl.Controller).Model.MetaField = new MetaField();
            ((CheckboxesFieldController)formControl.Controller).Model.MetaField.Title = fieldTitle;

            var configurator = new BackendFieldFallbackConfigurator();
            var backendControl = configurator.ConfigureFormControl(formControl, Guid.NewGuid());

            Assert.AreEqual(typeof(FormCheckboxes), backendControl.GetType(), "Backend control is not with correct type!");
            Assert.AreEqual(fieldTitle, ((FormCheckboxes)backendControl).Title, "The title of the field is not correctly set!");
        }
    }
}
