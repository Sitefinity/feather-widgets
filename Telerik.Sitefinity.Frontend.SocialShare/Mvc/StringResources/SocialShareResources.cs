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
            LastModified = "2014/08/22")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }

        #endregion
    }
}
