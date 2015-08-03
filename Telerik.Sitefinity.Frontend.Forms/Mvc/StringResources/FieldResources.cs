using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Form's field widgets
    /// </summary>
    [ObjectInfo(typeof(FieldResources), ResourceClassId = "FieldResources", Title = "FieldResourcesTitle", Description = "FieldResourcesDescription")]
    public class FieldResources : Resource
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldResources"/> class. 
        /// Initializes new instance of <see cref="FieldResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public FieldResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public FieldResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Class Description

        /// <summary>
        /// Gets Title for the form's field widgets resources class.
        /// </summary>
        [ResourceEntry("FieldResourcesTitle",
            Value = "Form's field widgets resources",
            Description = "Title for the form's field widgets resources class.",
            LastModified = "2015/08/03")]
        public string FieldResourcesTitle
        {
            get
            {
                return this["FieldResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Description for the form's field widgets resources class.
        /// </summary>
        [ResourceEntry("FieldResourcesDescription",
            Value = "Form's field widgets resources",
            Description = "Description for the form's field widgets resources class.",
            LastModified = "2015/08/03")]
        public string FieldResourcesDescription
        {
            get
            {
                return this["FieldResourcesDescription"];
            }
        }

        #endregion

        /// <summary>
        /// Gets phrase : CSS classes
        /// </summary>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "phrase : CSS classes",
            LastModified = "2015/08/03")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/08/03")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// Gets phrase : Label and text
        /// </summary>
        [ResourceEntry("LabelAndText",
            Value = "Label and text",
            Description = "phrase : Label and text",
            LastModified = "2015/08/03")]
        public string LabelAndText
        {
            get
            {
                return this["LabelAndText"];
            }
        }

        /// <summary>
        /// Gets phrase : Placeholder text
        /// </summary>
        [ResourceEntry("PlaceholderText",
            Value = "Placeholder text",
            Description = "phrase : Placeholder text",
            LastModified = "2015/08/03")]
        public string PlaceholderText
        {
            get
            {
                return this["PlaceholderText"];
            }
        }

        /// <summary>
        /// Gets phrase : Predefined value
        /// </summary>
        [ResourceEntry("PredefinedValue",
            Value = "Predefined value",
            Description = "phrase : Predefined value",
            LastModified = "2015/08/03")]
        public string PredefinedValue
        {
            get
            {
                return this["PredefinedValue"];
            }
        }

        /// <summary>
        /// Gets phrase : Instructional text
        /// </summary>
        [ResourceEntry("InstructionalText",
            Value = "Instructional text",
            Description = "phrase : Instructional text",
            LastModified = "2015/08/03")]
        public string InstructionalText
        {
            get
            {
                return this["InstructionalText"];
            }
        }

        /// <summary>
        /// Gets phrase : Add instructional text
        /// </summary>
        [ResourceEntry("AddInstructionalText",
            Value = "Add instructional text",
            Description = "phrase : Add instructional text",
            LastModified = "2015/08/03")]
        public string AddInstructionalText
        {
            get
            {
                return this["AddInstructionalText"];
            }
        }

        /// <summary>
        /// Gets phrase : Required field
        /// </summary>
        [ResourceEntry("RequiredField",
            Value = "Required field",
            Description = "phrase : Required field",
            LastModified = "2015/08/03")]
        public string RequiredField
        {
            get
            {
                return this["RequiredField"];
            }
        }

        /// <summary>
        /// Gets phrase : Error message displayed if the textbox is empty
        /// </summary>
        [ResourceEntry("RequiredErrorMessageTitle",
            Value = "Error message displayed if the textbox is empty",
            Description = "phrase : Error message displayed if the textbox is empty",
            LastModified = "2015/08/03")]
        public string RequiredErrorMessageTitle
        {
            get
            {
                return this["RequiredErrorMessageTitle"];
            }
        }

        /// <summary>
        /// Gets phrase : Error message displayed when the entry is out of range
        /// </summary>
        [ResourceEntry("TooLargeErrorMessageTitle",
            Value = "Error message displayed when the entry is out of range",
            Description = "phrase : Error message displayed when the entry is out of range",
            LastModified = "2015/08/03")]
        public string TooLargeErrorMessageTitle
        {
            get
            {
                return this["TooLargeErrorMessageTitle"];
            }
        }

        /// <summary>
        /// Gets word : Limitations
        /// </summary>
        [ResourceEntry("Limitations",
            Value = "Limitations",
            Description = "word : Limitations",
            LastModified = "2015/08/03")]
        public string Limitations
        {
            get
            {
                return this["Limitations"];
            }
        }

        /// <summary>
        /// Gets word : Label
        /// </summary>
        [ResourceEntry("Label",
            Value = "Label",
            Description = "word : Label",
            LastModified = "2015/08/03")]
        public string Label
        {
            get
            {
                return this["Label"];
            }
        }

        /// <summary>
        /// Gets phrase : Example: First name
        /// </summary>
        [ResourceEntry("ExampleTitle",
            Value = "Example: First name",
            Description = "phrase : Example: <i>First name</i>",
            LastModified = "2015/08/03")]
        public string ExampleTitle
        {
            get
            {
                return this["ExampleTitle"];
            }
        }

        /// <summary>
        /// Gets phrase : Suitable for giving examples and instructions how the entered value will be used
        /// </summary>
        [ResourceEntry("ExampleExplanation",
            Value = "Suitable for giving examples and instructions how the entered value will be used",
            Description = "phrase : Suitable for giving examples and instructions how the entered value will be used",
            LastModified = "2015/08/03")]
        public string ExampleExplanation
        {
            get
            {
                return this["ExampleExplanation"];
            }
        }

        /// <summary>
        /// Gets word : Style
        /// </summary>
        [ResourceEntry("Style",
            Value = "Style",
            Description = "word : Style",
            LastModified = "2015/08/03")]
        public string Style
        {
            get
            {
                return this["Style"];
            }
        }

        /// <summary>
        /// Gets word : Template
        /// </summary>
        [ResourceEntry("Template",
            Value = "Template",
            Description = "word : Template",
            LastModified = "2015/08/03")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }

        /// <summary>
        /// Gets word : Range
        /// </summary>
        [ResourceEntry("Range",
            Value = "Range",
            Description = "word : Range",
            LastModified = "2015/08/03")]
        public string Range
        {
            get
            {
                return this["Range"];
            }
        }

        /// <summary>
        /// Gets word : Min
        /// </summary>
        [ResourceEntry("Min",
            Value = "Min",
            Description = "word : Min",
            LastModified = "2015/08/03")]
        public string Min
        {
            get
            {
                return this["Min"];
            }
        }

        /// <summary>
        /// Gets word : Max
        /// </summary>
        [ResourceEntry("Max",
            Value = "Max",
            Description = "word : Max",
            LastModified = "2015/08/03")]
        public string Max
        {
            get
            {
                return this["Max"];
            }
        }

        /// <summary>
        /// Gets word : Characters
        /// </summary>
        [ResourceEntry("Characters",
            Value = "characters",
            Description = "word : characters",
            LastModified = "2015/08/03")]
        public string Characters
        {
            get
            {
                return this["Characters"];
            }
        }
    }
}
