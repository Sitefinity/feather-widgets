using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.ScriptsAndStyles
{
    /// <summary>
    /// This class contains references to the elements contained in the Feather css widget back-end screens.
    /// </summary>
    public class ScriptsAndStylesMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptsAndStylesMap" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ScriptsAndStylesMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the scripts and styles widgets edit screen.
        /// </summary>
        /// <value>The scripts and styles widgets edit screen.</value>
        public ScriptsAndStylesEditScreen ScriptsAndStylesEditScreen
        {
            get
            {
                return new ScriptsAndStylesEditScreen(this.find);
            }
        }

        /// <summary>
        /// Gets the java script edit screen.
        /// </summary>
        /// <value>The java script edit screen.</value>
        public JavaScriptEditScreen JavaScriptEditScreen
        {
            get
            {
                return new JavaScriptEditScreen(this.find);
            }
        }
    
        private Find find;
    }
}