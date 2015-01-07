using Ninject.Modules;
using Telerik.Sitefinity.Frontend.SocialShare.Mvc.Models;

namespace Telerik.Sitefinity.Frontend.SocialShare
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
            Bind<ISocialShareModel>().To<SocialShareModel>();
        }
    }
}
