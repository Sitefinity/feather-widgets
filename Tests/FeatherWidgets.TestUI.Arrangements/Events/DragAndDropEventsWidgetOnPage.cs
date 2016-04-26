using System;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// DragAndDropEventsWidgetOnPage arrangement class.
    /// </summary>
    public class DragAndDropEventsWidgetOnPage
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
            ServerOperations.Events().CreateEvent(EventsTitle);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeletePage(PageName);
            ServerOperations.Events().DeleteEvent(null, EventsTitle);
        }

        private const string PageName = "Events";
        private const string EventsContent = "Events content";
        private const string EventsTitle = "EventsTitle";
    }
}
