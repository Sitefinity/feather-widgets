using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Frontend.Comments.Mvc.Models
{
    public class CommentsConfigModel
    {
        public CommentsConfigModel(string threadType)
        {
            var threadConfig = this.GetThreadConfigByType(threadType);

            var commentsSettingsElementType = Type.GetType("Telerik.Sitefinity.Modules.Comments.Configuration.CommentsSettingsElement");
            this.AllowComments = this.GetProperty(threadConfig, commentsSettingsElementType, "AllowComments");
            this.RequiresAuthentication = this.GetProperty(threadConfig, commentsSettingsElementType, "RequiresAuthentication");
            this.RequiresApproval = this.GetProperty(threadConfig, commentsSettingsElementType, "RequiresApproval");
            this.AllowSubscription = this.GetProperty(threadConfig, commentsSettingsElementType, "AllowSubscription");
            this.EnableRatings = this.GetProperty(threadConfig, commentsSettingsElementType, "EnableRatings");
        }

        public bool AllowComments { get; set; }

        public bool RequiresAuthentication { get; set; }

        public bool RequiresApproval { get; set; }

        public bool AllowSubscription { get; set; }

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
