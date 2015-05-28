using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.CommentsAndReviews
{
    /// <summary>
    /// This is the entry point class for comments and reviews common wrapper.
    /// </summary>
    public abstract class CommentsAndReviewsCommonWrapper : BaseWrapper
    {
        /// <summary>
        /// Close comments for thread
        /// </summary>
        public void CloseCommentsAndReviewForThread(string isThreadCloseed)
        {
            ActiveBrowser.RefreshDomTree();

            HtmlInputControl threadClosed = this.EM.CommentsAndReviews.CommentsEditScreen.ThreadIdsClosed
                .AssertIsPresent("Close thread");

            threadClosed.ScrollToVisible();
            threadClosed.Focus();
            threadClosed.MouseClick();

            Manager.Current.Desktop.KeyBoard.KeyDown(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.A);
            Manager.Current.Desktop.KeyBoard.KeyUp(System.Windows.Forms.Keys.Control);
            Manager.Current.Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Delete);

            Manager.Current.Desktop.KeyBoard.TypeText(isThreadCloseed);
        }
    }
}
