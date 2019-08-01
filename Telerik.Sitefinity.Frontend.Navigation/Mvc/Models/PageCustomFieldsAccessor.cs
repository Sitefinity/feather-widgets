using System;
using System.ComponentModel;
using System.Dynamic;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models
{
    /// <summary>
    /// Instances of this class are used to access fields of data items dynamically.
    /// </summary>
    internal class PageCustomFieldsAccessor : DynamicObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageCustomFieldsAccessor"/> class.
        /// </summary>
        /// <param name="node">The sitemap node.</param>
        public PageCustomFieldsAccessor(PageSiteNode node)
            : base()
        {
            this.node = node;
        }

        /// <summary>
        /// Provides the implementation for operations that get member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as getting a value for a property.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name="result" />.</param>
        /// <returns>
        /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)
        /// </returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (binder == null)
                throw new ArgumentNullException("binder");

            var name = binder.Name;
            result = this.GetMemberValue(name);

            return TypeDescriptor.GetProperties(typeof(PageNode))[name] != null;
        }

        /// <summary>
        /// Gets the member value.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>The value of the member.</returns>
        public object GetMemberValue(string fieldName)
        {
            object value = null;

            var propInfo = TypeDescriptor.GetProperties(typeof(PageNode))[fieldName];
            if (propInfo != null)
            {
                var relatedDataInfo = propInfo as RelatedDataPropertyDescriptor;
                if (relatedDataInfo == null)
                {
                    value = this.node.GetCustomFieldValue(fieldName);
                    var fieldType = propInfo.GetFieldType();
                    if (fieldType != null && fieldType == UserFriendlyDataType.LongText)
                    {
                        var stringValue = this.GetAppropriateStringValue(value);
                        value = HtmlFilterProvider.ApplyFilters(stringValue);
                    }
                }
                else
                {
                    value = propInfo.GetValue(this.node.RelatedDataHolder);
                }
            }

            return value;
        }

        private string GetAppropriateStringValue(object value)
        {
            string stringValue = value as Lstring;
            if (stringValue != null)
            {
                return stringValue;
            }

            return value as string;
        }

        private PageSiteNode node;
    }
}
