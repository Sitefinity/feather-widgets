using Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Registration;

namespace FeatherWidgets.TestUnit.DummyClasses.Identity
{
    /// <summary>
    /// This class is used for testing the Registration controller.
    /// </summary>
    public class DummyRegistrationController : RegistrationController
    {
        public override IRegistrationModel Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = new DummyRegistrationModel();
                }

                return this.model;
            }
        }

        private IRegistrationModel model;
    }
}
