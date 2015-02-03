using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.SocialShare
{
    /// <summary>
    /// Element map for social share options in page editor and frontend
    /// </summary>
    public class SocialShareOptions : HtmlElementContainer
    {
          /// <summary>
        /// Initializes a new instance of the <see cref="SocialShareOptions" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public SocialShareOptions(Find find)
            : base(find)
        {
        }
    
        /// <summary>
        /// Gets the unordered list containing options.
        /// </summary>
        /// <value>The unordered list containing options.</value>
        public HtmlUnorderedList UnorderedListContainingOptions
        {
            get
            {
                return this.Get<HtmlUnorderedList>("class=list-inline sf-social-share");
            }
        }

        /// <summary>
        /// Gets the list of all options.
        /// </summary>
        /// <value>The list of all options.</value>
        public ICollection<HtmlListItem> ListOfAllOptions
        {
            get
            {
                return this.UnorderedListContainingOptions.Find.AllByExpression<HtmlListItem>("tagname=li");
            }
        }
    }
}
