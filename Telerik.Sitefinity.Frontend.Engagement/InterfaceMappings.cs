using Ninject.Modules;
using Telerik.Sitefinity.Frontend.Engagement.Mvc.Models.Engagement;

namespace Telerik.Sitefinity.Frontend.Engagement
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
            Bind<IEngagementModel>().To<EngagementModel>();
        }
    }
}
