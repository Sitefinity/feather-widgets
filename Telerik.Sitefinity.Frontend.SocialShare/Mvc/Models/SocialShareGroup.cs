using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.SocialShare.Mvc.Models
{
    /// <summary>
    /// Class that is used for grouping social share options.
    /// </summary>
    public class SocialShareGroup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SocialShareGroup" /> class.
        /// </summary>
        /// <param name="groups">The groups of <see cref="SocialShareOption" /> objects.</param>
        public SocialShareGroup(IEnumerable<SocialShareOption> groups)
        {
            this.Groups = groups;
        }

        /// <summary>
        /// Gets or sets the groups of <see cref="SocialShareOption" /> objects.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public IEnumerable<SocialShareOption> Groups { get; private set; }
    }
}
