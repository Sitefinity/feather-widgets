using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SectionHeader;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Helpers
{
    /// <summary>
    /// This class contains helper methods for <see cref="Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SectionHeaderField.HeadingType"/>
    /// </summary>
    public static class SectionHeaderFieldHelpers
    {
        /// <summary>
        /// Builds the section header.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="text">The text.</param>
        /// <param name="headingType">Type of the heading.</param>
        /// <returns></returns>
        public static MvcHtmlString BuildSectionHeader(this HtmlHelper helper, string text, HeadingType headingType)
        {
            var tagBuilder = new TagBuilder(headingType.ToString());
            tagBuilder.SetInnerText(text);
            return MvcHtmlString.Create(tagBuilder.ToString());
        }

        public static Dictionary<string, string> GetHeadingTypeValues(this HtmlHelper helper)
        {
            var dict = new Dictionary<string, string>();
            var allDigits = "1234567890".ToCharArray();

            foreach (HeadingType headingType in Enum.GetValues(typeof(HeadingType)))
            {
                var headingTypeStr = headingType.ToString();

                if (headingType != HeadingType.p)
                {
                    dict.Add(string.Format("Heading {0}", headingTypeStr.Substring(headingTypeStr.IndexOfAny(allDigits))), headingTypeStr);
                }
                else
                {
                    dict.Add("Paragraph", headingTypeStr);
                }
            }

            return dict;
        }
    }
}