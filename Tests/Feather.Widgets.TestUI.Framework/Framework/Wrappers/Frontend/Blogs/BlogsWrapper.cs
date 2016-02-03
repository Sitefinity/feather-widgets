using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    /// <summary>
    /// This is the entry point class for blogs widget on the frontend.
    /// </summary>
    public class BlogsWrapper : BaseWrapper
    {
        /// <summary>
        /// Verifies the blog posts titles on the page frontend.
        /// </summary>
        /// <param name="blogsPostTitles">The blog post titles.</param>
        /// <returns>true or false depending on blog post titles presence on frontend</returns>
        public bool IsBlogPostTitlesPresentOnThePageFrontend(string[] blogsPostTitles)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();

            for (int i = 0; i < blogsPostTitles.Length; i++)
            {
                HtmlAnchor newsAnchor = frontendPageMainDiv.Find.ByExpression<HtmlAnchor>("tagname=a", "InnerText=" + blogsPostTitles[i]);
                if ((newsAnchor == null) || (newsAnchor != null && !newsAnchor.IsVisible()))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
