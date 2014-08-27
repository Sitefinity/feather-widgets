using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers;
using Telerik.Sitefinity.TestUI.Core.Configuration;

namespace Feather.Widgets.TestUI.Framework
{
    /// <summary>
    /// Entry class for the fluent API for Batch Automated Testing.
    /// </summary>
    public static class BATFeather
    {
        /// <summary>
        /// Provides unified access to the Wrappers
        /// </summary>
        /// <param name="url">The url value</param>
        /// <returns>Returns the wrapper facade</returns>
        public static WrappersFacade Wrappers(Telerik.Sitefinity.TestUI.Core.Configuration.ConfigredUrls url = Telerik.Sitefinity.TestUI.Core.Configuration.ConfigredUrls.NotSet)
        {
            BATFeather.ChangeBaseUrl(url);
            return new WrappersFacade();
        }

        /// <summary>
        /// Provides unified access to the ChangeBaseUrl
        /// </summary>
        /// <param name="url">The url value</param>
        private static void ChangeBaseUrl(Telerik.Sitefinity.TestUI.Core.Configuration.ConfigredUrls url)
        {
            var configUrl = ConfigurationHelper.GetConfiurationSettings(url);
            Manager.Current.Settings.Web.SetBaseUrl(configUrl);
        }
    }
}
