using System.Reflection;
using System.Text.RegularExpressions;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.InlineClientAssets
{
    [TestFixture]
    public class StyleSheetWidgetTests
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestIntegration.Data.Content.PageContentGenerator.AddControlToPage(System.Guid,System.Web.UI.Control,System.String,System.String,System.Action<Telerik.Sitefinity.Pages.Model.PageDraftControl>)"), Test]
        [Category(TestCategories.InlineClientAssets)]
        [Author(FeatherTeams.Team2)]
        [Description("Verifies that StyleSheet renders CSS markup in the page Head.")]
        public void StyleSheetWidget_RendersInHead()
        {
            var testName = MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName;
            string pageTitlePrefix = testName;
            string urlNamePrefix = testName;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(StyleSheetController).FullName;
            var controller = new StyleSheetController();
            controller.Model.Mode = ResourceMode.Reference;
            controller.Model.ResourceUrl = "http://my-styles.com/styles.css";
            controller.Model.MediaType = "screen";
            mvcProxy.Settings = new ControllerSettings(controller);

            using (var generator = new PageContentGenerator())
            {
                var pageId = generator.CreatePage(pageNamePrefix, pageTitlePrefix, urlNamePrefix);
                PageContentGenerator.AddControlToPage(pageId, mvcProxy, "CSS widget");

                string responseContent = PageInvoker.ExecuteWebRequest(url);
                Assert.IsTrue(this.IsInHead(responseContent, @"<link href=""http://my-styles.com/styles.css"" media=""screen"" rel=""stylesheet"" type=""text/css"" />"), "The stylesheet reference was not found in the head.");
            }
        }

        private bool IsInHead(string source, string tag)
        {
            var regExpression = "<head>(?!</head>).*" + Regex.Escape(tag) + ".*</head>";
            return new Regex(regExpression, RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(source);
        }
    }
}
