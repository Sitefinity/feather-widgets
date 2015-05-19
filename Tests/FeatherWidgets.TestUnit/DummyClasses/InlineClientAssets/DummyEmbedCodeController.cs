using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Controllers;

namespace FeatherWidgets.TestUnit.DummyClasses.InlineClientAssets
{
    public class DummyEmbedCodeController : EmbedCodeController
    {
        private readonly string resource;

        private readonly bool isEdit;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public DummyEmbedCodeController(bool isEdit = false)
        {
            this.isEdit = isEdit;
        }

        public DummyEmbedCodeController(string resource, bool isEdit)
            : this(isEdit)
        {
            this.resource = resource;
        }

        protected override string GetIncludedWhereDroppedResourceString
        {
            get
            {
                return this.resource;
            }
        }

        protected override bool IsEdit
        {
            get
            {
                return this.isEdit;
            }
        }
    }
}
