﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Locations.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Frontend.DynamicContent.WidgetTemplates.Fields.Helpers
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
        /// Gets the label.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="parentTypeId">The parent type identifier.</param>
        /// <returns></returns>
        public static string GetChoiceLabel(this ItemViewModel item, string fieldName, Type type)
        {
            var fieldValue = item.Fields.GetMemberValue(fieldName).ToString();
            var moduleType = ModuleBuilderManager.GetModules().GetTypeByFullName(type.FullName);
            string label = fieldValue;

            if (moduleType != null && moduleType.Fields != null)
            {
                var field = moduleType.Fields.Where(f => f.Name == fieldName).FirstOrDefault();
                if (field != null)
                {
                    try
                    {
                        System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Parse(field.Choices);
                        var element = doc.Elements("element").Where(e => e.Attribute("value").Value == fieldValue).FirstOrDefault();
                        label = (element != null) ? element.Attribute("text").Value : fieldValue;
                    }
                    catch (System.Xml.XmlException)
                    {
                        // not an XML
                    }
                }
            }

            return label;
        }

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

            // TODO: breaking change
            if (type.IsAssignableFrom(typeof(FlatTaxonomy)))
                return TaxonomyType.Flat;
            else if (type.IsAssignableFrom(typeof(HierarchicalTaxonomy)))
                return TaxonomyType.Hierarchical;

            throw new InvalidOperationException();
        }

        /// <summary>
        /// HTML 'target' attribute for the item link.
        /// </summary>
        /// <param name="item">The item view model.</param>
        /// <returns>HTML 'target' attribute for the item link.</returns>
        public static string LinkTargetAttribute(this ItemViewModel item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (item.DataItem == null || !(item.DataItem is PageNode))
                throw new InvalidOperationException("LinkTargetAttribute extensions should only be used on view models of page node.");

            return item.Fields.OpenNewWindow ? "target='_blank'" : string.Empty;
        }
    }
}
