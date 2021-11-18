using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Form widget
    /// </summary>
    [ObjectInfo(typeof(FormResources), ResourceClassId = "FormResources", Title = "FormResourcesTitle", Description = "FormResourcesDescription")]
    public class FormResources : Resource
    {
        #region Constructions

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsWidgetResources"/> class.
        /// </summary>
        public FormResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsWidgetResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public FormResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion

        #region Class Description

        /// <summary>
        /// Gets Title for the form widgets resources class.
        /// </summary>
        [ResourceEntry("FormResourcesTitle",
            Value = "Form widgets resources",
            Description = "MVC Form widgets resources class.",
            LastModified = "2015/10/19")]
        public string FormResourcesTitle
        {
            get
            {
                return this["FormResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Description for the form widgets resources class.
        /// </summary>
        [ResourceEntry("FormResourcesDescription",
            Value = "Form widgets resources",
            Description = "Contains resources for the form widgets resources class.",
            LastModified = "2015/10/19")]
        public string FormResourcesDescription
        {
            get
            {
                return this["FormResourcesDescription"];
            }
        }

        #endregion

        #region Designer Form

        /// <summary>
        /// Gets word : Form
        /// </summary>
        [ResourceEntry("Form",
            Value = "Form",
            Description = "word: Form",
            LastModified = "2015/08/05")]
        public string Form
        {
            get
            {
                return this["Form"];
            }
        }

        /// <summary>
        /// Gets phrase : Select a form
        /// </summary>
        [ResourceEntry("SelectForm",
            Value = "Select a form",
            Description = "phrase: Select a form",
            LastModified = "2015/08/05")]
        public string SelectForm
        {
            get
            {
                return this["SelectForm"];
            }
        }

        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/08/05")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// Gets phrase : CSS classes
        /// </summary>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "phrase : CSS classes",
            LastModified = "2015/08/05")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        #endregion

        #region Designer Settings

        /// <summary>
        /// Gets word : Settings
        /// </summary>
        [ResourceEntry("Settings",
            Value = "Settings",
            Description = "word: Settings",
            LastModified = "2015/08/05")]
        public string Settings
        {
            get
            {
                return this["Settings"];
            }
        }

        /// <summary>
        /// Gets phrase : Confirmation options
        /// </summary>
        [ResourceEntry("ConfirmationOptions",
            Value = "Confirmation options",
            Description = "phrase: Confirmation options",
            LastModified = "2015/08/05")]
        public string ConfirmationOptions
        {
            get
            {
                return this["ConfirmationOptions"];
            }
        }

        /// <summary>
        /// Gets phrase : Use custom confirmation
        /// </summary>
        [ResourceEntry("UseCustomConfirmation",
            Value = "Use custom confirmation",
            Description = "phrase: Use custom confirmation",
            LastModified = "2015/08/05")]
        public string UseCustomConfirmation
        {
            get
            {
                return this["UseCustomConfirmation"];
            }
        }

        /// <summary>
        /// Gets phrase : Show message for success
        /// </summary>
        [ResourceEntry("ShowMessageForSuccess",
            Value = "Show message for success",
            Description = "phrase: Show message for success",
            LastModified = "2015/08/05")]
        public string ShowMessageForSuccess
        {
            get
            {
                return this["ShowMessageForSuccess"];
            }
        }

        /// <summary>
        /// Gets phrase : Redirect to a page...
        /// </summary>
        [ResourceEntry("RedirectToAPage",
            Value = "Redirect to a page...",
            Description = "phrase: Redirect to a page...",
            LastModified = "2015/08/05")]
        public string RedirectToAPage
        {
            get
            {
                return this["RedirectToAPage"];
            }
        }

        /// <summary>
        /// Gets phrase : Thank you for filling out our form.
        /// </summary>
        [ResourceEntry("SuccessfullySubmittedMessage",
            Value = "Thank you for filling out our form.",
            Description = "phrase: Thank you for filling out our form.",
            LastModified = "2019/06/27")]
        public string SuccessfullySubmittedMessage
        {
            get
            {
                return this["SuccessfullySubmittedMessage"];
            }
        }

        /// <summary>
        /// Gets phrase : Entry is not valid!
        /// </summary>
        [ResourceEntry("UnsuccessfullySubmittedMessage",
            Value = "Entry is not valid!",
            Description = "phrase: Entry is not valid!",
            LastModified = "2015/08/27")]
        public string UnsuccessfullySubmittedMessage
        {
            get
            {
                return this["UnsuccessfullySubmittedMessage"];
            }
        }

        #endregion

        /// <summary>
        /// phrase: {0} field input is invalid
        /// </summary>
        /// <value>{0} field input is invalid</value>
        [ResourceEntry("InvalidInputErrorMessage",
            Value = "{0} field input is invalid",
            Description = "phrase: {0} field input is invalid",
            LastModified = "2017/03/15")]
        public string InvalidInputErrorMessage
        {
            get
            {
                return this["InvalidInputErrorMessage"];
            }
        }

        /// <summary>
        /// phrase: {0} field input is required
        /// </summary>
        /// <value>{0} field input is invalid</value>
        [ResourceEntry("RequiredInputErrorMessage",
            Value = "{0} field input is required",
            Description = "phrase: {0} field input is invalid",
            LastModified = "2018/07/17")]
        public string RequiredInputErrorMessage
        {
            get
            {
                return this["RequiredInputErrorMessage"];
            }
        }

        /// <summary>
        /// phrase: Email address is invalid
        /// </summary>
        /// <value>Email address is invalid</value>
        [ResourceEntry("InvalidEmailErrorMessage",
            Value = "Email address is invalid",
            Description = "phrase: Email address is invalidd",
            LastModified = "2019/07/24")]
        public string InvalidEmailErrorMessage
        {
            get
            {
                return this["InvalidEmailErrorMessage"];
            }
        }

        /// <summary>
        /// phrase: Email address is required
        /// </summary>
        /// <value>Email address is required</value>
        [ResourceEntry("RequiredEmailErrorMessage",
            Value = "Email address is required",
            Description = "phrase: Email address is required",
            LastModified = "2019/07/24")]
        public string RequiredEmailErrorMessage
        {
            get
            {
                return this["RequiredEmailErrorMessage"];
            }
        }

        /// <summary>
        /// phrase: {0} field input is required
        /// </summary>
        /// <value>{0} field input is invalid</value>
        [ResourceEntry("MaxLengthInputErrorMessage",
            Value = "{0} field input is too long",
            Description = "phrase: {0} field input is too long",
            LastModified = "2018/07/17")]
        public string MaxLengthInputErrorMessage
        {
            get
            {
                return this["MaxLengthInputErrorMessage"];
            }
        }

        /// <summary>
        /// phrase: {0} field must be less than {1} characters
        /// </summary>
        /// <value>{0} field input is invalid</value>
        [ResourceEntry("MaxLengthInputErrorMessageWithRange",
            Value = "{0} field must be less than {1} characters",
            Description = "phrase: {0} field must be less than {1} characters",
            LastModified = "2021/03/30")]
        public string MaxLengthInputErrorMessageWithRange
        {
            get
            {
                return this["MaxLengthInputErrorMessageWithRange"];
            }
        }

        /// <summary>
        /// FormsControl
        /// </summary>
        [ResourceEntry("FormsControlTitle",
            Value = "Form",
            Description = "The title of the FormsControl widget at content toolbox.",
            LastModified = "2019/06/03")]
        public string FormsControlTitle
        {
            get
            {
                return this["FormsControlTitle"];
            }
        }

        /// <summary>
        /// Control description: A control for displaying a selected form.
        /// </summary>
        [ResourceEntry("FormsControlDescription",
            Value = "Displaying of already created form",
            Description = "Control description: A control for displaying a selected form.",
            LastModified = "2019/06/03")]
        public string FormsControlDescription
        {
            get
            {
                return this["FormsControlDescription"];
            }
        }
    }
}