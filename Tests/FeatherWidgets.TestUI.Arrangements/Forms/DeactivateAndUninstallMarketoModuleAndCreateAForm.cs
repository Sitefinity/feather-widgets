using System;
using System.Collections.Generic;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using FeatherWidgets.TestUtilities.CommonOperations.Forms;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Web.Services;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// DeactivateAndUninstallMarketoModuleAndCreateAForm arrangement class.
    /// </summary>
    public class DeactivateAndUninstallMarketoModuleAndCreateAForm : TestArrangementBase
    {
        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.StaticModules().ActivateModule(ModuleName);
        }

        private const string ModuleName = "MarketoConnector";
    }
}
