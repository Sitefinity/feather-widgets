using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.Resources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models
{
    /// <summary>
    /// This class represents the model used for Form widget.
    /// </summary>
    public class FormModel : ContentModelBase, IFormModel
    {
        /// <inheritDoc/>
        public Guid FormId { get; set; }

        /// <inheritDoc/>
        public FormViewMode ViewMode { get; set; }

        /// <inheritDoc/>
        public bool UseCustomConfirmation { get; set; }

        /// <inheritDoc/>
        public CustomConfirmationMode CustomConfirmationMode { get; set; }

        /// <inheritDoc/>
        public string CustomConfirmationMessage { get; set; }

        /// <inheritDoc/>
        public Guid CustomConfirmationPageId { get; set; }

        /// <inheritDoc/>
        public string CssClass { get; set; }

        /// <inheritDoc/>
        public FormViewModel GetViewModel()
        {
            if (this.FormId == Guid.Empty)
            {
                return null;
            }

            var viewModel = new FormViewModel()
            {
                FormId = this.FormId,
                ViewMode = this.ViewMode
            };

            if (!FormsManager.GetManager().GetForms().Any(f => f.Id == this.FormId))
            {
                viewModel.Error = Res.Get<FormsResources>().TheSpecifiedFormNoLongerExists;
            }

            return viewModel;
        }

        /// <inheritDoc/>
        public string GetViewPath()
        {
            var currentPackage = new PackageManager().GetCurrentPackage();
            if (string.IsNullOrEmpty(currentPackage))
            {
                currentPackage = "default";
            }

            var viewPath = FormsVirtualRazorResolver.Path + currentPackage + "/" + this.FormId.ToString("D") + ".cshtml";

            return viewPath;
        }

        /// <inheritDoc/>
        public bool TrySubmitForm(FormCollection collection, string userHostAddress)
        {
            var success = false;

            var manager = FormsManager.GetManager();
            var form = manager.GetForm(this.FormId);

            if (this.IsValidForm(form, collection, manager))
            {
                var formData = new KeyValuePair<string, object>[collection.Count];
                for (int i = 0; i < collection.Count; i++)
                {
                    formData[i] = new KeyValuePair<string, object>(collection.Keys[i], collection[collection.Keys[i]]);
                }

                var formLanguage = SystemManager.CurrentContext.AppSettings.Multilingual ? CultureInfo.CurrentUICulture.Name : null;

                FormsHelper.SaveFormsEntry(form, formData, null, userHostAddress, ClaimsManager.GetCurrentUserId(), formLanguage);

                success = true;
            }

            return success;
        }

        #region ContentModelBase

        /// <inheritDoc/>
        public override Type ContentType
        {
            get { return typeof(FormDescription); }
            set { }
        }

        /// <inheritDoc/>
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            return ((FormsManager)this.GetManager()).GetForms().Where(f => f.Framework == FormFramework.Mvc);
        }

        #endregion

        #region Private Methods
        
        private bool IsValidForm(FormDescription form, FormCollection collection, FormsManager manager)
        {
            var behaviorResolver = ObjectFactory.Resolve<IControlBehaviorResolver>();
            foreach (var control in form.Controls)
            {
                if (control.IsLayoutControl)
                    continue;

                Type controlType;
                if (control.ObjectType.StartsWith("~/"))
                    controlType = FormsManager.GetControlType(control);
                else
                    controlType = TypeResolutionService.ResolveType(behaviorResolver.GetBehaviorObjectType(control), true);

                if (!typeof(IFormFieldController).IsAssignableFrom(controlType))
                    continue;

                var controlInstance = manager.LoadControl(control);
                var fieldControl = (IFormFieldController)behaviorResolver.GetBehaviorObject(controlInstance);

                var fieldValue = collection[fieldControl.MetaField.FieldName];

                if (!fieldControl.FieldModel.IsValid(fieldValue))
                    return false;
            }

            return true;
        }

        #endregion
    }
}