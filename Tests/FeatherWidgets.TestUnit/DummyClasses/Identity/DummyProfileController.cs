using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Profile;

namespace FeatherWidgets.TestUnit.DummyClasses.Identity
{
    /// <summary>
    /// This class is used to test the Profile controller.
    /// </summary>
    public class DummyProfileController : ProfileController
    {
        public override Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Profile.IProfileModel Model
        {
            get
            {
                return this.model;
            }
        }

        private readonly IProfileModel model = new DummyProfileModel();
    }
}
