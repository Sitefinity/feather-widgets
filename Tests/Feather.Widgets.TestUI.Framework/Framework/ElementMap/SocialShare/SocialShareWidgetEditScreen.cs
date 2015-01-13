using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.SocialShare
{
    /// <summary>
    /// Provides access to social share widget screen
    /// </summary>
    public class SocialShareWidgetEditScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SocialShareWidgetEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public SocialShareWidgetEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets template selector.
        /// </summary>
        public HtmlSelect TemplateSelector
        {
            get
            {
                return this.Get<HtmlSelect>("id=socialShareTemplateName");
            }
        }
    }
}
