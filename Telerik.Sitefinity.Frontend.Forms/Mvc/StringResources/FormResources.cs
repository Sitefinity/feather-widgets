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

        #endregion
    }
}