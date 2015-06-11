using System;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Frontend.Publishing.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Publishing.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities.DummyClasses.HttpContext;

namespace FeatherWidgets.TestUnit.DummyClasses.Publishing
{
    internal class DummyFeedController : FeedController
    {
        private readonly bool isModuleActive;

        public DummyFeedController()
            : this(true)
        {
        }

        public DummyFeedController(bool isModuleActive)
        {
            this.isModuleActive = isModuleActive;
        }

        protected override HttpContextBase GetHttpContext()
        {
            return new DummyHttpContext();
        }

        protected override bool IsPublishingModuleActivated()
        {
            return this.isModuleActive;
        }
    }
}
