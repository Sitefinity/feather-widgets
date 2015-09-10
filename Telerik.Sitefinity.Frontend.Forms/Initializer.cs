﻿using System.Configuration;
using System.Web.Mvc;
using System.Web.UI;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.ControlTemplates;
using Telerik.Sitefinity.Modules.Forms.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.RecaptchaField;

namespace Telerik.Sitefinity.Frontend.Forms
{
    public static class Initializer
    {
        public static void Initialize()
        {
            VirtualPathManager.AddVirtualFileResolver<FormsVirtualRazorResolver>(FormsVirtualRazorResolver.Path + "*", "MvcFormsResolver");
            ObjectFactory.Container.RegisterInstance<IControlDefinitionExtender>("FormsDefinitionsExtender", new FormsDefinitionsExtender(), new ContainerControlledLifetimeManager());

            EventHub.Unsubscribe<IScriptsRegisteringEvent>(Initializer.RegisteringFormScriptsHandler);
            EventHub.Subscribe<IScriptsRegisteringEvent>(Initializer.RegisteringFormScriptsHandler);

            Bootstrapper.Initialized += Bootstrapper_Initialized;
        }

        private static void Bootstrapper_Initialized(object sender, ExecutedEventArgs e)
        {
            if (e.CommandName == "Bootstrapped")
            {
                Initializer.UnregisterTemplatableControl();
                Initializer.CreateFormsGoogleRecaptchaFieldConfig();
            }
        }

        private static void UnregisterTemplatableControl()
        {
            ControlTemplates.UnregisterTemplatableControl(new ControlTemplateInfo() { ControlType = typeof(FormController), AreaName = "Form" });
        }

        private static void CreateFormsGoogleRecaptchaFieldConfig()
        {
            const string TestGRecaptchaDataSitekey = "6Ldj-gsTAAAAAJ3yIz0mOEVoLIl4FLbGZr7e-sc_";
            const string TestGRecaptchaSecret = "magicunicorns";

            var manager = ConfigManager.GetManager();
            var formsConfigSection = manager.GetSection<FormsConfig>();
            if (formsConfigSection.Parameters[RecaptchaFieldModel.GRecaptchaParameterDataSiteKey] == null)
            {
                formsConfigSection.Parameters.Add(RecaptchaFieldModel.GRecaptchaParameterDataSiteKey, TestGRecaptchaDataSitekey);
            }

            if (formsConfigSection.Parameters[RecaptchaFieldModel.GRecaptchaParameterSecretKey] == null)
            {
                formsConfigSection.Parameters.Add(RecaptchaFieldModel.GRecaptchaParameterSecretKey, TestGRecaptchaSecret);
            }

            using (var a = new ElevatedConfigModeRegion())
            {
                manager.SaveSection(formsConfigSection);
            }
        }

        private static void RegisteringFormScriptsHandler(IScriptsRegisteringEvent @event)
        {
            var zoneEditor = @event.Sender as ZoneEditor;
            if (zoneEditor != null && zoneEditor.MediaType == DesignMediaType.Form)
            {
                @event.Scripts.Add(new ScriptReference(string.Format("~/Frontend-Assembly/{0}/Mvc/Scripts/Form/form.js", typeof(Initializer).Assembly.GetName().Name)));
            }
        }
    }
}