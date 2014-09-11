using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// SystemContext Arrangement
    /// </summary>
    public class SystemContext
    {
        /// <summary>
        /// Determines whether the system context is in multisite mode.
        /// </summary>
        [ServerArrangement]
        public void IsMultisiteMode()
        {
            var isMultisiteMode = ServerOperations.MultiSite().CheckIsMultisiteMode();

            ServerArrangementContext.GetCurrent().Values.Add("isMultisiteMode", isMultisiteMode.ToString());
        }
    }
}
