using System;
using System.Linq;
using System.Web.UI;

using DynamicContent.Mvc.Controllers;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Utilities.TypeConverters;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Frontend.Mvc.Models;

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
        /// Resolves the plural form of the control's content type display name.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns>Plural display name of the control's content type.</returns>
        public static string ContentTypeDisplayNamePlural(this Control control)
        {
            var dynamicType = control.ResolveDynamicModuleType();
            return PluralsResolver.Instance.ToPlural(dynamicType.DisplayName);
        }

        /// <summary>
        /// Resolves all parent types of the type that is associated with the control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns>Parent module types.</returns>
        public static IEnumerable<DynamicModuleType> ParentModuleTypes(this Control control)
        {
            var dynamicType = control.ResolveDynamicModuleType();

            var result = new List<DynamicModuleType>();
            while (dynamicType.ParentModuleType != null)
            {
                result.Add(dynamicType.ParentModuleType);
                dynamicType = dynamicType.ParentModuleType;
            }

            return result;
        }

        /// <summary>
        /// Returns a string of serialized parent types of the type that is associated with the control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns>The serialized parent types.</returns>
        public static string SerializedParentTypes(this Control control)
        {
            var parentTypes = control.ParentModuleTypes();
            var parentTypeInfos = parentTypes.Select(p => new { TypeName = p.GetFullTypeName(), DisplayName = PluralsResolver.Instance.ToPlural(p.DisplayName).ToLower() });

            return new JavaScriptSerializer().Serialize(parentTypeInfos);
        }

        /// <summary>
        /// Checks whether 'Narrow selection' section should be expanded.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns>Whether 'Narrow selection' section should be expanded.</returns>
        public static bool ExpandNarrowSelection(this Control control)
        {
            var controller = control.ResolveController();
            return controller.Model.SelectionMode != SelectionMode.AllItems;
        }

        /// <summary>
        /// Converts a word from a string to its plural form.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>Plural form of the word.</returns>
        public static string ToPlural(this string word)
        {
            return PluralsResolver.Instance.ToPlural(word);
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
        private static DynamicModuleType ResolveDynamicModuleType(this Control control)
        {
            var mvcProxy = control as MvcWidgetProxy;
            if (mvcProxy == null)
                throw new ArgumentException("This method should be used for control designers of Dynamic content MVC widgets.");

            var dynamicContentController = mvcProxy.Controller as DynamicContentController;
            if (dynamicContentController == null)
                throw new ArgumentException("This method should be used for DynamicContentController's designer only.");

            return dynamicContentController.GetDynamicContentType(mvcProxy.WidgetName);
        }

        private static DynamicContentController ResolveController(this Control control)
        {
            var mvcProxy = control as MvcWidgetProxy;
            if (mvcProxy == null)
                throw new ArgumentException("This method should be used for control designers of Dynamic content MVC widgets.");

            var dynamicContentController = mvcProxy.Controller as DynamicContentController;
            if (dynamicContentController == null)
                throw new ArgumentException("This method should be used for DynamicContentController's designer only.");

            return dynamicContentController;
        }
    }
}
