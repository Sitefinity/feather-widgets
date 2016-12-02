using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Restriction;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// DeactivateFeatherModule arrangement class.
    /// </summary>
    public class DeactivateFeatherModule : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            var providerName = ContentManager.GetManager().Provider.Name;
            ServerOperationsFeather.ContentBlockOperations().CreateContentBlock(ContentBlockTitle, ContentBlockContent, providerName);
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddSharedContentBlockWidgetToPage(page1Id, ContentBlockTitle);
        }

        /// <summary>
        /// Deactivating Feather Module.
        /// </summary>
        [ServerArrangement]
        public void DeactivateModule()
        {
            using (new UnrestrictedModeRegion())
            {
                ServerOperations.StaticModules().DeactivateModule(FeatherModuleName);
            }
        }

        /// <summary>
        /// Activating Feather Module.
        /// </summary>
        [ServerArrangement]
        public void ActivateModule()
        {
            using (new UnrestrictedModeRegion())
            {
                ServerOperations.StaticModules().ActivateModule(FeatherModuleName);
            }
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.ContentBlocks().DeleteAllContentBlocks();
            using (new UnrestrictedModeRegion())
            {
                ServerOperations.StaticModules().ActivateModule(FeatherModuleName);
            }
        }

        private const string PageName = "ContentBlock";
        private const string ContentBlockContent = "Test content";
        private const string ContentBlockTitle = "ContentBlockTitle";
        private const string FeatherModuleName = "Feather";
    }
}
