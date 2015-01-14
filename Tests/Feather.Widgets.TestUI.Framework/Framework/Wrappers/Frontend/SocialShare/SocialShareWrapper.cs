using ArtOfTest.WebAii.Controls.HtmlControls;
using System;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    /// <summary>
    /// Wrapper for SocialShare frontend
    /// </summary>
    public class SocialShareWrapper : BaseWrapper
    {
        /// <summary>
        /// Verifies the social share template content on the frontend.
        /// </summary>
        /// <param name="socialShareContent">Content of the social share.</param>
        /// <returns></returns>
        public bool IsSocialShareTemplateContentPresentOnTheFrontend(string[] socialShareContent)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            bool isContained = false;

            for (int i = 0; i < socialShareContent.Length; i++)
            {
                isContained = frontendPageMainDiv.InnerText.Contains(socialShareContent[i]);
                if (!isContained)
                {
                    return isContained;
                }
            }

            return isContained;
        }
    }
}
