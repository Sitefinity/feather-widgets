using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.SocialShare.Mvc.Models
{
    /// <summary>
    /// Class that is used for grouping social share buttons.
    /// </summary>
    public class SocialShareGroupMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SocialShareGroupMap" /> class.
        /// </summary>
        /// <param name="groups">The groups of <see cref="SocialShareMap" /> objects.</param>
        public SocialShareGroupMap(IEnumerable<SocialShareMap> groups)
        {
            this.Groups = groups;
        }

        /// <summary>
        /// Gets or sets the groups of <see cref="SocialShareMap" /> objects.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public IEnumerable<SocialShareMap> Groups { get; private set; }
    }
}
