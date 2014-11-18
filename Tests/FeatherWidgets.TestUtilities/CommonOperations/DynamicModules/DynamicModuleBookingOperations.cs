using System;
using System.Linq;
using System.Text.RegularExpressions;
using Telerik.Sitefinity;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace FeatherWidgets.TestUtilities.CommonOperations
{
    /// <summary>
    /// Common operations for Booking dynamic module
    /// </summary>
    public class DynamicModuleBookingOperations
    {
        /// <summary>
        /// Creates a new country item.
        /// </summary>
        /// <param name="title">The title of the country.</param>     
        public Guid CreateCountry(string title)
        {
            var providerName = string.Empty;

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type countryType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Booking.Country");
            DynamicContent countryItem = dynamicModuleManager.CreateDataItem(countryType);

            countryItem.SetValue("Title", title);

            countryItem.SetString("UrlName", Regex.Replace(title.ToLower(), urlNameCharsToReplace, urlNameReplaceString));
            countryItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            countryItem.SetValue("PublicationDate", DateTime.Now);
            countryItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
            dynamicModuleManager.Lifecycle.Publish(countryItem);

            dynamicModuleManager.SaveChanges();

            return countryItem.Id;
        }

        /// <summary>
        /// Creates a new city item.
        /// </summary>
        /// <param name="parentId">The id of the country.</param>
        /// <param name="title">The title of the city.</param>        
        public Guid CreateCity(Guid parentId, string title)
        {
            var providerName = string.Empty;

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type cityType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Booking.City");
            DynamicContent cityItem = dynamicModuleManager.CreateDataItem(cityType);

            cityItem.SetValue("Title", title);
            cityItem.SetString("UrlName", Regex.Replace(title.ToLower(), urlNameCharsToReplace, urlNameReplaceString));
            cityItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            cityItem.SetValue("PublicationDate", DateTime.Now);
            cityItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
            
            Type countryType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Booking.Country");
            DynamicContent parent = dynamicModuleManager.GetDataItems(countryType)
                .Where(i => i.Id == parentId).First();

            cityItem.SetParent(parent.Id, countryType.FullName);

            dynamicModuleManager.Lifecycle.Publish(cityItem);
            dynamicModuleManager.SaveChanges();

            return cityItem.Id;
        }

        /// <summary>
        /// Creates a new hotel item.
        /// </summary>
        /// <param name="parentId">The id of the city.</param>
        /// <param name="title">The title of the hotel.</param>
        public void CreateHotel(Guid parentId, string title)
        {
            var providerName = string.Empty;

            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
            Type hotelType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Booking.Hotel");
            DynamicContent hotelItem = dynamicModuleManager.CreateDataItem(hotelType);

            hotelItem.SetValue("Title", title);
            hotelItem.SetString("UrlName", Regex.Replace(title.ToLower(), urlNameCharsToReplace, urlNameReplaceString));
            hotelItem.SetValue("Owner", SecurityManager.GetCurrentUserId());
            hotelItem.SetValue("PublicationDate", DateTime.Now);

            hotelItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, "Published");
            
            Type cityType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.Booking.City");
            DynamicContent parent = dynamicModuleManager.GetDataItems(cityType)
                .Where(i => i.Id == parentId).First();
            hotelItem.SetParent(parent.Id, cityType.FullName);

            dynamicModuleManager.Lifecycle.Publish(hotelItem);
            dynamicModuleManager.SaveChanges();
        }

        private static string urlNameCharsToReplace = @"[^\w\-\!\$\'\(\)\=\@\d_]+";
        private static string urlNameReplaceString = "-";
    }
}
