using System;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.Classifications;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.CommentsAndReviews;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.Content;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.EmailCampaigns;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.Identity;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.Lists;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.Media;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.MediaGallery;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.ModuleBuilder;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.Navigation;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.ScriptsAndStyles;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.Search;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.Selectors;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.SocialShare;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.Widgets;
using Feather.Widgets.TestUI.Framework.Framework.ElementMap.WidgetTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap
{
    /// <summary>
    /// This is the entry point class for all Feather element maps.
    /// </summary>
    public class FeatherElementMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatherElementMap" /> class.
        /// </summary>
        public FeatherElementMap()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatherElementMap" /> class.
        /// </summary>
        /// <param name="find">The find object used to get the elements/controls.</param>
        public FeatherElementMap(Find find)
        {
            this.find = find;
        }

        /// <summary>
        /// Gets the content element map.
        /// </summary>
        /// <value>An initialized instance of generic content element map.</value>
        public ContentMap GenericContent
        {
            get
            {
                if (this.contentMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.contentMap = new ContentMap(this.find);
                }

                return this.contentMap;
            }

            private set
            {
                this.contentMap = value;
            }
        }

        /// <summary>
        /// Gets the navigation element map.
        /// </summary>
        /// <value>An initialized instance of navigation element map.</value>
        public NavigationMap Navigation
        {
            get
            {
                if (this.navigationMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.navigationMap = new NavigationMap(this.find);
                }

                return this.navigationMap;
            }

            private set
            {
                this.navigationMap = value;
            }
        }

        /// <summary>
        /// Gets the media element map.
        /// </summary>
        /// <value>An initialized instance of media element map.</value>
        public MediaMap Media
        {
            get
            {
                if (this.mediaMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.mediaMap = new MediaMap(this.find);
                }

                return this.mediaMap;
            }

            private set
            {
                this.mediaMap = value;
            }
        }

        /// <summary>
        /// Gets the social share element map.
        /// </summary>
        /// <value>An initialized instance of navigation element map.</value>
        public SocialShareMap SocialShare
        {
            get
            {
                if (this.socialShareMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.socialShareMap = new SocialShareMap(this.find);
                }

                return this.socialShareMap;
            }

            private set
            {
                this.socialShareMap = value;
            }
        }

        /// <summary>
        /// Gets the content widget designer element map.
        /// </summary>
        /// <value>An initialized instance of content widget designer element map.</value>
        public WidgetDesignerMap Widgets
        {
            get
            {
                if (this.widgetDesignerMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.widgetDesignerMap = new WidgetDesignerMap(this.find);
                }

                return this.widgetDesignerMap;
            }

            private set
            {
                this.widgetDesignerMap = value;
            }
        }

        /// <summary>
        /// Gets the module builder element map.
        /// </summary>
        /// <value>An initialized instance of module builder element map.</value>
        public ModuleBuilderMap ModuleBuilder
        {
            get
            {
                if (this.moduleBuilderMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.moduleBuilderMap = new ModuleBuilderMap(this.find);
                }

                return this.moduleBuilderMap;
            }

            private set
            {
                this.moduleBuilderMap = value;
            }
        }

        /// <summary>
        /// Gets the widget templates element map.
        /// </summary>
        /// <value>An initialized instance of widget template element map..</value>
        public WidgetTemplatesMap WidgetTemplates
        {
            get
            {
                if (this.widgetTemplatesMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.widgetTemplatesMap = new WidgetTemplatesMap(this.find);
                }

                return this.widgetTemplatesMap;
            }

            private set
            {
                this.widgetTemplatesMap = value;
            }
        }

        /// <summary>
        /// Gets the selectors element map.
        /// </summary>
        /// <value>An initialized instance of selectors element map.</value>
        public SelectorsMap Selectors
        {
            get
            {
                if (this.selectorsMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.selectorsMap = new SelectorsMap(this.find);
                }

                return this.selectorsMap;
            }

            private set
            {
                this.selectorsMap = value;
            }
        }

        /// <summary>
        /// Gets the search element map.
        /// </summary>
        /// <value>An initialized instance of search element map.</value>
        public SearchMap Search
        {
            get
            {
                if (this.searchMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.searchMap = new SearchMap(this.find);
                }

                return this.searchMap;
            }

            private set
            {
                this.searchMap = value;
            }
        }

        /// <summary>
        /// Gets the identity element map.
        /// </summary>
        /// <value>An initialized instance of identity element map.</value>
        public IdentityMap Identity
        {
            get
            {
                if (this.identityMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.identityMap = new IdentityMap(this.find);
                }

                return this.identityMap;
            }

            private set
            {
                this.identityMap = value;
            }
        }

        /// <summary>
        /// Gets or sets the image gallery.
        /// </summary>
        /// <value>The image gallery.</value>
        public MediaGalleryMap MediaGallery
        {
            get
            {
                if (this.mediaGalleryMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.mediaGalleryMap = new MediaGalleryMap(this.find);
                }

                return this.mediaGalleryMap;
            }

            private set
            {
                this.mediaGalleryMap = value;
            }
        }

        /// <summary>
        /// Gets the lists element map.
        /// </summary>
        /// <value>The list map.</value>
        public ListsMap Lists
        {
            get
            {
                if (this.listsMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.listsMap = new ListsMap(this.find);
                }

                return this.listsMap;
            }

            private set
            {
                this.listsMap = value;
            }
        }

        /// <summary>
        /// Gets or sets the classifications.
        /// </summary>
        /// <value>The classifications.</value>
        public ClassificationsMap Classifications
        {
            get
            {
                if (this.classificationsMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.classificationsMap = new ClassificationsMap(this.find);
                }

                return this.classificationsMap;
            }

            private set
            {
                this.classificationsMap = value;
            }
        }

        /// <summary>
        /// Gets or sets the scripts and styles.
        /// </summary>
        /// <value>The scripts and styles.</value>
        public ScriptsAndStylesMap ScriptsAndStyles
        {
            get
            {
                if (this.scriptsAndStylesMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.scriptsAndStylesMap = new ScriptsAndStylesMap(this.find);
                }

                return this.scriptsAndStylesMap;
            }

            private set
            {
                this.scriptsAndStylesMap = value;
            }
        }

        /// <summary>
        /// Gets the CommentsAndReviewsMap element map.
        /// </summary>
        /// <value>The CommentsAndReviewsMap map.</value>
        public CommentsAndReviewsMap CommentsAndReviews
        {
            get
            {
                if (this.commentsAndReviewsMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.commentsAndReviewsMap = new CommentsAndReviewsMap(this.find);
                }

                return this.commentsAndReviewsMap;
            }

            private set
            {
                this.commentsAndReviewsMap = value;
            }
        }

        /// <summary>
        /// Gets the EmailCampaignsMap element map.
        /// </summary>
        /// <value>The EmailCampaignsMap map.</value>
        public EmailCampaignsMap EmailCampaigns
        {
            get
            {
                if (this.emailCampaignsMap == null)
                {
                    this.EnsureFindIsInitialized();
                    this.emailCampaignsMap = new EmailCampaignsMap(this.find);
                }

                return this.emailCampaignsMap;
            }

            private set
            {
                this.emailCampaignsMap = value;
            }
        }

        private void EnsureFindIsInitialized()
        {
            if (this.find == null)
            {
                throw new NotSupportedException("The element map can't be used without specifying its Find object.");
            }
        }

        private Find find;
        private ContentMap contentMap;
        private NavigationMap navigationMap;
        private SocialShareMap socialShareMap;
        private ModuleBuilderMap moduleBuilderMap;
        private WidgetTemplatesMap widgetTemplatesMap;
        private WidgetDesignerMap widgetDesignerMap;
        private SelectorsMap selectorsMap;
        private SearchMap searchMap;
        private MediaMap mediaMap;
        private IdentityMap identityMap;
        private MediaGalleryMap mediaGalleryMap;
        private ListsMap listsMap;
        private ScriptsAndStylesMap scriptsAndStylesMap;
        private CommentsAndReviewsMap commentsAndReviewsMap;
        private ClassificationsMap classificationsMap;
        private EmailCampaignsMap emailCampaignsMap;
    }
}
