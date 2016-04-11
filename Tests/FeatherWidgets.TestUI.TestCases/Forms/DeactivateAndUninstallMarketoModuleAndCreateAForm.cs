using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.TestUI.Core.Utilities;

namespace FeatherWidgets.TestUI.TestCases.Forms
{
    /// <summary>
    /// DeactivateAndUninstallMarketoModuleAndCreateAForm test class.
    /// </summary>
    [TestClass]
    public class DeactivateAndUninstallMarketoModuleAndCreateAForm_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DeactivateAndUninstallMarketoModuleAndCreateAForm
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6)]
        public void DeactivateAndUninstallMarketoModuleAndCreateAForm()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Wrappers().Backend().ModulesAndServices().ModulesAndServicesWrapper().NavigateToModules();
            BAT.Wrappers().Backend().ModulesAndServices().ModulesAndServicesWrapper().DeactivateModule(moduleName);
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().Modules().Forms(this.Culture));            
            BAT.Wrappers().Backend().Forms().FormsDashboard().ClickCreateAFormButton();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickAdvancedButton();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyWebFrameworkExists();
            BAT.Wrappers().Backend().Forms().FormsCreateScreen().ClickBackToForms();

            BAT.Wrappers().Backend().ModulesAndServices().ModulesAndServicesWrapper().NavigateToModules();
            BAT.Wrappers().Backend().ModulesAndServices().ModulesAndServicesWrapper().UninstallModule(moduleName);
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().Modules().Forms(this.Culture));
            BAT.Wrappers().Backend().Forms().FormsDashboard().ClickCreateAFormButton();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().ClickAdvancedButton();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyWebFrameworkExists();
            BAT.Wrappers().Backend().Forms().FormsCreateScreen().ClickBackToForms();

            BAT.Wrappers().Backend().ModulesAndServices().ModulesAndServicesWrapper().NavigateToModules();
            BAT.Wrappers().Backend().ModulesAndServices().ModulesAndServicesWrapper().InstallModule(moduleName);
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().Modules().Forms(this.Culture));
            BAT.Wrappers().Backend().Forms().FormsDashboard().ClickCreateAFormButton();
            BAT.Wrappers().Backend().Forms().FormsCreateScreen().ExpandAdvancedOptions();
            BATFeather.Wrappers().Backend().Forms().FormsContentScreenWrapper().VerifyWebFrameworkExists();
            BAT.Wrappers().Backend().Forms().FormsCreateScreen().ClickBackToForms();

        }

        private readonly String moduleName = "Connector for Marketo";
    }
}
