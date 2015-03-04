using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Profile widget
    /// </summary>
    [ObjectInfo(typeof(ProfileResources), Title = "ProfileResources", Description = "ProfileResources")]
    public class ProfileResources : Resource
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileResources"/> class. 
        /// Initializes new instance of <see cref="ProfileResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public ProfileResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public ProfileResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }

        #endregion

        /// <summary>
        /// Gets phrase : CSS classes
        /// </summary>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "phrase : CSS classes",
            LastModified = "2015/03/04")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// Gets phrase: Template
        /// </summary>
        [ResourceEntry("Template",
            Value = "Template",
            Description = "phrase : Template",
            LastModified = "2015/03/04")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }

        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/03/04")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// Gets phrase: Edit mode only
        /// </summary>
        [ResourceEntry("EditModeOnly",
            Value = "Edit mode only",
            Description = "phrase : Edit mode only",
            LastModified = "2015/03/04")]
        public string EditModeOnly
        {
            get
            {
                return this["EditModeOnly"];
            }
        }

        /// <summary>
        /// Gets phrase: Read mode only
        /// </summary>
        [ResourceEntry("ReadModeOnly",
            Value = "Read mode only",
            Description = "phrase : Read mode only",
            LastModified = "2015/03/04")]
        public string ReadModeOnly
        {
            get
            {
                return this["ReadModeOnly"];
            }
        }

        /// <summary>
        /// Gets phrase: Both: Read mode can be edited
        /// </summary>
        [ResourceEntry("BothModes",
            Value = "Both: Read mode can be edited",
            Description = "phrase : Both: Read mode can be edited",
            LastModified = "2015/03/04")]
        public string BothModes
        {
            get
            {
                return this["BothModes"];
            }
        }

        /// <summary>
        /// Gets phrase: When the changes are saved...
        /// </summary>
        [ResourceEntry("WhenChangesAreSaved",
            Value = "When the changes are saved...",
            Description = "phrase : When the changes are saved...",
            LastModified = "2015/03/04")]
        public string WhenChangesAreSaved
        {
            get
            {
                return this["WhenChangesAreSaved"];
            }
        }

        /// <summary>
        /// Gets phrase: Switch to 'Read mode'
        /// </summary>
        [ResourceEntry("SwitchToReadMode",
            Value = "Switch to 'Read mode'",
            Description = "phrase : Switch to 'Read mode'",
            LastModified = "2015/03/04")]
        public string SwitchToReadMode
        {
            get
            {
                return this["SwitchToReadMode"];
            }
        }

        /// <summary>
        /// Gets phrase: Show a message above the form
        /// </summary>
        [ResourceEntry("ShowMsg",
            Value = "Show a message above the form",
            Description = "phrase : Show message",
            LastModified = "2015/03/04")]
        public string ShowMsg
        {
            get
            {
                return this["ShowMsg"];
            }
        }

        /// <summary>
        /// Gets phrase: Open a specially prepared page...
        /// </summary>
        [ResourceEntry("OpenPage",
            Value = "Open a specially prepared page...",
            Description = "phrase : Open a specially prepared page...",
            LastModified = "2015/03/04")]
        public string OpenPage
        {
            get
            {
                return this["OpenPage"];
            }
        }
    }
}
