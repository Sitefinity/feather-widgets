using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Frontend.Mvc.Models;

namespace DynamicContent.Mvc.Models
{
    /// <summary>
    /// View model for the list view of the Dynamic Content widget.
    /// </summary>
    public class DynamicContentListViewModel : ContentListViewModel
    {
        /// <summary>
        /// Gets or sets the currently selected item.
        /// </summary>
        public Telerik.Sitefinity.DynamicModules.Model.DynamicContent SelectedItem { get; set; }
    }
}
