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
        /// Gets word : Placeholder
        /// </summary>
        [ResourceEntry("PlaceholderText",
            Value = "Placeholder",
            Description = "word : Placeholder",
            LastModified = "2015/08/03")]
        public string PlaceholderText
        {
            get
            {
                return this["PlaceholderText"];
            }
        }

        /// <summary>
        /// Gets word : Untitled
        /// </summary>
        [ResourceEntry("Untitled",
            Value = "Untitled",
            Description = "word : Untitled",
            LastModified = "2015/08/26")]
        public string Untitled
        {
            get
            {
                return this["Untitled"];
            }
        }

        /// <summary>
        /// Gets phrase : Select a choice
        /// </summary>
        [ResourceEntry("SelectChoice",
            Value = "Select a choice",
            Description = "phrase : Select a choice",
            LastModified = "2015/09/03")]
        public string SelectChoice
        {
            get
            {
                return this["SelectChoice"];
            }
        }

        /// <summary>
        /// Gets phrase : Select choices
        /// </summary>
        [ResourceEntry("SelectChoices",
            Value = "Select choices",
            Description = "phrase : Select choices",
            LastModified = "2015/09/03")]
        public string SelectChoices
        {
            get
            {
                return this["SelectChoices"];
            }
        }

        /// <summary>
        /// Gets phrase : Label (Question)
        /// </summary>
        [ResourceEntry("LabelQuestion",
            Value = "Label (Question)",
            Description = "phrase : Label (Question)",
            LastModified = "2015/09/03")]
        public string LabelQuestion
        {
            get
            {
                return this["LabelQuestion"];
            }
        }

        /// <summary>
        /// Gets word : Choices
        /// </summary>
        [ResourceEntry("Choices",
            Value = "Choices",
            Description = "word : Choices",
            LastModified = "2015/09/03")]
        public string Choices
        {
            get
            {
                return this["Choices"];
            }
        }

        /// <summary>
        /// Gets word : Default
        /// </summary>
        [ResourceEntry("Default",
            Value = "Default",
            Description = "word : Default",
            LastModified = "2015/09/03")]
        public string Default
        {
            get
            {
                return this["Default"];
            }
        }

        /// <summary>
        /// Gets phrase : Enter label
        /// </summary>
        [ResourceEntry("EnterLabel",
            Value = "Enter label",
            Description = "phrase : Enter label",
            LastModified = "2015/09/03")]
        public string EnterLabel
        {
            get
            {
                return this["EnterLabel"];
            }
        }

        /// <summary>
        /// Gets phrase : Click to add a choice
        /// </summary>
        [ResourceEntry("ClickToAddChoice",
            Value = "Click to add a choice",
            Description = "phrase : Click to add a choice",
            LastModified = "2015/09/03")]
        public string ClickToAddChoice
        {
            get
            {
                return this["ClickToAddChoice"];
            }
        }

        /// <summary>
        /// Gets phrase : (Default choice have to be changed)
        /// </summary>
        [ResourceEntry("DefaultChoiceHaveToBeChanged",
            Value = "(Default choice have to be changed)",
            Description = "phrase : (Default choice have to be changed)",
            LastModified = "2015/09/03")]
        public string DefaultChoiceHaveToBeChanged
        {
            get
            {
                return this["DefaultChoiceHaveToBeChanged"];
            }
        }

        /// <summary>
        /// Gets phrase : Make default
        /// </summary>
        [ResourceEntry("MakeDefault",
            Value = "Make default",
            Description = "phrase : Make default",
            LastModified = "2015/09/03")]
        public string MakeDefault
        {
            get
            {
                return this["MakeDefault"];
            }
        }

        /// <summary>
        /// Gets phrase : Error message displayed if choice is not made
        /// </summary>
        [ResourceEntry("ErrorMessageDisplayedLabel",
            Value = "Error message displayed if choice is not made",
            Description = "phrase : Error message displayed if choice is not made",
            LastModified = "2015/09/03")]
        public string ErrorMessageDisplayedLabel
        {
            get
            {
                return this["ErrorMessageDisplayedLabel"];
            }
        }

        /// <summary>
        /// Gets phrase : - Select -
        /// </summary>
        [ResourceEntry("OptionSelect",
            Value = "- Select -",
            Description = "phrase : - Select -",
            LastModified = "2015/09/03")]
        public string OptionSelect
        {
            get
            {
                return this["OptionSelect"];
            }
        }

        /// <summary>
        /// Gets phrase : First choice
        /// </summary>
        [ResourceEntry("OptionFirstChoice",
            Value = "First choice",
            Description = "phrase : First choice",
            LastModified = "2015/09/03")]
        public string OptionFirstChoice
        {
            get
            {
                return this["OptionFirstChoice"];
            }
        }

        /// <summary>
        /// Gets phrase : Second choice
        /// </summary>
        [ResourceEntry("OptionSecondChoice",
            Value = "Second choice",
            Description = "phrase : Second choice",
            LastModified = "2015/09/03")]
        public string OptionSecondChoice
        {
            get
            {
                return this["OptionSecondChoice"];
            }
        }

        /// <summary>
        /// Gets phrase : Third choice
        /// </summary>
        [ResourceEntry("OptionThirdChoice",
            Value = "Third choice",
            Description = "phrase : Third choice",
            LastModified = "2015/09/04")]
        public string OptionThirdChoice
        {
            get
            {
                return this["OptionThirdChoice"];
            }
        }
        
        /// <summary>
        /// Gets phrase : Add "Other" as a last choice
        /// </summary>
        [ResourceEntry("AddOtherLastChoice",
            Value = "Add \"Other\" as a last choice",
            Description = "phrase : Add \"Other\" as a last choice",
            LastModified = "2015/09/04")]
        public string AddOtherLastChoice
        {
            get
            {
                return this["AddOtherLastChoice"];
            }
        }

        /// <summary>
        /// Gets phrase : (expanding a text box)
        /// </summary>
        [ResourceEntry("ExpandingTextBox",
            Value = "(expanding a text box)",
            Description = "phrase : (expanding a text box)",
            LastModified = "2015/09/04")]
        public string ExpandingTextBox
        {
            get
            {
                return this["ExpandingTextBox"];
            }
        }

        /// <summary>
        /// Gets phrase : Other...
        /// </summary>
        [ResourceEntry("Other",
            Value = "Other...",
            Description = "phrase : Other...",
            LastModified = "2015/09/04")]
        public string Other
        {
            get
            {
                return this["Other"];
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
        /// Gets phrase : This field is required
        /// </summary>
        [ResourceEntry("RequiredErrorMessageValue",
            Value = "This field is required",
            Description = "phrase : This field is required",
            LastModified = "2015/08/26")]
        public string RequiredErrorMessageValue
        {
            get
            {
                return this["RequiredErrorMessageValue"];
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
        /// Gets phrase : Entered text is too long
        /// </summary>
        [ResourceEntry("TooLargeErrorMessageValue",
            Value = "Entered text is too long",
            Description = "phrase : Entered text is too long",
            LastModified = "2015/08/26")]
        public string TooLargeErrorMessageValue
        {
            get
            {
                return this["TooLargeErrorMessageValue"];
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

        /// <summary>
        /// Gets word : Type
        /// </summary>
        [ResourceEntry("Type",
            Value = "Type",
            Description = "word : Type",
            LastModified = "2015/08/25")]
        public string Type
        {
            get
            {
                return this["Type"];
            }
        }

        /// <summary>
        /// Gets word : Submit
        /// </summary>
        [ResourceEntry("SubmitButtonLabel",
            Value = "Submit",
            Description = "word : Submit",
            LastModified = "2015/08/24")]
        public string SubmitButtonLabel
        {
            get
            {
                return this["SubmitButtonLabel"];
            }
        }

        /// <summary>
        /// Gets phrase : Entry is not valid!
        /// </summary>
        [ResourceEntry("InvalidEntryMessage",
            Value = "Entry is not valid!",
            Description = "phrase: Entry is not valid!",
            LastModified = "2015/09/07")]
        public string InvalidEntryMessage
        {
            get
            {
                return this["InvalidEntryMessage"];
            }
        }

        #region ReCAPTCHA

        /// <summary>
        /// Gets phrase : What's this?
        /// </summary>
        [ResourceEntry("WhatsThis",
            Value = "What's this?",
            Description = "phrase : What's this?",
            LastModified = "2015/08/28")]
        public string WhatsThis
        {
            get
            {
                return this["WhatsThis"];
            }
        }

        /// <summary>
        /// Gets description for reCapthca widget
        /// </summary>
        [ResourceEntry("WhatsRecaptchaDescription",
            Value = "reCAPTCHA is a free service by Google to protect your website from spam and abuse.",
            Description = "Description for reCapthca widget",
            LastModified = "2015/08/28")]
        public string WhatsRecaptchaDescription
        {
            get
            {
                return this["WhatsRecaptchaDescription"];
            }
        }

        /// <summary>
        /// Gets phrase : Request unauthenticated users only
        /// </summary>
        [ResourceEntry("RequestUnauthenticatedUsersOnly",
            Value = "Request unauthenticated users only",
            Description = "phrase : Request unauthenticated users only",
            LastModified = "2015/08/28")]
        public string RequestUnauthenticatedUsersOnly
        {
            get
            {
                return this["RequestUnauthenticatedUsersOnly"];
            }
        }

        /// <summary>
        /// Gets word : Light
        /// </summary>
        [ResourceEntry("Light",
            Value = "Light",
            Description = "word : Light",
            LastModified = "2015/08/28")]
        public string Light
        {
            get
            {
                return this["Light"];
            }
        }

        /// <summary>
        /// Gets word : Dark
        /// </summary>
        [ResourceEntry("Dark",
            Value = "Dark",
            Description = "word : Dark",
            LastModified = "2015/08/28")]
        public string Dark
        {
            get
            {
                return this["Dark"];
            }
        }

        /// <summary>
        /// Gets word : Theme
        /// </summary>
        [ResourceEntry("Theme",
            Value = "Theme",
            Description = "word : Theme",
            LastModified = "2015/08/28")]
        public string Theme
        {
            get
            {
                return this["Theme"];
            }
        }

        /// <summary>
        /// Gets word : Image
        /// </summary>
        [ResourceEntry("Image",
            Value = "Image",
            Description = "word : Image",
            LastModified = "2015/08/28")]
        public string Image
        {
            get
            {
                return this["Image"];
            }
        }

        /// <summary>
        /// Gets word : Audio
        /// </summary>
        [ResourceEntry("Audio",
            Value = "Audio",
            Description = "word : Audio",
            LastModified = "2015/08/28")]
        public string Audio
        {
            get
            {
                return this["Audio"];
            }
        }

        /// <summary>
        /// Gets word : Size
        /// </summary>
        [ResourceEntry("Size",
            Value = "Size",
            Description = "word : Size",
            LastModified = "2015/08/28")]
        public string Size
        {
            get
            {
                return this["Size"];
            }
        }

        /// <summary>
        /// Gets word : Normal
        /// </summary>
        [ResourceEntry("Normal",
            Value = "Normal",
            Description = "word : Normal",
            LastModified = "2015/08/28")]
        public string Normal
        {
            get
            {
                return this["Normal"];
            }
        }

        /// <summary>
        /// Gets word : Compact
        /// </summary>
        [ResourceEntry("Compact",
            Value = "Compact",
            Description = "word : Compact",
            LastModified = "2015/08/28")]
        public string Compact
        {
            get
            {
                return this["Compact"];
            }
        }

        /// <summary>
        /// Gets phrase : Learn more.
        /// </summary>
        [ResourceEntry("LearnMore",
            Value = "Learn more",
            Description = "phrase : Learn more",
            LastModified = "2015/08/28")]
        public string LearnMore
        {
            get
            {
                return this["LearnMore"];
            }
        }

        /// <summary>
        /// Gets word : Preview.
        /// </summary>
        [ResourceEntry("Preview",
            Value = "Preview",
            Description = "word : Preview",
            LastModified = "2015/08/31")]
        public string Preview
        {
            get
            {
                return this["Preview"];
            }
        }

        #endregion

        #region Section header

        /// <summary>
        /// Gets phrase : Enter section header here...
        /// </summary>
        [ResourceEntry("SectionHeaderPlaceholderText",
            Value = "Enter section header here...",
            Description = "phrase : Enter section header here...",
            LastModified = "2015/09/01")]
        public string SectionHeaderPlaceholderText
        {
            get
            {
                return this["SectionHeaderPlaceholderText"];
            }
        }

        #endregion
    }
}
