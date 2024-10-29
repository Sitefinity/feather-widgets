using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telerik.Sitefinity.Frontend.Search
{
    internal static class NullableParser
    {
        /// <summary>
        /// Try parse a boolean string
        /// </summary>
        /// <param name="valueToParse">The value to be parsed</param>
        /// <returns>The parsed value, null if the parsing fails</returns>
        public static bool? ParseBoolNullable(string valueToParse)
        {
            bool? result = null;
            bool tempVar;
            if (bool.TryParse(valueToParse, out tempVar))
            {
                result = tempVar;
            }

            return result;
        }

        /// <summary>
        /// Try parse an int string
        /// </summary>
        /// <param name="valueToParse">The value to be parsed</param>
        /// <returns>The parsed value, null if the parsing fails</returns>
        public static int? ParseIntNullable(string valueToParse)
        {
            int? result = null;
            int tempVar;
            if (int.TryParse(valueToParse, out tempVar))
            {
                result = tempVar;
            }

            return result;
        }
    }
}
