using System;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.SocialShare.Mvc.StringResources
{
    [ObjectInfo(typeof(SocialShareResources), Title = "SocialShareResourcesTitle", Description = "SocialShareResourcesDescription")]
    public class SocialShareResources : Resource
    {
        #region Constructions

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialShareResources" /> class.
        /// </summary>
        public SocialShareResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialShareResources" /> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public SocialShareResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets phrase: Which sharing options you want to use?
        /// </summary>
        [ResourceEntry("SocialShareOptions",
            Value = "Which sharing options you want to use?",
            Description = "phrase : Which sharing options you want to use?",
            LastModified = "2015/01/06")]
        public string SocialShareOptions
        {
            get
            {
                return this["SocialShareOptions"];
            }
        }

        /// <summary>
        /// Gets phrase: Template
        /// </summary>
        [ResourceEntry("Template",
            Value = "Template",
            Description = "phrase : Template",
            LastModified = "2015/01/06")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }

        /// <summary>
        /// Gets phrase: More options
        /// </summary>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/01/06")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// Gets phrase : CSS classes
        /// </summary>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "phrase : CSS classes",
            LastModified = "2015/01/06")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        #region Social share labels

        /// <summary>
        /// Gets phrase: Facebook
        /// </summary>
        [ResourceEntry("Facebook",
            Value = "Facebook",
            Description = "phrase : Facebook",
            LastModified = "2015/01/06")]
        public string Facebook
        {
            get
            {
                return this["Facebook"];
            }
        }

        /// <summary>
        /// Gets phrase: Twitter
        /// </summary>
        [ResourceEntry("Twitter",
            Value = "Twitter",
            Description = "phrase : Twitter",
            LastModified = "2015/01/06")]
        public string Twitter
        {
            get
            {
                return this["Twitter"];
            }
        }

        /// <summary>
        /// Gets phrase: Twitter
        /// </summary>
        [ResourceEntry("GooglePlusOne",
            Value = "Google +",
            Description = "phrase : Google +",
            LastModified = "2015/01/06")]
        public string GooglePlusOne
        {
            get
            {
                return this["GooglePlusOne"];
            }
        }

        /// <summary>
        /// Gets phrase: LinkedIn
        /// </summary>
        [ResourceEntry("LinkedIn",
            Value = "LinkedIn",
            Description = "phrase : Google +",
            LastModified = "2015/01/06")]
        public string LinkedIn
        {
            get
            {
                return this["LinkedIn"];
            }
        }

        /// <summary>
        /// Gets phrase: Digg
        /// </summary>
        [ResourceEntry("Digg",
            Value = "Digg",
            Description = "phrase : Digg",
            LastModified = "2015/01/06")]
        public string Digg
        {
            get
            {
                return this["Digg"];
            }
        }

        /// <summary>
        /// Gets phrase: Blogger
        /// </summary>
        [ResourceEntry("Blogger",
            Value = "Blogger",
            Description = "phrase : Blogger",
            LastModified = "2015/01/06")]
        public string Blogger
        {
            get
            {
                return this["Blogger"];
            }
        }

        /// <summary>
        /// Gets phrase: Tumblr
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Tumblr"), ResourceEntry("Tumblr",
            Value = "Tumblr",
            Description = "phrase : Tumblr",
            LastModified = "2015/01/06")]
        public string Tumblr
        {
            get
            {
                return this["Tumblr"];
            }
        }

        /// <summary>
        /// Gets phrase: GoogleBookmarks
        /// </summary>
        [ResourceEntry("GoogleBookmarks",
            Value = "Google bookmarks",
            Description = "phrase : Google bookmarks",
            LastModified = "2015/01/06")]
        public string GoogleBookmarks
        {
            get
            {
                return this["GoogleBookmarks"];
            }
        }

        /// <summary>
        /// Gets phrase: MySpace
        /// </summary>
        [ResourceEntry("MySpace",
            Value = "My Space",
            Description = "phrase : My Space",
            LastModified = "2015/01/06")]
        public string MySpace
        {
            get
            {
                return this["MySpace"];
            }
        }

        /// <summary>
        /// Gets phrase: Stumble upon
        /// </summary>
        [ResourceEntry("StumbleUpon",
            Value = "Stumble upon",
            Description = "phrase : Stumble upon",
            LastModified = "2015/01/06")]
        public string StumbleUpon
        {
            get
            {
                return this["StumbleUpon"];
            }
        }

        /// <summary>
        /// Gets phrase: Reddit
        /// </summary>
        [ResourceEntry("Reddit",
            Value = "Reddit",
            Description = "phrase : Reddit",
            LastModified = "2015/01/06")]
        public string Reddit
        {
            get
            {
                return this["Reddit"];
            }
        }

        /// <summary>
        /// Gets phrase: MailTo
        /// </summary>
        [ResourceEntry("MailTo",
            Value = "MailTo",
            Description = "phrase : MailTo",
            LastModified = "2015/01/06")]
        public string MailTo
        {
            get
            {
                return this["MailTo"];
            }
        }

        /// <summary>
        /// Gets phrase: Delicious
        /// </summary>
        [ResourceEntry("Delicious",
            Value = "Delicious",
            Description = "phrase : Delicious",
            LastModified = "2015/01/06")]
        public string Delicious
        {
            get
            {
                return this["Delicious"];
            }
        }
        #endregion

        #endregion
    }
}
