using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.SocialShare.Mvc.StringResources;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Frontend.SocialShare.Mvc.Models
{
    public class SocialShareMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SocialShareMap" /> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="isChecked">The is checked.</param>
        public SocialShareMap(string key, bool isChecked)
        {
            this.Key = key;
            this.IsChecked = isChecked;
            this.Label = Res.Get<SocialShareResources>(key);
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <value>The label.</value>
        public string Label { get; private set; }

        /// <summary>
        /// Gets the is checked.
        /// </summary>
        /// <value>The is checked.</value>
        public bool IsChecked { get; private set; }
    }
}
