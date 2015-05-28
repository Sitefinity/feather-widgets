using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Helpers
{
    internal static class EmbedCodeHelper
    {
        /// <summary>
        /// Gets the short part of the whole embeded code.
        /// </summary>
        /// <param name="code">The embeded code.</param>
        /// <returns></returns>
        public static string GetShortEmbededCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return string.Empty;
            }
            
            var divider = Environment.NewLine;

            var lines = code.Split(new string[2] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            string result = string.Join(divider, lines.Take(2));

            if (lines.Length > 2)
            {
                result = result + divider + "...";
            }

            return result;
        }
    }
}
