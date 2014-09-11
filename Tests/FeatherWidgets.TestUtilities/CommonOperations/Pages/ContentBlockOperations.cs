using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Fluent;

namespace FeatherWidgets.TestUtilities.CommonOperations.Pages
{
    public class ContentBlockOperations
    {
        /// <summary>
        /// Creates a content block.
        /// </summary>
        /// <param name="contentBlockTitle">The content block title.</param>
        /// <param name="htmlContent">HTML Content of the content block.</param>
        public void CreateContentBlock(string contentBlockTitle, string htmlContent, string providerName = "OpenAccessDataProvider")
        {
            var appSettings = new AppSettings() { ContentManagerName = providerName, ContentProviderName = providerName };
            var fluent = App.Prepare(appSettings).WorkWith().ContentItem();
            try
            {
                fluent.GetManager().Provider.SuppressSecurityChecks = true;
                fluent.CreateNew()
                      .Do(c =>
                      {
                          c.Title = contentBlockTitle;
                          c.Content = htmlContent;
                      })
                      .Publish()
                      .SaveChanges();
            }
            finally
            {
                fluent.GetManager().Provider.SuppressSecurityChecks = false;
            }
        }
    }
}
