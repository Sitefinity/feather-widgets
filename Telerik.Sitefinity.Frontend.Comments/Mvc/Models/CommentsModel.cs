﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.ComponentModel;
using System.Web;
using Telerik.Sitefinity.Frontend.Comments.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Comments;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
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
        #region Properties
        
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

                // Changing the thread key should reset the thread.
                this.Thread = null;
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

                // Changing the tread type should reset the thread key and config.
                this.ThreadKey = null;
                this.threadConfig = null;
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
        public string DataSource { get; set; }

        /// <inheritDoc/>
        public int CommentTextMaxLength
        {
            get 
            {
                return this.commentTextMaxLength;
            }

            set 
            {
                this.commentTextMaxLength = value;
            }
        }

        /// <inheritDoc/>
        public bool ThreadIsClosed
        {
            get
            {
                return this.threadIsClosed || (this.Thread != null && this.Thread.IsClosed);
            }
            set
            {
                this.threadIsClosed = value;
            }
        }

        /// <inheritDoc/>
        public string CssClass { get; set; }

        /// <inheritDoc/>
        public bool CommentsAutoRefresh
        {
            get { return this.commentsAutoRefresh; }
            set { this.commentsAutoRefresh = value; }
        }

        /// <inheritDoc/>
        public int CommentsRefreshInterval
        {
            get { return this.commentsRefreshInterval; }
            set { this.commentsRefreshInterval = value; }
        }

        /// <inheritDoc/>
        [Browsable(false)]
        protected string LoginPageUrl
        {
            get
            {
                var loginRedirectUrl = string.Format("{0}?ReturnUrl={1}", this.GetDefaultLoginUrl(), HttpUtility.UrlEncode(Telerik.Sitefinity.Web.UrlPath.ResolveAbsoluteUrl(SystemManager.CurrentHttpContext.Request.Url.AbsoluteUri)));

                return loginRedirectUrl;
            }
        }

        /// <inheritDoc/>
        [Browsable(false)]
        protected string UserAvatarImageUrl
        {
            get
            {
                Libraries.Model.Image avatarImage;
                return this.sitefinityUserDisplayNameBuilder.Value.GetAvatarImageUrl(Sitefinity.Security.SecurityManager.CurrentUserId, out avatarImage);
            }
        }

        /// <inheritDoc/>
        [Browsable(false)]
        protected string UserDisplayName
        {
            get
            {
                return this.sitefinityUserDisplayNameBuilder.Value.GetUserDisplayName(Sitefinity.Security.SecurityManager.CurrentUserId);
            }
        }

        /// <inheritDoc/>
        [Browsable(false)]
        protected bool AllowComments
        {
            get
            {
                return this.ThreadConfig.AllowComments;
            }
        }

        /// <inheritDoc/>
        [Browsable(false)]
        protected IThread Thread
        {
            get
            {
                if (this.thread == null)
                {
                    if (!this.ThreadKey.IsNullOrEmpty())
                    {
                        var cs = SystemManager.GetCommentsService();
                        this.thread = cs.GetThread(this.ThreadKey);
                    }
                }

                return this.thread;
            }

            private set
            {
                this.thread = value;
            }
        }

        /// <inheritDoc/>
        [Browsable(false)]
        protected ThreadsConfigModel ThreadConfig
        {
            get
            {
                if (this.threadConfig == null)
                {
                    this.threadConfig = new ThreadsConfigModel(this.ThreadType);
                }

                return this.threadConfig;
            }
        }

        /// <inheritDoc/>
        [Browsable(false)]
        protected CommentsConfigModel CommentsConfig
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

        #endregion

        #region Methods

        /// <inheritDoc/>
        public CommentsListViewModel GetCommentsListViewModel(CommentsInputModel inputModel)
        {
            this.Initialize(inputModel);
            var allowComments = (inputModel != null && inputModel.AllowComments.HasValue) ? inputModel.AllowComments.Value : this.AllowComments;
            if (allowComments)
            {
                var widgetResources = this.GetCommentsListWidgetResources();
                var widgetSettings = this.GetCommentsListWidgetSettings(this.ThreadTitle);

                var jsonSerializerSettings = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                var viewModel = new CommentsListViewModel()
                {
                    AllowSubscription = this.ThreadConfig.AllowSubscription && !this.ThreadIsClosed,
                    CssClass = this.CssClass,
                    LoginPageUrl = this.LoginPageUrl,
                    ThreadIsClosed = this.ThreadIsClosed,
                    UserAvatarImageUrl = this.UserAvatarImageUrl,
                    RequiresAuthentication = this.ThreadConfig.RequiresAuthentication,
                    RequiresApproval = this.ThreadConfig.RequiresApproval,
                    RequiresCaptcha = this.CommentsConfig.UseSpamProtectionImage,
                    SerializedWidgetResources = JsonConvert.SerializeObject(widgetResources, Formatting.None, jsonSerializerSettings),
                    SerializedWidgetSettings = JsonConvert.SerializeObject(widgetSettings, Formatting.None, jsonSerializerSettings)
                };

                return viewModel;
            }
            else
            {
                return null;
            }
        }

        /// <inheritDoc/>
        public CommentsCountViewModel GetCommentsCountViewModel(CommentsCountInputModel inputModel)
        {
            this.ThreadKey = inputModel.ThreadKey;
            var allowComments = inputModel.AllowComments.HasValue ? inputModel.AllowComments.Value : this.AllowComments;
            if (allowComments)
            {
                var viewModel = new CommentsCountViewModel()
                {
                    NavigateUrl = inputModel.NavigateUrl,
                    ThreadKey = inputModel.ThreadKey
                };

                return viewModel;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Private Methods

        private void Initialize(CommentsInputModel commentsInputModel)
        {
            if (commentsInputModel == null)
                return;

            if (!string.IsNullOrEmpty(commentsInputModel.ThreadType))
            {
                this.ThreadType = commentsInputModel.ThreadType;
            }

            if (!string.IsNullOrEmpty(commentsInputModel.ThreadKey))
            {
                this.ThreadKey = commentsInputModel.ThreadKey;
            }

            if (!string.IsNullOrEmpty(commentsInputModel.ThreadTitle))
            {
                this.ThreadTitle = commentsInputModel.ThreadTitle;
            }

            if (!string.IsNullOrEmpty(commentsInputModel.GroupKey))
            {
                this.GroupKey = commentsInputModel.GroupKey;
            }

            if (!string.IsNullOrEmpty(commentsInputModel.DataSource))
            {
                this.DataSource = commentsInputModel.DataSource;
            }
        }

        private CommentsListWidgetResources GetCommentsListWidgetResources()
        {
            return new CommentsListWidgetResources()
            {
                CommentSingular = Res.Get<CommentsWidgetResources>().Comment,
                CommentsPlural = Res.Get<CommentsWidgetResources>().CommentsPlural,
                ReadFullComment = Res.Get<CommentsWidgetResources>().ReadFullComment,
                SubscribeLink = Res.Get<CommentsWidgetResources>().SubscribeLink,
                UnsubscribeLink = Res.Get<CommentsWidgetResources>().UnsubscribeLink,
                SubscribeToNewComments = Res.Get<CommentsWidgetResources>().SubscribeToNewComments,
                YouAreSubscribedToNewComments = Res.Get<CommentsWidgetResources>().YouAreSubscribedToNewComments,
                SuccessfullySubscribedToNewComments = Res.Get<CommentsWidgetResources>().SuccessfullySubscribedToNewComments,
                SuccessfullyUnsubscribedFromNewComments = Res.Get<CommentsWidgetResources>().SuccessfullyUnsubscribedFromNewComments
            };
        }

        private CommentsListWidgetSettings GetCommentsListWidgetSettings(string threadTitle)
        {
            var isUserAuthenticatedUrl =  RouteHelper.ResolveUrl("~/RestApi/session/is-authenticated", UrlResolveOptions.Rooted);
            var rootUrl = RouteHelper.ResolveUrl("~/RestApi/comments-api/", UrlResolveOptions.Rooted);

            return new CommentsListWidgetSettings()
            {
                CommentsAllowSubscription = this.ThreadConfig.AllowSubscription && !this.ThreadIsClosed,
                CommentsAutoRefresh = this.CommentsAutoRefresh,
                CommentsInitiallySortedDescending = this.CommentsConfig.AreNewestOnTop,
                CommentsPerPage = this.CommentsConfig.CommentsPerPage,
                CommentsRefreshInterval = this.CommentsRefreshInterval,
                CommentsTextMaxLength = this.CommentTextMaxLength,
                CommentsThread = this.Thread ?? this.GetCommentsThreadProxy(threadTitle),
                CommentsThreadKey = this.ThreadKey,
                CommentsThreadType = this.ThreadType,
                EnablePaging = this.CommentsConfig.EnablePaging,
                IsDesignMode = SystemManager.IsDesignMode,
                IsUserAuthenticatedUrl = isUserAuthenticatedUrl,
                RequiresApproval = this.ThreadConfig.RequiresApproval,
                RequiresAuthentication = this.ThreadConfig.RequiresAuthentication,
                RequiresCaptcha = this.CommentsConfig.UseSpamProtectionImage,
                RootUrl = rootUrl,
                UserAvatarImageUrl = this.UserAvatarImageUrl,
                UserDisplayName = this.UserDisplayName
            };
        }

        private IThread GetCommentsThreadProxy(string threadTitle)
        {
            var author = new AuthorProxy(SecurityManager.GetCurrentUserId().ToString());

            return new ThreadProxy(threadTitle, this.ThreadType, this.GroupKey, author)
            {
                IsClosed = false,
                Key = this.ThreadKey,
                DataSource = this.DataSource
            };
        }

        private string GetDefaultLoginUrl()
        {
            string defaultLoginPageUrl = string.Empty;
            var currentSite = Telerik.Sitefinity.Services.SystemManager.CurrentContext.CurrentSite;
            if (currentSite.FrontEndLoginPageId != Guid.Empty)
            {
                var manager = Telerik.Sitefinity.Modules.Pages.PageManager.GetManager();
                var redirectPage = manager.GetPageNode(currentSite.FrontEndLoginPageId);

                if (redirectPage != null)
                {
                    defaultLoginPageUrl = Telerik.Sitefinity.Modules.Pages.PageExtesnsions.GetUrl(redirectPage, String.Empty, null, true);
                }
            }
            else if (!string.IsNullOrWhiteSpace(currentSite.FrontEndLoginPageUrl))
            {
                defaultLoginPageUrl = currentSite.FrontEndLoginPageUrl;
            }

            return Telerik.Sitefinity.Web.UrlPath.ResolveAbsoluteUrl(defaultLoginPageUrl);
        }

        #endregion

        #region Private fields

        private string threadKey;
        private string threadType;
        private string threadTitle;
        private string groupKey;
        private int commentTextMaxLength = 100;
        private bool threadIsClosed;
        private IThread thread;
        private bool commentsAutoRefresh = false;
        private int commentsRefreshInterval = 3000;
        private ThreadsConfigModel threadConfig;
        private CommentsConfigModel commentsConfig;
        private Lazy<SitefinityUserDisplayNameBuilder> sitefinityUserDisplayNameBuilder = new Lazy<SitefinityUserDisplayNameBuilder>();

        #endregion
    }
}
