using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.RelatedData;

namespace DynamicContent.Mvc.Helpers
{
    public static class RelatedDataExtensions
    {
        public static string GetIdentifierField(this string relatedDataType)
        {
            var identifierField = RelatedDataHelper.GetRelatedTypeIdentifierField(relatedDataType);

            return identifierField;
        }
    }
}
