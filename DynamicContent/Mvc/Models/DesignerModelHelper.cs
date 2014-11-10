using System;
using System.Web.UI;

using DynamicContent.Mvc.Controllers;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace DynamicContent.Mvc.Models
{
    /// <summary>
    /// Helper class for views of the designer.
    /// </summary>
    public static class DesignerModelHelper
    {
        /// <summary>
        /// Resolves the dynamic content type of the control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns>The content type of the control.</returns>
        public static Type DynamicContentType(this Control control)
        {
            var dynamicType = control.ResolveDynamicModuleType();
            return TypeResolutionService.ResolveType(dynamicType.GetFullTypeName());
        }

        /// <summary>
        /// Resolves the module name of the dynamic content type that of the control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns>Module name of the dynamic content type.</returns>
        public static string DynamicModuleName(this Control control)
        {
            var dynamicType = control.ResolveDynamicModuleType();
            return dynamicType.ModuleName;
        }

        /// <summary>
        /// Resolves the plural lower case form of the control's content type display name.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns>Plural lower case display name of the control's content type.</returns>
        public static string ContentTypeDisplayNamePluralLower(this Control control)
        {
            var dynamicType = control.ResolveDynamicModuleType();
            return PluralsResolver.Instance.ToPlural(dynamicType.DisplayName).ToLower();
        }

        /// <summary>
        /// Resolves the type of the dynamic module.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns>The dynamic module type.</returns>
        /// <exception cref="System.ArgumentException">
        /// This method should be used for control designers of Dynamic content MVC widgets.
        /// or
        /// This method should be used for DynamicContentController's designer only.
        /// </exception>
        public static DynamicModuleType ResolveDynamicModuleType(this Control control)
        {
            var mvcProxy = control as MvcWidgetProxy;
            if (mvcProxy == null)
                throw new ArgumentException("This method should be used for control designers of Dynamic content MVC widgets.");

            var dynamicContentController = mvcProxy.Controller as DynamicContentController;
            if (dynamicContentController == null)
                throw new ArgumentException("This method should be used for DynamicContentController's designer only.");

            return dynamicContentController.GetDynamicContentType(mvcProxy.WidgetName);
        }
    }
}
