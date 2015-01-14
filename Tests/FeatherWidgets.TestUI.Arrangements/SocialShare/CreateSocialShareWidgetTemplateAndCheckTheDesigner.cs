using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// CreateSocialShareWidgetTemplateAndCheckTheDesigner arragement.
    /// </summary>
    public class CreateSocialShareWidgetTemplateAndCheckTheDesigner : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperations.Pages().CreatePage(PageName);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Widgets().DeleteWidgetTemplate(TemplateName);
        }

        private const string TemplateName = "SocialShareTest";
        private const string PageName = "SocialShare";
    }
}
