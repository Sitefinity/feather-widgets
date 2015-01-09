using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.SocialShare.Mvc.StringResources;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Frontend.SocialShare.Mvc.Models
{
    /// <summary>
    /// This class represents a Social Share option with a resource localization of its text.
    /// </summary>
    public class SocialShareOption
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SocialShareOption" /> class.
        /// </summary>
        /// <param name="key">The social share option key.</param>
        /// <param name="isChecked">The value indicating whether social share option is checked.</param>
        public SocialShareOption(string key, bool isChecked)
        {
            this.Key = key;
            this.IsChecked = isChecked;
        }

        /// <summary>
        /// Gets the social share option key.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the social share option label.
        /// </summary>
        public string Label
        {
            get
            {
                if (this.label.IsNullOrEmpty())
                {
                    this.label = Res.Get<SocialShareResources>(this.Key);
                }

                return this.label;
            }
        }

        /// <summary>
        /// Gets the value indicating whether social share option is checked.
        /// </summary>
        public bool IsChecked { get; private set; }

        private string label;
    }
}
