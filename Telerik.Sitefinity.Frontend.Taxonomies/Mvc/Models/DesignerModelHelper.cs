using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Controllers;
using Telerik.Sitefinity.Mvc.Proxy;

namespace Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models
{
    /// <summary>
    /// Helper class for views of the designer.
    /// </summary>
    public static class DesignerModelHelper
    {
        /// <summary>
        /// Gets the taxonomy id from the model of the control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public static Guid GetTaxonomyId(this Control control)
        {
            var mvcProxy = control as MvcControllerProxy;
            if (mvcProxy == null)
                throw new ArgumentException("This method should be used for control designers of Tags and Categories MVC widgets.");

            var flatTaxonomyController = mvcProxy.Controller as FlatTaxonomyController;
            var hierarchicalTaxonomyController = mvcProxy.Controller as HierarchicalTaxonomyController;
            if (flatTaxonomyController == null && hierarchicalTaxonomyController == null)
            {
                throw new ArgumentException("This method should be used for MVC Tags and Categories designers only.");
            }

            if (flatTaxonomyController != null)
            {
                return flatTaxonomyController.Model.TaxonomyId;
            }
            else
            {
                return hierarchicalTaxonomyController.Model.TaxonomyId;
            }
        }
    }
}
