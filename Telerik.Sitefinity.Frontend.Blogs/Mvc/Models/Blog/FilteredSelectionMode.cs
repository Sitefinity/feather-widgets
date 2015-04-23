using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.Models.Blog
{
    /// <summary>
    /// Specifies when <see cref="SelectionMode"/> is Filtered, By which value is it filtered
    /// </summary>
    public enum FilteredSelectionMode
    {
        MinPostsCount = 0,
        MaxPostsAge = 1
    }
}
