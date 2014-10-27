using System;
using System.Linq;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;

namespace FeatherWidgets.TestUtilities.CommonOperations.Pages
{
    /// <summary>
    /// This class provides access to navigation common server operations
    /// </summary>
    public class NavigationOperations
    {
        /// <summary>
        /// Change the css class of the navigation widget.
        /// There should be an already added css class in order to have the property present in the model.
        /// </summary>
        /// <param name="pageTitle">The title of the page with the navigation widget.</param>
        /// <param name="cssClass">The css class.</param>
        public void ChangeCssClassMvcNavigationWidget(string pageTitle, string cssClass)
        {
            PageManager pm = PageManager.GetManager();

            PageNode page = pm.GetPageNodes().FirstOrDefault(pn => pn.Title == pageTitle);

            var pageData = pm.PagesLifecycle.GetMaster(page.GetPageData());

            var navigationControl = pageData.Controls.Where(c => c.ObjectType == typeof(MvcControllerProxy).FullName).FirstOrDefault();

            if (navigationControl == null)
            {
                throw new ArgumentException("Navigation control was not found on the page");
            }

            var settings = navigationControl.Properties.Where(p => p.Name == "Settings").FirstOrDefault();
            var css = settings.ChildProperties.Where(c => c.Name == "CssClass").FirstOrDefault();

            css.Value = cssClass;

            pm.PagesLifecycle.Publish(pageData);
            pm.SaveChanges();
        }
    }
}
