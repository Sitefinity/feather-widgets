using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.Navigation
{
    public class DummyBreadcrumbExtender : IBreadcrumExtender
    {
        public IEnumerable<SiteMapNode> GetVirtualNodes(SiteMapProvider provider)
        {
            List<SiteMapNode> list = new List<SiteMapNode>();            
               
            var siteMapNode = new SiteMapNode(
                provider, Guid.NewGuid().ToString(), string.Empty, DummySiteMapNodeTitle, "description");
            list.Add(siteMapNode);                
            
            return list;
        }

        public const string DummySiteMapNodeTitle = "dummySiteMapNode";
    }
}
