using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.CardWidget
{
    /// <summary>
    /// This is an entry point for card wrappers for the backend.
    /// </summary>
    public class CardWrapperFacade
    {
        /// <summary>
        /// Provides access to CardWrapper
        /// </summary>
        /// <returns>New instance of CardWrapper</returns>
        public CardWrapper CardWrapper()
        {
            return new CardWrapper();
        }
    }
}
