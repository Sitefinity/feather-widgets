using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Comments;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.Services.Comments.Proxies;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Comments.Mvc.Models
{
    /// <summary>
    /// Provides API for working with <see cref="Telerik.Sitefinity.Services.Comments.IComment"/> items.
    /// </summary>
    public class CommentsModel : ICommentsModel
    {
        public CommentsModel()
        {
            this.GetThread();
        }

        /// <inheritDoc/>
        public string ThreadKey
        {
            get
            {
                if (this.threadKey.IsNullOrEmpty() && SiteMapBase.GetActualCurrentNode() != null)
                {
                    this.threadKey = ControlUtilities.GetLocalizedKey(SiteMapBase.GetActualCurrentNode().Id, null, CommentsBehaviorUtilities.GetLocalizedKeySuffix(this.ThreadType));
                }

                return this.threadKey;
            }
            set
            {
                this.threadKey = value;
            }
        }

        /// <inheritDoc/>
        public string ThreadType
        {
            get
            {
                if (this.threadType.IsNullOrEmpty())
                {
                    this.threadType = typeof(PageNode).ToString();
                }

                return this.threadType;
            }
            set 
            {
                this.threadType = value;
            }
        }

        /// <inheritDoc/>
        public string GroupKey
        {
            get
            {
                if (this.groupKey.IsNullOrEmpty())
                {
                    this.groupKey = SystemManager.CurrentContext.CurrentSite.Id.ToString();
                }

                return this.groupKey;
            }
            set
            {
                this.groupKey = value;
            }
        }

        /// <inheritDoc/>
        public string ThreadTitle
        {
            get
            {
                if (this.threadTitle.IsNullOrEmpty() && SiteMapBase.GetActualCurrentNode() != null)
                {
                    this.threadTitle = SiteMapBase.GetActualCurrentNode().Title;
                }

                return this.threadTitle;
            }
            set
            {
                this.threadTitle = value;
            }
        }

        /// <inheritDoc/>
        public string DataSource { get; set; }

        /// <inheritDoc/>
        public bool? AllowComments { get; set; }

        /// <inheritDoc/>
        public bool RestrictToAuthenticated { set; get; }

        /// <inheritDoc/>
        public bool ThreadIsClosed { get; set; }

        /// <summary>
        /// Gets the Comments Settings element
        /// </summary>
        private CommentsConfigModel CommentsConfig
        {
            get
            {
                if (this.commentsConfig == null)
                {
                    this.commentsConfig = new CommentsConfigModel(this.ThreadType);
                }
                return this.commentsConfig;
            }
        }

        private IThread GetThread()
        {
            var cs = SystemManager.GetCommentsService();
            IThread thread = null;
            if (!this.ThreadKey.IsNullOrEmpty())
            {
                thread = cs.GetThread(this.ThreadKey);
            }

            if (thread != null)
            {
                this.ThreadIsClosed = thread.IsClosed;
            }
            else
            {
                var currentUser = Telerik.Sitefinity.Security.Claims.ClaimsManager.GetCurrentIdentity();
                var author = new AuthorProxy(currentUser.UserId.ToString());

                thread = new ThreadProxy(this.ThreadTitle, this.ThreadType, this.GroupKey, author)
                {
                    IsClosed = false,
                    Key = this.ThreadKey,
                    DataSource = this.DataSource
                };

                if (this.CommentsConfig.EnableRatings)
                {
                    thread.Behavior = CommentsBehaviorUtilities.ReviewBehaviorIdent;
                }
            }

            return thread;
        }

        private string threadType;
        private string threadTitle;
        private string groupKey;
        private string threadKey;
        private CommentsConfigModel commentsConfig;
    }
}
