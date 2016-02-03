using System;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Mvc.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// LanguageSelectorXssAttack arrangement class.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Xss")]
    public class LanguageSelectorXssAttack : TestArrangementBase
    {
        /// <summary>
        /// Server side set up. 
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            FeatherServerOperations.Pages().AddMvcWidgetToPage(pageId, typeof(LanguageSelectorController).FullName, WidgetCaption, PlaceHolderId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
        }

        private const string PageName = "LanguagePage";
        private const string WidgetCaption = "Language selector";
        private const string PlaceHolderId = "Body";
    }
}
