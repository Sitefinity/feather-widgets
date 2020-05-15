using System;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Helpers
{
    /// <summary>
    /// This class contains functionality for working with event items.
    /// </summary>
    internal class EventSchedulerHelper
    {
        public static bool IsRtl()
        {
            var currentCulture = SystemManager.CurrentContext.AppSettings.CurrentCulture;
            var isRtl = (Array.Find(rtlLanguages, s => currentCulture.ToString().StartsWith(s)) != null) || currentCulture.ToString().ToLower().Contains("arab");
            return isRtl;
        }

        private static string[] rtlLanguages = { "ar", "he", "fa", "ku", "ur", "dv", "ps", "ha", "ks", "yi" };
    }
}