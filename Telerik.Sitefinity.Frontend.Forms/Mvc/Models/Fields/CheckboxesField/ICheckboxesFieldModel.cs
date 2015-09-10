﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.CheckboxesField
{
    /// <summary>
    /// This interface provides API for form multiple choice fields.
    /// </summary>
    public interface ICheckboxesFieldModel : IFormFieldModel
    {
        /// <summary>
        /// Gets or sets the serialized choices.
        /// </summary>
        /// <value>
        /// The serialized choices.
        /// </value>
        string SerializedChoices { get; set; }

        /// <summary>
        /// Gets or sets the CSS class.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string CssClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has other choice.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has other choice; otherwise, <c>false</c>.
        /// </value>
        bool HasOtherChoice { get; set; }

        /// <summary>
        /// Gets or sets the meta field.
        /// </summary>
        /// <value>
        /// The meta field.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        IMetaField MetaField { get; set; }

        /// <summary>
        /// Gets or sets a validation mechanism for the field.
        /// </summary>
        /// <value>The validation.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        ValidatorDefinition ValidatorDefinition { get; set; }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <returns></returns>
        CheckboxesFieldViewModel GetViewModel(object value, IMetaField metaField);
    }
}
