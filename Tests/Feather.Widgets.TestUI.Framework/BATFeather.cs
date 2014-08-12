using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.TestUI.Core.Configuration;

namespace Feather.Widgets.TestUI.Framework
{
    /// <summary>
    /// Entry class for the fluent API for Batch Automated Testing.
    /// </summary>
    public static class BATFeather
    {
        public static WrappersFacade Wrappers(Telerik.Sitefinity.TestUI.Core.Configuration.ConfigredUrls url = Telerik.Sitefinity.TestUI.Core.Configuration.ConfigredUrls.NotSet)
        {
            BATFeather.ChangeBaseUrl(url);
            return new WrappersFacade();
        }

        private static void ChangeBaseUrl(Telerik.Sitefinity.TestUI.Core.Configuration.ConfigredUrls url)
        {
            var configUrl = ConfigurationHelper.GetConfiurationSettings(url);
            Manager.Current.Settings.Web.SetBaseUrl(configUrl);
        }
    }
}
