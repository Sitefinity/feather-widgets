﻿using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Frontend.Identity
{
    [Serializable]
    internal class ResetPasswordCacheVariation : CustomOutputCacheVariationBase
    {
        public ResetPasswordCacheVariation()
        {
            this.Key = "sf-reset-password-mvc-view";
        }

        /// <inheritdoc />
        [IgnoreDataMember]
        public override bool NoCache
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc />
        public override string GetValue()
        {
            return string.Empty;
        }
    }
}