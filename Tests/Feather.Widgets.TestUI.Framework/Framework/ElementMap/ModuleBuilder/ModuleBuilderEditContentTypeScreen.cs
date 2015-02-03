using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.ModuleBuilder
{
    /// <summary>
    /// Provides access to Module builder Edit content type screen
    /// </summary>
    public class ModuleBuilderEditContentTypeScreen : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleBuilderEditContentTypeScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public ModuleBuilderEditContentTypeScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets the delete link in content type edit screen.
        /// </summary>
        public HtmlAnchor DeleteLink
        {
            get
            {
                return this.Get<HtmlAnchor>("id=deleteButton", "InnerText=Delete");
            }
        }

        /// <summary>
        /// Gets the confirmation link when deleting a content type.
        /// </summary>
        public HtmlAnchor ConfirmationLink
        {
            get
            {
                return this.Find.ByExpression<HtmlAnchor>("class=sfLinkBtn sfDelete delete-confirm", "InnerText=Delete this content type and all its data");
            }
        }
    }
}
