﻿using Telerik.Sitefinity.Frontend.Search.Mvc.Models;
using Telerik.Sitefinity.Publishing.PublishingPoints;

namespace Telerik.Sitefinity.Frontend.Search.SearchFacets
{
    internal static class SearchFacetExtensions
    {
        /// <summary>
        /// Checks in the facet settings whether it is a value facet.
        /// </summary>
        /// <param name="settings">The facet settings</param>
        /// <returns>Returns true if the facet is of a type value. false if the facet is a range or interval</returns>
        internal static bool IsValueFacet(this FacetFieldSettings settings)
        {
            if (settings.RangeType == SearchFacetExtensions.AutoGeneratedFacet)
            {
                if (settings.FacetType == SearchIndexAdditonalFieldType.DateAndTime.ToString())
                {
                    // UX: When no data step is set - the Date field should be displayed as interval facet with a day interval
                    return false;
                }

                if (settings.FacetType == SearchIndexAdditonalFieldType.NumberWhole.ToString()
                    || settings.FacetType == SearchIndexAdditonalFieldType.NumberDecimal.ToString())
                {
                    // UX: When no number step is set - the Number field should be displayd as a value facet
                    return settings.NumberStep == null;
                }

                return true;
            }

            return false;
        }

        internal const int AutoGeneratedFacet = 0;
    }
}
