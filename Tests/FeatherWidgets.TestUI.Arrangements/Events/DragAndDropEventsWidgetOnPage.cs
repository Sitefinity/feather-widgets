using System;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements.Events
{
    /// <summary>
    /// DragAndDropEventsWidgetOnPage arrangement class.
    /// </summary>
    public class DragAndDropEventsWidgetOnPage : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            string templateName = ServerArrangementContext.GetCurrent().Values["templateName"];

            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(templateName);
            ServerOperations.Pages().CreatePage(PageName, templateId);
            ServerOperations.News().CreatePublishedNewsItem(EventsTitle);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.News().DeleteAllNews();
        }

        private const string PageName = "Events";
        private const string EventsContent = "Events content";
        private const string EventsTitle = "EventsTitle";
    }
}
