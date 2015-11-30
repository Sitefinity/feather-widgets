using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Forms
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather Forms widget back-end screens.
    /// </summary>
    public class FormsMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormsMap" /> class.
        /// </summary>        
        public FormsMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the Forms widget frontend
        /// </summary>
        public FormsFrontend FormsFrontend
        {
            get
            {
                return new FormsFrontend(this.find);
            }
        }

        /// <summary>
        /// Gets the Forms widget backend
        /// </summary>
        public FormsBackend FormsBackend
        {
            get
            {
                return new FormsBackend(this.find);
            }
        }
       private Find find;
    }
}
