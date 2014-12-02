using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Locations.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace DynamicContent.TemplateGeneration.Fields.Helpers
{
    /// <summary>
    /// This class contains helper methods for working with front-end fields.
    /// </summary>
    public static class FieldExtensions
    {
        #region Address field

        /// <summary>
        /// Gets the API key.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "helper"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Api")]
        public static string GetApiKey()
        {
            var apiKey = Telerik.Sitefinity.Configuration.Config.Get<Telerik.Sitefinity.Services.SystemConfig>().GeoLocationSettings.GoogleMapApiV3Key;

            return apiKey;
        }

        /// <summary>
        /// Determines whether the API key valid.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "helper"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Api")]
        public static bool IsApiKeyValid()
        {
            var apiKey = Telerik.Sitefinity.Configuration.Config.Get<Telerik.Sitefinity.Services.SystemConfig>().GeoLocationSettings.GoogleMapApiV3Key;
            var isValid = !apiKey.IsNullOrEmpty();

            return isValid;
        }

        #endregion

        /// <summary>
        /// Gets the type of the taxonomy.
        /// </summary>
        /// <param name="classificationId">The classification identifier.</param>
        /// <returns></returns>
        /// <value>
        /// The type of the taxonomy.
        ///   </value>
        /// <exception cref="System.InvalidOperationException"></exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static TaxonomyType GetTaxonomyType(Guid classificationId)
        {
            TaxonomyManager manager = TaxonomyManager.GetManager();
            var taxonomy = manager.GetTaxonomy(classificationId);
            var type = taxonomy.GetType();

            if (type.IsAssignableFrom(typeof(FlatTaxonomy)))
                return TaxonomyType.Flat;
            else if (type.IsAssignableFrom(typeof(FacetTaxonomy)))
                return TaxonomyType.Facet;
            else if (type.IsAssignableFrom(typeof(HierarchicalTaxonomy)))
                return TaxonomyType.Hierarchical;
            else if (type.IsAssignableFrom(typeof(NetworkTaxonomy)))
                return TaxonomyType.Network;

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Gets the identifier field.
        /// </summary>
        /// <param name="relatedDataType">Type of the related data.</param>
        /// <returns></returns>
        public static string GetIdentifierField(this string relatedDataType)
        {
            var identifierField = RelatedDataHelper.GetRelatedTypeIdentifierField(relatedDataType);

            return identifierField;
        }


        /// <summary>
        /// To the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataItemList">The data item list.</param>
        /// <returns></returns>
        public static IList<T> ToList<T>(this IList<IDataItem> dataItemList)
        {
            if (dataItemList == null)
                return null;

            return dataItemList.Cast<T>().ToList<T>();
        }

        /// <summary>
        /// Gets the single.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataItemList">The data item list.</param>
        /// <returns></returns>
        public static T GetSingle<T>(this IList<IDataItem> dataItemList)
        {
            if (dataItemList == null)
                return default(T);

            var dataItem = dataItemList.Cast<T>().FirstOrDefault();

            return dataItem;
        }
    }
}
