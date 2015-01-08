using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.SocialShare.Mvc.StringResources;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Frontend.SocialShare.Mvc.Models
{
    /// <summary>
    /// This class represents a Social share link with a resource localization of its text.
    /// </summary>
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
        /// Gets the is checked.
        /// </summary>
        /// <value>The is checked.</value>
        public bool IsChecked { get; private set; }

        private string label;
    }
}
