using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.CardWidget
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather card widgets.
    /// </summary>
    public class CardMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public CardMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the card widget backend
        /// </summary>
        public CardEditScreen CardEditScreen
        {
            get
            {
                return new CardEditScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the card widget frontend
        /// </summary>
        public CardFrontend CardFrontend
        {
            get
            {
                return new CardFrontend(this.find);
            }
        }

        private Find find;
    }
}
