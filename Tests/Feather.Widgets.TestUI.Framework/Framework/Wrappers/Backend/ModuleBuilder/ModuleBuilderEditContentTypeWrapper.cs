using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.ModuleBuilder
{
    /// <summary>
    /// This class wraps the base operations related to edit content type screen.
    /// </summary>
    public class ModuleBuilderEditContentTypeWrapper : BaseWrapper
    {
        /// <summary>
        /// Deletes a content type from edit and confirms the delete message.
        /// </summary>
        public void DeleteContentType()
        {
            var deleteBtn = this.EM.ModuleBuilder.ModuleBuilderEditContentTypeScreen.DeleteLink
                .AssertIsPresent("Delete Button");

            deleteBtn.Click();

            var confirmation = this.EM.ModuleBuilder.ModuleBuilderEditContentTypeScreen.ConfirmationLink;

            confirmation.Wait.ForExists();
            confirmation.Wait.ForVisible();
            confirmation.Click();           
        }
    }
}
