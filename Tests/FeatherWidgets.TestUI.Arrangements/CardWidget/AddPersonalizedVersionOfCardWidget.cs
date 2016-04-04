using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// AddPersonalizedVersionOfCardWidget arrangement class.
    /// </summary>
    public class AddPersonalizedVersionOfCardWidget : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddCardWidgetToPage(pageId);

            // Create a segment.
            ServerOperations.Personalization().CreateRoleSegment(AdministratorRole);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Personalization().DeleteAllSegments();
        }

        private const string PageName = "CardPage";
        private const string AdministratorRole = "Administrators";
    }
}
