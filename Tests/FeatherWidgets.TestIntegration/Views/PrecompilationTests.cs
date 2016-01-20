using System;
using System.Linq;
using System.Web.Mvc;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Card.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Comments.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Lists.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Media.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.News.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Publishing.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Search.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.SocialShare.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Controllers;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestUtilities;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.Views
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Precompilation"), TestFixture]
    [Description("Integration tests for proper compilation of the Feather views.")]
    [Author(TestAuthor.Team2)]
    public class PrecompilationTests
    {
        /// <summary>
        /// Set up method
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.precompilationFilter = new PrecompilationFilterAttribute();
            this.pagesOperations = new PagesOperations();
            GlobalFilters.Filters.Add(this.precompilationFilter);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.pagesOperations.DeletePages();
            GlobalFilters.Filters.Remove(this.precompilationFilter);
        }

        [Test]
        [Description("Ensures that content widgets use precompiled view on the front-end.")]
        public void Content_Frontend_UsePrecompiledView()
        {
            var widgetControllerTypes = new Type[15] 
            { 
                typeof(BlogController),
                typeof(BlogPostController),
                typeof(CardController),
                typeof(CommentsController),
                typeof(ReviewsController),
                typeof(ContentBlockController),
                typeof(ListsController),
                typeof(DocumentController),
                typeof(DocumentsListController),
                typeof(ImageController),
                typeof(ImageGalleryController),
                typeof(VideoController),
                typeof(VideoGalleryController),
                typeof(NewsController),
                typeof(FeedController)
            };

            this.AddWidgetToPageAndInvokeIt(widgetControllerTypes);
        }

        [Test]
        [Description("Ensures that navigation widgets use precompiled view on the front-end.")]
        public void Navigation_Frontend_UsePrecompiledView()
        {
            var widgetControllerTypes = new Type[4] 
            { 
                typeof(NavigationController),
                typeof(BreadcrumbController),
                typeof(SiteSelectorController),
                typeof(LanguageSelectorController)
            };

            this.AddWidgetToPageAndInvokeIt(widgetControllerTypes);
        }

        [Test]
        [Description("Ensures that ScriptStyles widgets use precompiled view on the front-end.")]
        public void ScriptStyles_Frontend_UsePrecompiledView()
        {
            var widgetControllerTypes = new Type[3] 
            { 
                typeof(EmbedCodeController),
                typeof(JavaScriptController),
                typeof(StyleSheetController)
            };

            this.AddWidgetToPageAndInvokeIt(widgetControllerTypes);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login"), Test]
        [Description("Ensures that login widgets use precompiled view on the front-end.")]
        public void Login_Frontend_UsePrecompiledView()
        {
            var widgetControllerTypes = new Type[3] 
            { 
                typeof(LoginFormController),
                typeof(LoginStatusController),
                typeof(ChangePasswordController)
            };

            this.AddWidgetToPageAndInvokeIt(widgetControllerTypes);
        }

        [Test]
        [Description("Ensures that users widgets use precompiled view on the front-end.")]
        public void Users_Frontend_UsePrecompiledView()
        {
            var widgetControllerTypes = new Type[4] 
            { 
                typeof(ProfileController),
                typeof(AccountActivationController),
                typeof(RegistrationController),
                typeof(UsersListController)
            };

            this.AddWidgetToPageAndInvokeIt(widgetControllerTypes);
        }

        [Test]
        [Description("Ensures that social widgets use precompiled view on the front-end.")]
        public void Social_Frontend_UsePrecompiledView()
        {
            var widgetControllerTypes = new Type[1] { typeof(SocialShareController) };

            this.AddWidgetToPageAndInvokeIt(widgetControllerTypes);
        }

        [Test]
        [Description("Ensures that search widgets use precompiled view on the front-end.")]
        public void Search_Frontend_UsePrecompiledView()
        {
            var widgetControllerTypes = new Type[2] { typeof(SearchBoxController), typeof(SearchResultsController) };

            this.AddWidgetToPageAndInvokeIt(widgetControllerTypes);
        }

        [Test]
        [Description("Ensures that classification widgets use precompiled view on the front-end.")]
        public void Classification_Frontend_UsePrecompiledView()
        {
            var widgetControllerTypes = new Type[2] { typeof(FlatTaxonomyController), typeof(HierarchicalTaxonomyController) };

            this.AddWidgetToPageAndInvokeIt(widgetControllerTypes);
        }

        [Test]
        [Description("Ensures that email campaign widgets use precompiled view on the front-end.")]
        public void EmailCampaigns_Frontend_UsePrecompiledView()
        {
            var widgetControllerTypes = new Type[2] { typeof(UnsubscribeFormController), typeof(SubscribeFormController) };

            this.AddWidgetToPageAndInvokeIt(widgetControllerTypes);
        }

        private void AddWidgetToPageAndInvokeIt(Type[] widgetControllerTypes)
        {
            if (widgetControllerTypes == null || widgetControllerTypes.Count() == 0)
                return;

            string pageNamePrefix = "testpage";
            string pageTitlePrefix = "Precompiled Page";
            string urlNamePrefix = "test-page";
            int pageIndex = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);

            var mvcProxy = this.CreateMvcProxy(widgetControllerTypes[0]);

            var pageId = this.pagesOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, pageIndex);

            for (var i = 0; i < widgetControllerTypes.Count(); i++)
            {
                Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.Pages().AddMvcWidgetToPage(pageId, widgetControllerTypes[i].FullName, "Widget" + i, "Body");
            }

            PageInvoker.ExecuteWebRequest(url);
        }

        private MvcControllerProxy CreateMvcProxy(Type widgetControllerType)
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = widgetControllerType.FullName;
            var widgetController = Activator.CreateInstance(widgetControllerType) as Controller;
            mvcProxy.Settings = new ControllerSettings(widgetController);

            return mvcProxy;
        }

        private PrecompilationFilterAttribute precompilationFilter;

        private PagesOperations pagesOperations;
    }
}
