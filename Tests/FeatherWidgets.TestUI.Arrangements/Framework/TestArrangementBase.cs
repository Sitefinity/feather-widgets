﻿using System;
using System.Linq;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Base class for Test Arrangements classes.
    /// Default values for the Culture and Site are specified in the testArrangements.config
    /// </summary>
    [Culture]
    [Site]
    public abstract class TestArrangementBase : ITestArrangement
    {
        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// <value>Provider name.</value>
        protected virtual string DynamicProviderName
        {
            get
            {
                if (string.IsNullOrEmpty(this.providerName))
                {
                    this.providerName = ServerOperations.MultiSite().CheckIsMultisiteMode() ?
                        TestArrangementBase.MultisiteProviderName :
                        TestArrangementBase.SingleSiteProviderName;
                }

                return this.providerName;
            }
        }

        private const string SingleSiteProviderName = "OpenAccessProvider";
        private const string MultisiteProviderName = "dynamicContentProvider";
        private string providerName;
    }
}