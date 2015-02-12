using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers
{
    /// <summary>
    /// This is the entry point class for base wrappers.
    /// </summary>
    public class BaseWrapper
    {
        /// <summary>
        /// Gets the ActiveBrowser object
        /// </summary>
        public Browser ActiveBrowser
        {
            get
            {
                return Manager.Current.ActiveBrowser;
            }
        }

        /// <summary>
        /// Gets the FeatherElementMap object
        /// </summary>
        public virtual FeatherElementMap EM
        {
            get
            {
                return new FeatherElementMap(this.ActiveBrowser.Find);
            }
        }

        /// <summary>
        /// Gets the Log object
        /// </summary>
        public Log Log
        {
            get
            {
                return Manager.Current.Log;
            }
        }

        /// <summary>
        /// Gets the Manager object.
        /// </summary>
        public Manager Manager
        {
            get
            {
                return Manager.Current;
            }
        }
    }
}
