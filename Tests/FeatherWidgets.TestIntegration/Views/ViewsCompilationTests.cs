using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MbUnit.Framework;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using Telerik.Sitefinity.TestUtilities;

namespace FeatherWidgets.TestIntegration.Views
{
    [TestFixture]
    [AssemblyFixture]
    [Description("Integration tests for proper compilation of the Feather views.")]
    [Author(TestAuthor.Team7)]
    public class ViewsCompilationTests
    {
        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that all .cshtml views in aTelerik.Sitefinity.Frontend.Blogs can be compiled.")]
        public void EnsureBlogsViewsCanBeCompiled()
        {
            var assembly = this.GetAssembly("Telerik.Sitefinity.Frontend.Blogs");

            this.AssertAllViewsInAssemblyCanBeCompiled(assembly);
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that all .cshtml views in Telerik.Sitefinity.Frontend.Comments can be compiled.")]
        public void EnsureCommentsViewsCanBeCompiled()
        {
            var assembly = this.GetAssembly("Telerik.Sitefinity.Frontend.Comments");

            this.AssertAllViewsInAssemblyCanBeCompiled(assembly);
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that all .cshtml views in Telerik.Sitefinity.Frontend.ContentBlock can be compiled.")]
        public void EnsureContentBlockViewsCanBeCompiled()
        {
            var assembly = this.GetAssembly("Telerik.Sitefinity.Frontend.ContentBlock");

            this.AssertAllViewsInAssemblyCanBeCompiled(assembly);
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that all .cshtml views in Telerik.Sitefinity.Frontend.DynamicContent can be compiled.")]
        public void EnsureDynamicContentViewsCanBeCompiled()
        {
            var assembly = this.GetAssembly("Telerik.Sitefinity.Frontend.DynamicContent");

            this.AssertAllViewsInAssemblyCanBeCompiled(assembly);
        }

        [Ignore("RazorEngine cannot compile statements with lambda like @Html.TextBoxFor(m => m.UserName). The reason for this behavior should be researched.")]
        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that all .cshtml views in Telerik.Sitefinity.Frontend.EmailCampaigns can be compiled.")]
        public void EnsureEmailCampaignsViewsCanBeCompiled()
        {
            var assembly = this.GetAssembly("Telerik.Sitefinity.Frontend.EmailCampaigns");

            this.AssertAllViewsInAssemblyCanBeCompiled(assembly);
        }

        [Ignore("RazorEngine cannot compile statements with lambda like @Html.TextBoxFor(m => m.UserName). The reason for this behavior should be researched.")]
        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that all .cshtml views in Telerik.Sitefinity.Frontend.Identity can be compiled.")]
        public void EnsureIdentityViewsCanBeCompiled()
        {
            var assembly = this.GetAssembly("Telerik.Sitefinity.Frontend.Identity");

            this.AssertAllViewsInAssemblyCanBeCompiled(assembly);
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that all .cshtml views in Telerik.Sitefinity.Frontend.InlineClientAssets can be compiled.")]
        public void EnsureInlineClientAssetsViewsCanBeCompiled()
        {
            var assembly = this.GetAssembly("Telerik.Sitefinity.Frontend.InlineClientAssets");

            this.AssertAllViewsInAssemblyCanBeCompiled(assembly);
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that all .cshtml views in Telerik.Sitefinity.Frontend.Lists can be compiled.")]
        public void EnsureListsViewsCanBeCompiled()
        {
            var assembly = this.GetAssembly("Telerik.Sitefinity.Frontend.Lists");

            this.AssertAllViewsInAssemblyCanBeCompiled(assembly);
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that all .cshtml views in Telerik.Sitefinity.Frontend.Media can be compiled.")]
        public void EnsureMediaViewsCanBeCompiled()
        {
            var assembly = this.GetAssembly("Telerik.Sitefinity.Frontend.Media");

            this.AssertAllViewsInAssemblyCanBeCompiled(assembly);
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that all .cshtml views in Telerik.Sitefinity.Frontend.Navigation can be compiled.")]
        public void EnsureNavigationViewsCanBeCompiled()
        {
            var assembly = this.GetAssembly("Telerik.Sitefinity.Frontend.Navigation");

            this.AssertAllViewsInAssemblyCanBeCompiled(assembly);
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that all .cshtml views in Telerik.Sitefinity.Frontend.News can be compiled.")]
        public void EnsureNewsViewsCanBeCompiled()
        {
            var assembly = this.GetAssembly("Telerik.Sitefinity.Frontend.News");

            this.AssertAllViewsInAssemblyCanBeCompiled(assembly);
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that all .cshtml views in Telerik.Sitefinity.Frontend.Publishing can be compiled.")]
        public void EnsurePublishingViewsCanBeCompiled()
        {
            var assembly = this.GetAssembly("Telerik.Sitefinity.Frontend.Publishing");

            this.AssertAllViewsInAssemblyCanBeCompiled(assembly);
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that all .cshtml views in Telerik.Sitefinity.Frontend.Search can be compiled.")]
        public void EnsureSearchViewsCanBeCompiled()
        {
            var assembly = this.GetAssembly("Telerik.Sitefinity.Frontend.Search");

            this.AssertAllViewsInAssemblyCanBeCompiled(assembly);
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that all .cshtml views in Telerik.Sitefinity.Frontend.SocialShare can be compiled.")]
        public void EnsureSocialShareViewsCanBeCompiled()
        {
            var assembly = this.GetAssembly("Telerik.Sitefinity.Frontend.SocialShare");

            this.AssertAllViewsInAssemblyCanBeCompiled(assembly);
        }

        [Test]
        [Author(TestAuthor.Team7)]
        [Description("Verifies that all .cshtml views in Telerik.Sitefinity.Frontend.Taxonomies can be compiled.")]
        public void EnsureTaxonomiesViewsCanBeCompiled()
        {
            var assembly = this.GetAssembly("Telerik.Sitefinity.Frontend.Taxonomies");

            this.AssertAllViewsInAssemblyCanBeCompiled(assembly);
        }

        private Assembly GetAssembly(string shortName)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == shortName);

            Assert.IsNotNull(assembly, string.Format(CultureInfo.InvariantCulture, "Assembly with name {0} was not found in the current app domain.", shortName));

            return assembly;
        }

        private void AssertAllViewsInAssemblyCanBeCompiled(Assembly assembly)
        {
            var config = new TemplateServiceConfiguration();
            config.BaseTemplateType = typeof(MvcTemplateBase<>);

            var processor = new TemplateService(config);

            var resourceNames = assembly.GetManifestResourceNames();

            foreach (var resource in resourceNames)
            {
                if (resource.EndsWith(".cshtml", StringComparison.OrdinalIgnoreCase))
                {
                    var content = this.GetEmbeddedViewContent(resource, assembly);

                    // The namespace for ChildActionExtensions
                    processor.AddNamespace("System.Web.Mvc.Html");

                    processor.AddNamespace("System.Web");

                    this.AssertViewCanBeCompiled(content, resource, processor);
                }
            }

            processor.Dispose();
        }

        private void AssertViewCanBeCompiled(string content, string viewPath, TemplateService processor)
        {
            try
            {
                processor.Compile(content, null, viewPath);
            }
            catch (TemplateCompilationException ex)
            {
                var message = new StringBuilder()
                    .AppendFormat("The view: {0} cannot be compiled because of the following errors:", viewPath);

                foreach (var error in ex.Errors)
                {
                    if (!error.IsWarning)
                    {
                        message.AppendLine().AppendFormat(
                            CultureInfo.InvariantCulture,
                            "Error: {0} - {1}",
                            error.ErrorNumber,
                            error.ErrorText);
                    }
                }

                Assert.Fail(message.ToString());
            }
        }

        private string GetEmbeddedViewContent(string path, Assembly assembly)
        {
            var fileStream = assembly.GetManifestResourceStream(path);

            using (var streamReader = new StreamReader(fileStream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
