using System;
using System.IO;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// AddRelatedDataToNewsWidget arragement.
    /// </summary>
    public class AddRelatedDataToNewsWidget : ITestArrangement
    {
        /// <summary>
        /// Server side set up. 
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            var newsId = ServerOperations.News().CreatePublishedNewsItem(News1, string.Empty);
            var relatedNewsId = ServerOperations.News().CreatePublishedNewsItem(News2, string.Empty);

            ServerOperations.RelatedData().AddRelatedDataCustomField(ContentTypeFullNames.NewsItem, ContentTypeFullNames.NewsItem, AddRelatedDataToNewsWidget.FieldName);

            ServerOperations.RelatedData().RelateItem(
                                          ContentTypeFullNames.NewsItem,
                                          newsId,
                                          ContentTypeFullNames.NewsItem,
                                          relatedNewsId,
                                          AddRelatedDataToNewsWidget.FieldName);

            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId);

            string filePath = FileInjectHelper.GetDestinationFilePath(this.viewPath);
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            Stream destination = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            var assembly = ServerOperationsFeather.Pages().GetTestUtilitiesAssembly();
            Stream source = assembly.GetManifestResourceStream(FileResource);
            FileInjectHelper.CopyStream(source, destination);
            source.Close();
            destination.Close();
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().DeleteAllNews();
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
            ServerOperations.RelatedData().RemoveRelatedDataFieldFromContex(ContentTypeFullNames.NewsItem, AddRelatedDataToNewsWidget.FieldName);
            string filePath = FileInjectHelper.GetDestinationFilePath(this.viewPath);
            File.Delete(filePath);
        }

        private const string PageName = "NewsPage";
        private const string FileResource = "FeatherWidgets.TestUtilities.Data.NewsWidget.Detail.DetailPageRelatedData.cshtml";
        private string viewPath = Path.Combine("Mvc", "Views", "Shared", ViewName);
        private const string ViewName = "Detail.DetailPageRelatedData.cshtml";
        private const string FieldName = "RelatedNews";
        private const string News1 = "NewsItem";
        private const string News2 = "NewsItem2";
    }
}
