using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Frontend.Mvc.Models;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.Models.Blog
{
    /// <summary>
    /// The view model for the detail page of <see cref="BlogController"/>
    /// </summary>
    public class BlogDetailsViewModel : ContentDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the blogs count.
        /// </summary>
        /// <value>
        /// The blogs count.
        /// </value>
        public int BlogsCount { get; set; }
    }
}
