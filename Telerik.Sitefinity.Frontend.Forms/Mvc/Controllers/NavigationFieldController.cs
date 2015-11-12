using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.NavigationField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the MVC forms Navigation field.
    /// </summary>
    [Localization(typeof(FieldResources))]
    public class NavigationFieldController : FormElementControllerBase<INavigationFieldModel>,
                                             IZoneEditorReloader
    {
        public NavigationFieldController()
        {
            this.DisplayMode = FieldDisplayMode.Read;
        }

        /// <inheritDocs />
        public bool ShouldReloadControlsWithSameKey()
        {
            return false;
        }

        /// <inheritDocs />
        public string Key
        {
            get
            {
                return "navigation-field";
            }
        }

        /// <summary>
        /// This form element doesn't support write mode so redirect to read.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override ActionResult Write(object value)
        {
            if (SystemManager.HttpContextItems["ControlId"] != null)
            {
                var controlId = (Guid)SystemManager.HttpContextItems["ControlId"];
                this.UpdatePages(controlId);
            }

            return this.Read(value);
        }

        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ReflectInheritedProperties]
        public override INavigationFieldModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<INavigationFieldModel>(this.GetType());

                return this.model;
            }
        }

        /// <summary>
        /// Updates SerializedPages property of the control depending on Form's IFormPageBreak controls
        /// </summary>
        private void UpdatePages(Guid controlId)
        {
            var manager = FormsManager.GetManager();
            var controlData = manager.GetControl<ControlData>(controlId) as FormDraftControl;
            if (controlData != null)
            {
                var form = controlData.Form;

                IList<FormDraftControl> pageBreakControls = NavigationFieldController.GetPageBreakControls(form);

                bool modelModified;
                this.TryUpdateModel(pageBreakControls, out modelModified);

                if (modelModified)
                {
                    this.SaveProperties(controlData, manager, form);
                }
            }
        }

        /// <summary>
        /// Gets Form controls that implement IFormPageBreak interface from "Body" placeholder
        /// </summary>
        private static IList<FormDraftControl> GetPageBreakControls(FormDraft form)
        {
            var behaviorResolver = ObjectFactory.Resolve<IControlBehaviorResolver>();

            IList<FormDraftControl> orderedControls = new List<FormDraftControl>();
            var bodyControls = form.Controls.Where(c => c.PlaceHolder == "Body").ToList();
            var siblingId = Guid.Empty;
            while (bodyControls.Count > 0)
            {
                var currentControl = bodyControls.FirstOrDefault(c => c.SiblingId == siblingId);
                if (currentControl == null)
                    break;

                bodyControls.Remove(currentControl);
                siblingId = currentControl.Id;

                orderedControls.Add(currentControl);
            }

            IList<FormDraftControl> pageBreakControls = new List<FormDraftControl>();
            foreach (var control in orderedControls.Where(c => c.IsLayoutControl == false))
            {
                var controlType = TypeResolutionService.ResolveType(behaviorResolver.GetBehaviorObjectType(control), false);
                if (controlType != null && controlType.ImplementsInterface(typeof(IFormPageBreak)))
                {
                    pageBreakControls.Add(control);
                }
            }
            return pageBreakControls;
        }

        /// <summary>
        /// Updates this.Model.SerializedPages property
        /// </summary>
        private void TryUpdateModel(IList<FormDraftControl> pageBreakControls, out bool changesMade)
        {
            changesMade = false;
            var pageBreakControlIds = pageBreakControls.Select(c => c.Properties.First(p => p.Name == "ID").Value).ToList();
            var deserializedPages = this.Model.DeserializePages().ToList();
            IList<FormPage> pages = new List<FormPage>() { deserializedPages.First() };

            for (int i = 0; i < pageBreakControlIds.Count; i++)
            {
                var currentPage = deserializedPages.FirstOrDefault(p => p.PreviousPageBreakId == pageBreakControlIds[i].ToString());
                if (currentPage == null)
                {
                    string pageTitle = Res.Get<FieldResources>().PageName + (i + 2);
                    var newPage = new FormPage() { Title = pageTitle, PreviousPageBreakId = pageBreakControlIds[i].ToString() };
                    pages.Add(newPage);

                    // A page has been added
                    changesMade = true;
                }
                else
                {
                    pages.Add(currentPage);
                }
            }

            if (!changesMade)
            {
                // Check if a page has been deleted
                if (pages.Count != deserializedPages.Count)
                {
                    changesMade = true;
                }
                else
                {
                    // Check if index of a page has been changed
                    for (int i = 0; i < pages.Count; i++)
                    {
                        if (pages[i] != deserializedPages[i])
                        {
                            changesMade = true;
                            break;
                        }
                    }
                }
            }

            this.Model.SerializedPages = JsonSerializer.SerializeToString(pages);
        }

        /// <summary>
        /// Persists modified SerializedPages property
        /// </summary>
        private void SaveProperties(FormDraftControl controlData, FormsManager manager, FormDraft form)
        {
            WcfPropertyManager propManager = new WcfPropertyManager();
            object formControl = manager.LoadControl(controlData);

            string parentPropertyPath = null;
            var property = propManager.GetProperties(formControl, controlData, -1, parentPropertyPath).Where(p => p.PropertyName == "SerializedPages").FirstOrDefault();
            if (property != null)
            {
                property.PropertyValue = this.Model.SerializedPages;

                ControlPropertyService service = new ControlPropertyService();
                service.SaveProperties(new WcfControlProperty[] { property }, controlData.Id.ToString(), null, form.Id.ToString(), "Form");
            }
        }

        private INavigationFieldModel model;
    }
}
