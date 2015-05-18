using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Helpers
{
    internal static class ScriptHelper
    {
        /// <summary>
        /// Gets the short part of the whole embeded code.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <returns></returns>
        public static string GetShortScript(string script)
        {
            if(string.IsNullOrWhiteSpace(script))
            {
                return string.Empty;
            }
            
            var divider = Environment.NewLine;

            var lines = script.Split(new string[2] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            string result = string.Join(divider, lines.Take(2));

            if (lines.Length > 2)
            {
                result = result + divider + "...";
            }

            return result;
        }
    }
}
