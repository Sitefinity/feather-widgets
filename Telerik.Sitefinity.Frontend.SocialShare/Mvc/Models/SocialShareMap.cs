using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.SocialShare.Mvc.Models
{
    internal class SocialShareMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SocialShareMap" /> class.
        /// </summary>
        /// <param name="groups">The groups.</param>
        public SocialShareMap(IDictionary<string, bool> groups)
        {
            this.Groups = groups;
        }

        /// <summary>
        /// Gets or sets the groups.
        /// </summary>
        /// <value>The groups.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public IDictionary<string, bool> Groups { get; private set; }
    }
}
