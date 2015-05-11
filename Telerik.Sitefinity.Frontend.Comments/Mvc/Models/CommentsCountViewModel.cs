using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Comments.Mvc.Models
{
    /// <summary>
    /// Provides view model for the comments count functionality.
    /// </summary>
    public class CommentsCountViewModel
    {
        /// <summary>
        /// Gets or sets the navigate URL.
        /// </summary>
        /// <value>
        /// The navigate URL.
        /// </value>
        public string NavigateUrl { get; set; }

        /// <summary>
        /// Gets or sets the thread key.
        /// </summary>
        /// <value>
        /// The thread key.
        /// </value>
        public string ThreadKey { set; get; }
    }
}
