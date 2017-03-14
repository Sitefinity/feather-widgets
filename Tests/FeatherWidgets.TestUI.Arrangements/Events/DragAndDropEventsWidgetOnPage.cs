using System;
using FeatherWidgets.TestUI.Arrangements.Events;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
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
            var templateId = ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);
            ServerOperations.Pages().CreatePage(PageName, templateId);
            ServerOperations.Events().CreateEvent(EventsTitle);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Events().DeleteAllEvents();
        }

        private const string PageName = "EventsPage";
        private const string EventsContent = "Events content";
        private const string EventsTitle = "EventsTitle";
        private const string TemplateTitle = "Bootstrap.default";
    }
}
