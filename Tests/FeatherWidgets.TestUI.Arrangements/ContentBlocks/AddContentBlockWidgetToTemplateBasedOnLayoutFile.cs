using System;
using System.IO;
using System.Threading;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// AddContentBlockWidgetToTemplateBasedOnLayoutFile arrangement class.
    /// </summary>
    public class AddContentBlockWidgetToTemplateBasedOnLayoutFile : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass, true);

            ServerOperations.Templates().CreatePureMVCPageTemplate(TemplateTitle);        
        }

        [ServerArrangement]
        public void GetTemplateId()
        {
            var templateId = ServerOperationsFeather.TemplateOperations().GetTemplateIdByTitle(TemplateTitle);
            ServerOperations.Pages().CreatePage(PageName, templateId);

            ServerArrangementContext.GetCurrent().Values.Add("templateId", templateId.ToString());
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Templates().DeletePageTemplate(TemplateTitle);
        }

        private const string TemplateTitle = "TestLayout";
        private const string PageName = "FeatherPage";
    }
}
