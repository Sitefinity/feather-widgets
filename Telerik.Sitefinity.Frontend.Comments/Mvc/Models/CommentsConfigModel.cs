﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Frontend.Comments.Mvc.Models
{
    /// <summary>
    /// This class provides API for working with comments settings.
    /// </summary>
    public class CommentsConfigModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsConfigModel"/> class.
        /// </summary>
        public CommentsConfigModel()
        {
            var assembly = typeof(Telerik.Sitefinity.Configuration.Config).Assembly;
            this.commentsModuleConfigType = assembly.GetType("Telerik.Sitefinity.Modules.Comments.Configuration.CommentsModuleConfig");
            this.config = Config.Get(commentsModuleConfigType);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use spam protection image.
        /// </summary>
        /// <value>
        /// When set to <c>true</c> spam protection image is used; otherwise, <c>false</c>.
        /// </value>
        public bool UseSpamProtectionImage
        {
            get
            {
                var useSpamProtectionImagePropInfo = this.commentsModuleConfigType.GetProperty("UseSpamProtectionImage", BindingFlags.Public | BindingFlags.Instance);
                var useSpamProtectionImage = (bool)useSpamProtectionImagePropInfo.GetValue(this.config, null); 

                return useSpamProtectionImage;
            }
        }

        /// <summary>
        /// Gets whether comments will be displayed on pages.
        /// </summary>
        public bool EnablePaging
        {
            get
            {
                var enablePagingPropInfo = this.commentsModuleConfigType.GetProperty("EnablePaging", BindingFlags.Public | BindingFlags.Instance);
                var enablePaging = (bool)enablePagingPropInfo.GetValue(this.config, null);

                return enablePaging;
            }
        }

        /// <summary>
        /// Gets how many comments will be displayed on a page.
        /// </summary>
        public int CommentsPerPage
        {
            get
            {
                var commentsPerPagePropInfo = this.commentsModuleConfigType.GetProperty("CommentsPerPage", BindingFlags.Public | BindingFlags.Instance);
                var commentsPerPage = (int)commentsPerPagePropInfo.GetValue(this.config, null);

                return commentsPerPage;
            }
        }


        /// <summary>
        /// Gets whether the newest comments will be displayed on top. Otherwise the oldest will be on top.
        /// </summary>
        public bool AreNewestOnTop
        {
            get
            {
                var areNewestOnTopPropInfo = this.commentsModuleConfigType.GetProperty("AreNewestOnTop", BindingFlags.Public | BindingFlags.Instance);
                var areNewestOnTop = (bool)areNewestOnTopPropInfo.GetValue(this.config, null);

                return areNewestOnTop;
            }
        }

        /// <summary>
        /// Gets whether the date time valus will be passed only in UTC
        /// </summary>
        public bool AlwaysUseUtc
        {
            get
            {
                var alwaysUseUtcPropInfo = this.commentsModuleConfigType.GetProperty("AlwaysUseUTC", BindingFlags.Public | BindingFlags.Instance);
                var alwaysUseUtc = (bool)alwaysUseUtcPropInfo.GetValue(this.config, null);

                return alwaysUseUtc;
            }
        }

        private Type commentsModuleConfigType;
        private ConfigSection config;
    }
}
