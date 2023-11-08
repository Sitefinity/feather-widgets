using System;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Identity
{
    [Serializable]
    internal class UserProfileMvcOutputCacheVariation : CustomOutputCacheVariationBase
    {
        [NonSerialized]
        private bool? noCache;

        public UserProfileMvcOutputCacheVariation()
        {
            this.Key = "sf-user-profile-mvc-view";
        }

        /// <inheritdoc />
        [IgnoreDataMember]
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