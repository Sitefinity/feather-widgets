using System;
using System.Linq;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Identity
{
    internal class UserProfileMvcOutputCacheVariation : CustomOutputCacheVariationBase
    {
        private bool? noCache;

        public UserProfileMvcOutputCacheVariation()
        {
        }

        /// <inheritdoc />
        public override string Key
        {
            get
            {
                return "sf-user-profile-mvc-view";
            }
        }

        /// <inheritdoc />
        public override bool NoCache
        {
            get
            {
                if (this.noCache.HasValue)
                {
                    return this.noCache.Value;
                }

                return ClaimsManager.GetCurrentIdentity().IsAuthenticated;
            }
        }

        /// <inheritdoc />
        public override string GetValue()
        {
            return string.Empty;
        }

        internal void ForceNoCache(bool noCache = true)
        {
            this.noCache = noCache;
        }
    }
}