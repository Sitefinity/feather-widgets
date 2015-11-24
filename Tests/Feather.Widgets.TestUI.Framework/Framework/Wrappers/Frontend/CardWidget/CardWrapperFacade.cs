using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend.CardWidget
{
    /// <summary>
    /// This is an entry point for card wrappers for the frontend.
    /// </summary>
    public class CardWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the CardWrapper
        /// </summary>
        /// <returns>Returns the CardWrapper</returns>
        public CardWrapper CardWrapper()
        {
            return new CardWrapper();
        }
    }
}
