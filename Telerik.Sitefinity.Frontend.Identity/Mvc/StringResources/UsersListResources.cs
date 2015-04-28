using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Users list widget
    /// </summary>
    [ObjectInfo(typeof(UsersListResources), ResourceClassId = "UsersListResources", Title = "UsersListResourcesTitle", Description = "UsersListResourcesDescription")]
    public class UsersListResources : Resource
    {
        #region Constructions
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersListResources"/> class.
        /// </summary>
        public UsersListResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersListResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public UsersListResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        /// <summary>
        /// Gets Title for the Users list widget resources class.
        /// </summary>
        [ResourceEntry("UsersListResourcesTitle",
            Value = "Users list widget resources",
            Description = "Title for the Users list widget resources class.",
            LastModified = "2015/04/27")]
        public string UsersListResourcesTitle
        {
            get
            {
                return this["UsersListResourcesTitle"];
            }
        }

        /// <summary>
        /// Gets Description for the Users list widget resources class.
        /// </summary>
        [ResourceEntry("UsersListResourcesDescription",
            Value = "Localizable strings for the Users list widget.",
            Description = "Description for the Users list widget resources class.",
            LastModified = "2015/04/27")]
        public string UsersListResourcesDescription
        {
            get
            {
                return this["UsersListResourcesDescription"];
            }
        }

        /// <summary>
        /// Gets the display.
        /// </summary>
        /// <value>The display.</value>
        [ResourceEntry("Display",
            Value = "Display",
            Description = "word : Display",
            LastModified = "2015/04/27")]
        public string Display
        {
            get
            {
                return this["Display"];
            }
        }

        /// <summary>
        /// Gets phrase : More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/04/27")]
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
            LastModified = "2015/04/27")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// Gets word : Content
        /// </summary>
        [ResourceEntry("Content",
            Value = "Content",
            Description = "word: Content",
            LastModified = "2015/04/27")]
        public string Content
        {
            get
            {
                return this["Content"];
            }
        }

        /// <summary>
        /// Gets phrase : List settings
        /// </summary>
        [ResourceEntry("ListSettings",
            Value = "List settings",
            Description = "phrase : List settings",
            LastModified = "2015/04/27")]
        public string ListSettings
        {
            get
            {
                return this["ListSettings"];
            }
        }

        /// <summary>
        /// Gets phrase : Single item settings
        /// </summary>
        [ResourceEntry("SingleItemSettings",
            Value = "Single item settings",
            Description = "phrase : Single item settings",
            LastModified = "2015/04/27")]
        public string SingleItemSettings
        {
            get
            {
                return this["SingleItemSettings"];
            }
        }

        /// <summary>
        /// Gets phrase : List template
        /// </summary>
        [ResourceEntry("ListTemplate",
            Value = "List template",
            Description = "phrase : List template",
            LastModified = "2015/04/27")]
        public string ListTemplate
        {
            get
            {
                return this["ListTemplate"];
            }
        }

        /// <summary>
        /// Gets phrase : Detail template
        /// </summary>
        [ResourceEntry("DetailTemplate",
            Value = "Detail template",
            Description = "phrase : Detail template",
            LastModified = "2015/04/27")]
        public string DetailTemplate
        {
            get
            {
                return this["DetailTemplate"];
            }
        }

        /// <summary>
        /// phrase : Which users to display?
        /// </summary>
        /// <value>Which users to display?</value>
        [ResourceEntry("UsersToDisplay",
            Value = "Which users to display?",
            Description = "phrase : Which users to display?",
            LastModified = "2015/04/27")]
        public string UsersToDisplay
        {
            get
            {
                return this["UsersToDisplay"];
            }
        }

        /// <summary>
        /// phrase : All registered users
        /// </summary>
        /// <value>All registered users</value>
        [ResourceEntry("AllRegisteredUsers",
            Value = "All registered users",
            Description = "phrase : All registered users",
            LastModified = "2015/04/27")]
        public string AllRegisteredUsers
        {
            get
            {
                return this["AllRegisteredUsers"];
            }
        }

        /// <summary>
        /// phrase : Selected users...
        /// </summary>
        /// <value>Selected users...</value>
        [ResourceEntry("SelectedUsers",
            Value = "Selected users...",
            Description = "phrase : Selected users...",
            LastModified = "2015/04/27")]
        public string SelectedUsers
        {
            get
            {
                return this["SelectedUsers"];
            }
        }

        /// <summary>
        /// phrase : Users by roles...
        /// </summary>
        /// <value>Users by roles...</value>
        [ResourceEntry("NarrowSelection",
            Value = "Users by roles...",
            Description = "phrase : Users by roles...",
            LastModified = "2015/04/27")]
        public string NarrowSelection
        {
            get
            {
                return this["NarrowSelection"];
            }
        }

        /// <summary>
        /// Gets phrase : Use paging
        /// </summary>
        [ResourceEntry("UsePaging",
            Value = "Use paging",
            Description = "phrase : Use paging",
            LastModified = "2015/04/27")]
        public string UsePaging
        {
            get
            {
                return this["UsePaging"];
            }
        }

        /// <summary>
        /// Gets phrase : Use limit
        /// </summary>
        [ResourceEntry("UseLimit",
            Value = "Use limit",
            Description = "phrase : Use limit",
            LastModified = "2015/04/27")]
        public string UseLimit
        {
            get
            {
                return this["UseLimit"];
            }
        }

        /// <summary>
        /// Gets phrase : No limit and paging
        /// </summary>
        [ResourceEntry("NoLimitPaging",
            Value = "No limit and paging",
            Description = "phrase : No limit and paging",
            LastModified = "2015/04/27")]
        public string NoLimitPaging
        {
            get
            {
                return this["NoLimitPaging"];
            }
        }

        /// <summary>
        /// Gets phrase : Divide the list on pages up to {TextBox} items per page
        /// </summary>
        [ResourceEntry("DivideToItemPerPage",
            Value = "Divide the list on pages up to {0} items per page",
            Description = "phrase : Divide the list on pages up to {TextBox} items per page",
            LastModified = "2015/04/27")]
        public string DivideToItemPerPage
        {
            get
            {
                return this["DivideToItemPerPage"];
            }
        }

        /// <summary>
        /// Gets phrase : Show only limited number of items {TextBox} items in total
        /// </summary>
        [ResourceEntry("ShowLimitedItems",
            Value = "Show only limited number of items {0} items in total",
            Description = "phrase : Show only limited number of items {TextBox} items in total",
            LastModified = "2015/04/27")]
        public string ShowLimitedItems
        {
            get
            {
                return this["ShowLimitedItems"];
            }
        }

        /// <summary>
        /// Gets phrase : Show all published items at once
        /// </summary>
        [ResourceEntry("ShowAllItems",
            Value = "Show all published items at once",
            Description = "phrase : Show all published items at once",
            LastModified = "2015/04/27")]
        public string ShowAllItems
        {
            get
            {
                return this["ShowAllItems"];
            }
        }

        /// <summary>
        /// phrase : Sort users
        /// </summary>
        /// <value>Sort users</value>
        [ResourceEntry("SortUsers",
            Value = "Sort users",
            Description = "phrase : Sort users",
            LastModified = "2015/04/27")]
        public string SortUsers
        {
            get
            {
                return this["SortUsers"];
            }
        }

        /// <summary>
        /// phrase : Last created
        /// </summary>
        /// <value>Last created</value>
        [ResourceEntry("LastCreated",
            Value = "Last created",
            Description = "phrase : Last created",
            LastModified = "2015/04/27")]
        public string LastCreated
        {
            get
            {
                return this["LastCreated"];
            }
        }

        /// <summary>
        /// Gets phrase : Last modified
        /// </summary>
        [ResourceEntry("LastModified",
            Value = "Last modified",
            Description = "phrase : Last modified",
            LastModified = "2015/04/27")]
        public string LastModified
        {
            get
            {
                return this["LastModified"];
            }
        }

        /// <summary>
        /// Gets phrase : Open single item in...
        /// </summary>
        [ResourceEntry("OpenSingleItem",
            Value = "Open single item in...",
            Description = "phrase : Open single item in...",
            LastModified = "2015/04/27")]
        public string OpenSingleItem
        {
            get
            {
                return this["OpenSingleItem"];
            }
        }

        /// <summary>
        /// Gets phrase : Auto-generated page
        /// </summary>
        [ResourceEntry("ShowInSamePage",
            Value = "Auto-generated page",
            Description = "phrase : Auto-generated page (with the same layout as the list page)",
            LastModified = "2015/04/27")]
        public string ShowInSamePage
        {
            get
            {
                return this["ShowInSamePage"];
            }
        }

        /// <summary>
        /// Gets phrase : (with the same layout as the list page)
        /// </summary>
        [ResourceEntry("ShowInSamePageNote",
            Value = "(with the same layout as the list page)",
            Description = "phrase : (with the same layout as the list page)",
            LastModified = "2015/04/27")]
        public string ShowInSamePageNote
        {
            get
            {
                return this["ShowInSamePageNote"];
            }
        }

        /// <summary>
        /// Gets phrase : Selected existing page...
        /// </summary>
        [ResourceEntry("ShowInExistingPage",
            Value = "Selected existing page...",
            Description = "phrase : Selected existing page...",
            LastModified = "2015/04/27")]
        public string ShowInExistingPage
        {
            get
            {
                return this["ShowInExistingPage"];
            }
        }

        /// <summary>
        /// Gets the Provider
        /// </summary>
        [ResourceEntry("Provider",
            Value = "Provider",
            Description = "Provider",
            LastModified = "2015/04/27")]
        public string Provider
        {
            get
            {
                return this["Provider"];
            }
        }

        /// <summary>
        /// Gets word : by
        /// </summary>
        [ResourceEntry("By",
            Value = "by",
            Description = "word: by",
            LastModified = "2015/04/27")]
        public string By
        {
            get
            {
                return this["By"];
            }
        }

        /// <summary>
        /// phrase: As set in Advanced mode
        /// </summary>
        /// <value>As set in Advanced mode</value>
        [ResourceEntry("AsSetInAdvancedMode",
            Value = "As set in Advanced mode",
            Description = "phrase: As set in Advanced mode",
            LastModified = "2015/01/12")]
        public string AsSetInAdvancedMode
        {
            get
            {
                return this["AsSetInAdvancedMode"];
            }
        }

        /// <summary>
        /// phrase: First name (A-Z)
        /// </summary>
        /// <value>First name (A-Z)</value>
        [ResourceEntry("ByFirstNameAZ",
            Value = "First name (A-Z)",
            Description = "phrase: First name (A-Z)",
            LastModified = "2015/04/27")]
        public string ByFirstNameAZ
        {
            get
            {
                return this["ByFirstNameAZ"];
            }
        }

        /// <summary>
        /// phrase: First name (Z-A)
        /// </summary>
        /// <value>First name (Z-A)</value>
        [ResourceEntry("ByFirstNameZA",
            Value = "First name (Z-A)",
            Description = "phrase: First name (Z-A)",
            LastModified = "2015/04/27")]
        public string ByFirstNameZA
        {
            get
            {
                return this["ByFirstNameZA"];
            }
        }

        /// <summary>
        /// phrase: Last name (A-Z)
        /// </summary>
        /// <value>Last name (A-Z)</value>
        [ResourceEntry("ByLastNameAZ",
            Value = "Last name (A-Z)",
            Description = "phrase: Last name (A-Z)",
            LastModified = "2015/04/27")]
        public string ByLastNameAZ
        {
            get
            {
                return this["ByLastNameAZ"];
            }
        }

        /// <summary>
        /// phrase: Last name (Z-A)
        /// </summary>
        /// <value>Last name (Z-A)</value>
        [ResourceEntry("ByLastNameZA",
            Value = "Last name (Z-A)",
            Description = "phrase: Last name (Z-A)",
            LastModified = "2015/04/27")]
        public string ByLastNameZA
        {
            get
            {
                return this["ByLastNameZA"];
            }
        }

        /// <summary>
        /// Gets the You are not logged in.
        /// </summary>
        /// <value>You are not logged in.</value>
        [ResourceEntry("NotLoggedInUser",
            Value = "You are not logged in.",
            Description = "phrase: You are not logged in.",
            LastModified = "2015/04/27")]
        public string NotLoggedInUser
        {
            get
            {
                return this["NotLoggedInUser"];
            }
        }

        /// <summary>
        /// phrase: The selected template cannot be used with this profile type. Select another template or edit this template.
        /// </summary>
        [ResourceEntry("TheSelectedTemplateCannotBeUsed",
            Value = "The selected template cannot be used with this profile type. Select another template or edit this template.",
            Description = "phrase: The selected template cannot be used with this profile type. Select another template or edit this template.",
            LastModified = "2015/05/04")]
        public string TheSelectedTemplateCannotBeUsed
        {
            get
            {
                return this["TheSelectedTemplateCannotBeUsed"];
            }
        }

        /// <summary>
        /// phrase: Which profile type to use?
        /// </summary>
        [ResourceEntry("SelectProfileType",
            Value = "Which profile type to use?",
            Description = "phrase: Which profile type to use?",
            LastModified = "2015/05/04")]
        public string SelectProfileType
        {
            get
            {
                return this["SelectProfileType"];
            }
        }
    }
}
