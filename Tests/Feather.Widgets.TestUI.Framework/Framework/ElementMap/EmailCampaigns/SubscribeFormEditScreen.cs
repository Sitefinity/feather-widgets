using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;
using System.Collections.Generic;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.EmailCampaigns
{
    /// <summary>
    /// Elements from Subscribe form edit screen.
    /// </summary>
    public class SubscribeFormEditScreen : HtmlElementContainer
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="SubscribeFormEditScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public SubscribeFormEditScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Selected existing page
        /// </summary>
        public HtmlInputRadioButton SelectedExistingPage
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("id=specificPage");
            }
        }

        /// <summary>
        /// Selected existing mail list
        /// </summary>
        public HtmlDiv ErrorMessageMailingList
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "ng-show=errors.missingSelectedItems");
            }
        }

        /// <summary>
        /// Selected existing page
        /// </summary>
        public HtmlDiv ErrorMessagePage
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "ng-show=errors.missingSelectedPage");
            }
        }

        /// <summary>
        /// Gets the "No items created yet." message.
        /// </summary>
        public HtmlDiv NoItemsCreatedMessage
        {
            get
            {
                return this.Get<HtmlDiv>("tagname=div", "InnerText=No items have been created yet.");
            }
        }

        /// <summary>
        /// Gets the mailing list items.
        /// </summary>
        /// <value>
        /// The mailing list items.
        /// </value>
        public ICollection<HtmlDiv> MailingListItems
        {
            get
            {
                return this.Find.AllByExpression<HtmlDiv>("tagName=div", "class=~list-group-item");
            }
        }
    }
}
