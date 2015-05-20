using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.SiteSelector;
using Telerik.Sitefinity.Frontend.TestUtilities.DummyClasses.HttpContext;

namespace FeatherWidgets.TestUnit.Navigation.SitesWidget
{
    /// <summary>
    /// Tests methods for the SiteSelectorController
    /// </summary>
    [TestClass]
    public class SiteSelectorControllerTests
    {
        /// <summary>
        /// Sites the selector controller_ call the index action_ ensures default model properties are presented.
        /// </summary>
        [TestMethod]
        [Owner("Manev")]
        public void SiteSelectorController_CallTheIndexAction_EnsuresDefaultModelPropertiesArePresented()
        {
            // Arrange
            using (var controller = new TeststableSiteSelectorController())
            {
                // Act
                var view = controller.Index() as ViewResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsTrue(view.ViewName == "SiteSelector.SiteLinks");
            }
        }

        /// <summary>
        /// Sites the name of the selector controller_ call the index action_ ensures changing view.
        /// </summary>
        [TestMethod]
        [Owner("Manev")]
        public void SiteSelectorController_CallTheIndexAction_EnsuresChangingViewName()
        {
            // Arrange
            using (var controller = new TeststableSiteSelectorController())
            {
                string templateName = "CustomTemplateName";

                controller.TemplateName = templateName;

                // Act
                var view = controller.Index() as ViewResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsTrue(view.ViewName == ("SiteSelector." + templateName));
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void SiteSelectorController_CallTheIndexAction_EnsuresDefaultSiteHasProperUrlFromQueryString()
        {
            var sites = new List<SiteViewModel>
            {
                new SiteViewModel { IsCurrent = true }
            };

            // Arrange
            using (var controller = new TeststableSiteSelectorController(new EmptySiteSelectorMode(sites)))
            {
                var queryString = new NameValueCollection();
                queryString.Add("name", "value");

                var dummyHttpContext = new MyDummyHttpContext(queryString);
                controller.ControllerContext = new ControllerContext(dummyHttpContext, new RouteData { }, controller);

                // Act
                var view = controller.Index() as ViewResult;

                var firstSite = sites.First();

                // Assert
                Assert.IsNotNull(firstSite);
                Assert.IsNotNull(view);
                Assert.IsTrue(firstSite.Url == "?name=value");
            }
        }

        private class MyDummyHttpRequest : DummyHttpRequest
        {
            private readonly NameValueCollection queryString;

            public MyDummyHttpRequest(HttpContextBase httpContext, string appPath, NameValueCollection queryString)
                : base(httpContext, appPath)
            {
                this.queryString = queryString;
            }

            public override NameValueCollection QueryString
            {
                get
                {
                    return this.queryString;
                }
            }
        }

        private class MyDummyHttpContext : DummyHttpContext
        {
            private readonly HttpRequestBase request;

            public MyDummyHttpContext(NameValueCollection queryString)
                : base()
            {
                this.request = new MyDummyHttpRequest(this, "/", queryString);
            }

            public override HttpRequestBase Request
            {
                get
                {
                    return this.request;
                }
            }
        }

        private class TeststableSiteSelectorController : SiteSelectorController
        {
            private readonly ISiteSelectorModel model;

            public TeststableSiteSelectorController()
                : this(new EmptySiteSelectorMode())
            {
            }

            public TeststableSiteSelectorController(ISiteSelectorModel model)
            {
                this.model = model;
            }

            public override ISiteSelectorModel Model
            {
                get
                {
                    return this.model;
                }
            }

            protected override bool IsMultisiteMode
            {
                get
                {
                    return true;
                }
            }
        }

        private class EmptySiteSelectorMode : ISiteSelectorModel
        {
            private readonly List<SiteViewModel> sites;

            public EmptySiteSelectorMode()
                : this(new List<SiteViewModel>())
            {
            }

            public EmptySiteSelectorMode(List<SiteViewModel> sites)
            {
                this.sites = sites;
            }

            public bool IncludeCurrentSite { get; set; }

            public bool EachLanguageAsSeparateSite { get; set; }

            public SiteLanguagesDisplayMode SiteLanguagesDisplayMode { get; set; }

            public bool UseLiveUrl { get; set; }

            public string CssClass { get; set; }

            public SiteSelectorViewModel CreateViewModel()
            {
                return new SiteSelectorViewModel { Sites = this.sites };
            }
        }
    }
}
