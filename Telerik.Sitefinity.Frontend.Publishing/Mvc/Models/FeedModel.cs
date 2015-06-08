using System;
using System.Globalization;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Publishing.Mvc.Models
{
    /// <summary>
    /// This class represents the model used for Feed widget.
    /// </summary>
    public class FeedModel : IFeedModel
    {
        #region Properties

        /// <inheritdoc />
        public Guid FeedId { get; set; }

        /// <inheritdoc />
        public FeedInsertionOption InsertionOption { get; set; }

        /// <inheritdoc />
        public string TextToDisplay { get; set; }

        /// <inheritdoc />
        public bool OpenInNewWindow { get; set; }
        
        /// <inheritdoc />
        public string ToolTip { get; set; }

        /// <inheritdoc />
        public string CssClass { get; set; }

        #endregion

        public virtual FeedViewModel GetViewModel()
        {
            var viewModel = new FeedViewModel();
            viewModel.InsertionOption = this.InsertionOption;
            viewModel.CssClass = this.CssClass;

            if (this.FeedId != null && this.FeedId != Guid.Empty)
            {
                RssPipeSettings pipe = PublishingManager.GetManager().GetPipeSettings<RssPipeSettings>().Where(p => p.Id == this.FeedId).FirstOrDefault();
                if (pipe != null)
                {
                    string url = RouteHelper.ResolveUrl(String.Concat("~/", Config.Get<PublishingConfig>().FeedsBaseURl, "/", pipe.UrlName), UrlResolveOptions.Absolute);
                    string title = (!string.IsNullOrEmpty(this.TextToDisplay)) ? HttpUtility.HtmlEncode(this.TextToDisplay) : string.Empty;

                    if (this.InsertionOption == FeedInsertionOption.AddressBarOnly || this.InsertionOption == FeedInsertionOption.PageAndAddressBar)
                    {
                        viewModel.HeadLink = string.Format(
                            CultureInfo.CurrentCulture, 
                            @"<link rel=""alternate"" type=""application/rss+xml"" title=""{0}"" href=""{1}""/>", 
                            title, 
                            url);
                    }
                    if (this.InsertionOption == FeedInsertionOption.PageOnly || this.InsertionOption == FeedInsertionOption.PageAndAddressBar)
                    {
                        // TODO: what to do with iconClass?
                        string openInNewWindowCode = this.OpenInNewWindow ? "target=\"_blank\"" : "";
                        viewModel.Link = string.Format(
                            CultureInfo.CurrentCulture, 
                            @"<a href=""{0}"" title=""{1}"" {2}>{3}</a>", 
                            url, 
                            this.ToolTip, 
                            openInNewWindowCode, 
                            title);
                    }
                }
            }

            return viewModel;
        }
    }
}
