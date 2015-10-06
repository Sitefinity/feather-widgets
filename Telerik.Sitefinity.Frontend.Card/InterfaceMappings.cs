using Ninject.Modules;
using Telerik.Sitefinity.Frontend.Card.Mvc.Models.Card;

namespace Telerik.Sitefinity.Frontend.Card
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
            Bind<ICardModel>().To<CardModel>();
        }
    }
}
