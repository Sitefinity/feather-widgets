using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Comments.Mvc.Models
{
    public class CommentsCountViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsCountViewModel"/> class.
        /// </summary>
        /// <param name="navigateUrl">The navigate URL.</param>
        /// <param name="threadKey">The thread key.</param>
        public CommentsCountViewModel(string navigateUrl, string threadKey)
        {
            this.NavigateUrl = navigateUrl;
            this.ThreadKey = threadKey;
        }

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
        public string ThreadKey { set; get; }
    }
}
