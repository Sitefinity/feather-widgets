using System;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Frontend.Assistant
{
    internal class SitefinityAssistantConfigAccessor : IDisposable
    {
        private readonly ConfigManager manager;
        private const string SectionName = "SitefinityAssistantConfig";
        private const string AdminApiBaseUrlPropertyName = "adminApiBaseUrl";
        private const string FeatureStatePropertyName = "featureState";
        private const string CdnHostNamePropertyName = "cdnHostName";
        private const string CdnRootFolderRelativePathPropertyName = "cdnRootFolderRelativePath";

        public SitefinityAssistantConfigAccessor()
        {
            this.manager = ConfigManager.GetManager();
        }

        public string AdminApiBaseUrl
        {
            get
            {
                return (string)this[AdminApiBaseUrlPropertyName];
            }
        }

        /// <summary>
        /// Gets the state of the feature.
        /// </summary>
        public string FeatureState
        {
            get
            {
                return (string)this[FeatureStatePropertyName];
            }
        }

        /// <summary>
        /// Gets the url of the CDN host name.
        /// </summary>
        public string CdnHostName
        {
            get
            {
                return (string)this[CdnHostNamePropertyName];
            }
        }

        /// <summary>
        /// Gets the relative path where the assistant scripts are located within the CDN.
        /// </summary>
        public string CdnRootFolderRelativePath
        {
            get
            {
                return (string)this[CdnRootFolderRelativePathPropertyName];
            }
        }

        private object this[string key]
        {
            get
            {
                object value;

                try
                {
                    var section = this.manager.GetSection(SectionName);
                    value = section[key];
                }
                catch
                {
                    value = null;
                }

                return value;
            }
        }

        public void Dispose()
        {
            this.manager.Dispose();
        }
    }
}
