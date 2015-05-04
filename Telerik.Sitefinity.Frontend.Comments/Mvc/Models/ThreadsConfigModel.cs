using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Frontend.Comments.Mvc.Models
{
    /// <summary>
    /// This class provides API for working with comments configuration per thread.
    /// </summary>
    public class ThreadsConfigModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadsConfigModel"/> class.
        /// </summary>
        /// <param name="threadType">Type of the thread.</param>
        public ThreadsConfigModel(string threadType)
        {
            var threadConfig = this.GetThreadConfigByType(threadType);

            var commentsSettingsElementType = Type.GetType("Telerik.Sitefinity.Modules.Comments.Configuration.CommentsSettingsElement");
            this.AllowComments = this.GetProperty(threadConfig, commentsSettingsElementType, "AllowComments");
            this.RequiresAuthentication = this.GetProperty(threadConfig, commentsSettingsElementType, "RequiresAuthentication");
            this.RequiresApproval = this.GetProperty(threadConfig, commentsSettingsElementType, "RequiresApproval");
            this.AllowSubscription = this.GetProperty(threadConfig, commentsSettingsElementType, "AllowSubscription");
            this.EnableRatings = this.GetProperty(threadConfig, commentsSettingsElementType, "EnableRatings");
        }

        /// <summary>
        /// Gets or sets a value indicating if content item supports comments.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if content item supports comments; otherwise, <c>false</c>.
        /// </value>
        public bool AllowComments { get; set; }

        /// <summary>
        /// Gets or sets whether threads on the commentable type require authentication by default.
        /// </summary>
        public bool RequiresAuthentication { get; set; }

        /// <summary>
        /// Gets or sets whether threads on the commentable type require approval by default.
        /// </summary>
        public bool RequiresApproval { get; set; }

        /// <summary>
        /// Gets whether comments will allow subscription for email notifications.
        /// </summary>
        public bool AllowSubscription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to restrict ratings to one from user by thread.
        /// </summary>
        /// <value>
        /// When set to <c>true</c> the comments are restricted to one from user by thread; otherwise, <c>false</c>.
        /// </value>
        public bool EnableRatings { get; set; }

        /// <summary>
        /// Gets the thread configuration by its type. Falls back to default settings if there are no settings for the type.
        /// </summary>
        /// <param name="threadKey">The thread key.</param>
        private ConfigElement GetThreadConfigByType(string threadType)
        {
            var commentsModuleConfigType = Type.GetType("Telerik.Sitefinity.Modules.Comments.Configuration.CommentsModuleConfig");
            var config = Config.Get(commentsModuleConfigType);
            var defaultSettubgsPropInfo = commentsModuleConfigType.GetProperty("DefaultSettings", BindingFlags.Public | BindingFlags.Instance);
            var defaultSettings = defaultSettubgsPropInfo.GetValue(config, null) as ConfigElement; 

            if (threadType.IsNullOrEmpty())
            {
                return defaultSettings;
            }
            else
            {
                var commentableTypesPropInfo = commentsModuleConfigType.GetProperty("CommentableTypes", BindingFlags.Public | BindingFlags.Instance);
                var commentableTypes = defaultSettubgsPropInfo.GetValue(config, null) as ConfigElementDictionary<string, ConfigElement>;

                return commentableTypes.Contains(threadType) ? commentableTypes[threadType] : defaultSettings;
            }
        }

        private bool GetProperty(ConfigElement config, Type commentsSettingsElementType, string propName)
        {
            var propInfo = commentsSettingsElementType.GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
            var propValue = (bool)propInfo.GetValue(config, null);

            return propValue;
        }
    }
}
