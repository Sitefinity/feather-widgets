using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.Navigation
{
    public class DummySitemapFilter : ISitemapNodeFilter
    {
        public static void RestrictPageNode(Guid id)
        {
            DummySitemapFilter.restrictedNodes.Add(id);
        }

        public static void Clear()
        {
            DummySitemapFilter.restrictedNodes.Clear();
        }

        public bool IsNodeAccessPrevented(PageSiteNode pageNode)
        {
            bool isRestricted = false;

            if (DummySitemapFilter.restrictedNodes.Contains(pageNode.Id))
            {
                isRestricted = true;
            }

            return isRestricted;
        }

        private static List<Guid> restrictedNodes = new List<Guid>();     
    }
}