using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        /// <inheritDoc/>
        public string CssClass { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsModel"/> class.
        /// </summary>
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
        public bool ThreadIsClosed { get; set; }

        /// <inheritDoc/>
        [Browsable(false)]
        public bool ShowComments
        {
            get
            {
                return (this.AllowComments.HasValue ? this.AllowComments.Value : this.ThreadsConfig.AllowComments);
            }
        }

        /// <summary>
        /// Gets the configuration for the thread
        /// </summary>
        [Browsable(false)]
        public ThreadsConfigModel ThreadsConfig
        {
            get
            {
                if (this.threadsConfig == null)
                {
                    this.threadsConfig = new ThreadsConfigModel(this.ThreadType);
                }
                return this.threadsConfig;
            }
        }

        /// <summary>
        /// Gets the configuration for the comments module
        /// </summary>
        [Browsable(false)]
        public CommentsConfigModel CommentsConfig
        {
            get
            {
                if (this.commentsConfig == null)
                {
                    this.commentsConfig = new CommentsConfigModel();
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

            return thread;
        }

        private string threadType;
        private string threadTitle;
        private string groupKey;
        private string threadKey;
        private ThreadsConfigModel threadsConfig;
        private CommentsConfigModel commentsConfig;
    }
}
