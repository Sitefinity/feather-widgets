using Ninject.Modules;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.AccountActivation;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.ChangePassword;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Registration;

namespace Telerik.Sitefinity.Frontend.Media
{
    /// <summary>
    /// This class is used to describe the bindings which will be used by the Ninject container when resolving classes
    /// </summary>
    public class InterfaceMappings : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<ILoginStatusModel>().To<LoginStatusModel>();
            Bind<IRegistrationModel>().To<RegistrationModel>();
            Bind<ILoginFormModel>().To<LoginFormModel>();
            Bind<IChangePasswordModel>().To<ChangePasswordModel>();
            Bind<IAccountActivationModel>().To<AccountActivationModel>();
        }
    }
}
