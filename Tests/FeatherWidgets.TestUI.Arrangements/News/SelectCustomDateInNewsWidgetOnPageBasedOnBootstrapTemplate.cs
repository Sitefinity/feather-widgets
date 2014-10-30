using System;
using System.IO;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// SelectCustomDateInNewsWidgetOnPageBasedOnBootstrapTemplate arrangement class.
    /// </summary>
    public class SelectCustomDateInNewsWidgetOnPageBasedOnBootstrapTemplate : ITestArrangement
    {
        /// <summary>
        /// Server side set up. 
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            string templateFileOriginal = FileInjectHelper.GetDestinationFilePath(this.layoutTemplatePath);
            string templateFileCopy = FileInjectHelper.GetDestinationFilePath(this.newLayoutTemplatePath);

            PageManager pageManager = PageManager.GetManager();
            int templatesCount = pageManager.GetTemplates().Count();
            File.Copy(templateFileOriginal, templateFileCopy);
            FeatherServerOperations.ResourcePackages().WaitForTemplatesCountToIncrease(templatesCount, 1);

            for (int i = 0; i < this.newsTitles.Length; i++)
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreateNewsItem(this.newsTitles[i]);

            DateTime publicationDate = DateTime.UtcNow.AddDays(-10);

            var newsManager = NewsManager.GetManager();
            NewsItem modified = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "Boat").FirstOrDefault();
            newsManager.Lifecycle.PublishWithSpecificDate(modified, publicationDate);
            newsManager.SaveChanges();

            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);

            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId, "Contentplaceholder1");
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().DeleteAllNews();

            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();

            string templateFileCopy = FileInjectHelper.GetDestinationFilePath(this.newLayoutTemplatePath);
            File.Delete(templateFileCopy);

            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().DeletePageTemplate(PageTemplateName);
        }

        private const string PageName = "NewsPage";
        private const string PageTemplateName = "Bootstrap.defaultNew";
        private string layoutTemplatePath = Path.Combine("ResourcePackages", "Bootstrap", "MVC", "Views", "Layouts", "default.cshtml");
        private string newLayoutTemplatePath = Path.Combine("ResourcePackages", "Bootstrap", "MVC", "Views", "Layouts", "defaultNew.cshtml");
        private string[] newsTitles = { "Cat", "Angel", "Boat" };
    }
}
