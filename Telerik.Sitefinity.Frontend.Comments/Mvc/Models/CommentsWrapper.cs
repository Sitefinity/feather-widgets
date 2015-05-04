using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Frontend.Comments.Mvc.Models
{
    public class CommentsWrapper
    {
        /// <summary>
        /// Gets the thread configuration by its type. Falls back to default settings if there are no settings for the type.
        /// </summary>
        /// <param name="threadKey">The thread key.</param>
        internal static ConfigElement GetThreadConfigByType(string threadType)
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
    }
}
