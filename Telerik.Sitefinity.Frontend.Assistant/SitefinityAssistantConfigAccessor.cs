using System;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Frontend.Assistant
{
    internal class SitefinityAssistantConfigAccessor : IDisposable
    {
        private readonly ConfigManager manager;
        private readonly string SectionName;
        private readonly string SubProperty;
        private const string CdnHostNamePropertyName = "cdnHostName";

        public SitefinityAssistantConfigAccessor(string sectionName)
            : this(sectionName, null)
        {
            this.manager = ConfigManager.GetManager();
            this.SectionName = sectionName;
        }

        public SitefinityAssistantConfigAccessor(string sectionName, string subProperty)
        {
            this.manager = ConfigManager.GetManager();
            this.SectionName = sectionName;
            this.SubProperty = subProperty;
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

        public object this[string key]
        {
            get
            {
                object value;

                try
                {
                    var section = this.manager.GetSection(SectionName);

                    if (!string.IsNullOrEmpty(this.SubProperty))
                    {
                        var sub = section[this.SubProperty] as ConfigElement;
                        value = sub[key];
                    }
                    else
                    {
                        value = section[key];
                    }
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
