using System.Collections.Generic;
using System.Linq;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;
using Telerik.TestUI.Core.ElementMap;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Forms
{
    /// <summary>
    /// Elements from Froms properties.
    /// </summary>
    public class FormsPropertiesScreenBaseFeather : Telerik.Sitefinity.TestUI.Framework.ElementMap.Forms.FormsPropertiesScreenBase
    {
        public FormsPropertiesScreenBaseFeather(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets the create Frame from the create form screen
        /// </summary>
        /// <returns></returns>
        public override FrameInfo GetHostedFrameInfo()
        {
            FrameInfo frameInfo = new FrameInfo() { Name = "create" };

            return frameInfo;
        }
           
        /// <summary>
        /// Gets Web framework list item
        /// </summary>
        public HtmlListItem WebFramework
        {
            get
            {
                return this.Get<HtmlListItem>("class=sfFormSeparator", "id=~FormFramework");
            }
        }

        /// <summary>
        /// Gets the Advanced button from the form screen.
        /// </summary>
        public HtmlAnchor AdvancedButton
        {
            get
            {
                return this.Find.AssociatedBrowser.GetControl<HtmlAnchor>("tagname=a", "TextContent=Advanced");
            }
        }
    }
}
