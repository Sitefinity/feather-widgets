using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.Models.BlogPost
{
    /// <summary>
    /// Mode for filtering blog posts by parent.
    /// </summary>
    public enum ParentFilterMode
    {
        All,
        Selected,
        CurrentlyOpen,
        NotApplicable
    }
}
