using System;
using System.Linq;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.Publishing.Helpers
{
    public static class PublishingWidgetExtensions
    {
        /// <summary>
        /// Determines whether the publishing module is activated.
        /// </summary>
        /// <returns></returns>
        public static bool IsModuleActivated()
        {
            var module = SystemManager.GetModule(PublishingModule.ModuleName);

            return module != null;
        }
    }
}
