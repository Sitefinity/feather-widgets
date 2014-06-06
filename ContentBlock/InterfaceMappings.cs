using ContentBlock.Mvc.Models;
using Ninject.Modules;

namespace ContentBlock
{
    /// <summary>
    /// This class is used to describe the bindings which will be used by the Ninject container when resolving classes
    /// </summary>
    public class InterfaceMappings: NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            this.Bind<IContentBlockModel>().To<ContentBlockModel>();
        }
    }
}
