using System;
using System.Collections.Generic;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// FilterNewsItemWithCustomTaxonomyOnPage arrangement class.
    /// </summary>
    public class FilterNewsItemWithCustomTaxonomyOnPage : ITestArrangement
    {   
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
           /* Guid newsItemId1 = ServerOperations.News().CreatePublishedNewsItem(NewsTitle1, NewsContent1, NewsProvider);
            Guid newsItemId2 = ServerOperations.News().CreatePublishedNewsItem(NewsTitle2, NewsContent2, NewsProvider);*/
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId);

            ServerOperations.Taxonomies().CreateFlatTaxonomy(this.customFlatTaxonomyName);
            ServerOperations.Taxonomies().CreateFlatTaxon(this.customFlatTaxonName1, this.customFlatTaxonomyName);
            ServerOperations.Taxonomies().CreateFlatTaxon(this.customFlatTaxonName2, this.customFlatTaxonomyName);
            ServerOperations.CustomFields().AddCustomTaxonomyToContext(this.newsType, this.customFlatTaxonomyName, this.isHierarchicalTaxonomy);
            ServerOperations.SystemManager().RestartApplication(false);

            var customTaxonName1 = new List<string> { this.customFlatTaxonName1 };
            var customTaxonName2 = new List<string> { this.customFlatTaxonName2 };
            ServerOperationsFeather.NewsOperations().CreatePublishedNewsItemWithCustomTaxonomy(NewsTitle1, NewsContent1, "AuthorName", "SourceName", this.customFlatTaxonomyName, customTaxonName1, null);
            ServerOperationsFeather.NewsOperations().CreatePublishedNewsItemWithCustomTaxonomy(NewsTitle2, NewsContent2, "AuthorName", "SourceName", this.customFlatTaxonomyName, customTaxonName2, null);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {           
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.News().DeleteAllNews();
            ServerOperations.CustomFields().RemoveCustomFieldsFromContent(this.newsType, this.customFlatTaxonomyName);
            ServerOperations.SystemManager().RestartApplication(false);
            ServerOperations.CustomFields().InitializeManager("Telerik.Sitefinity.Modules.News.NewsManager");
            ServerOperations.Taxonomies().DeleteFlatTaxonomy(this.customFlatTaxonomyName);
            ServerOperations.News().DeleteAllNews();
        }
        
        private const string PageName = "News";
        private const string NewsContent1 = "News content1";
        private const string NewsTitle1 = "NewsTitle1";
        private const string NewsContent2 = "News content2";
        private const string NewsTitle2 = "NewsTitle2";
        private const string NewsProvider = "Default News";

        private readonly string customFlatTaxonomyName = "MyCustomFlatTaxonomy";
        private readonly string customFlatTaxonName1 = "MyFlatTaxon1";
        private readonly string customFlatTaxonName2 = "MyFlatTaxon2";
        private readonly string newsType = "Telerik.Sitefinity.News.Model.NewsItem,Telerik.Sitefinity.ContentModules";
        private readonly bool isHierarchicalTaxonomy = false;
    }
}
