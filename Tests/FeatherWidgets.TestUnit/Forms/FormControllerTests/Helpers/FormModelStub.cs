using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;

namespace FeatherWidgets.TestUnit.Forms.FormControllerTests.Helpers
{
    internal class FormModelStub : IFormModel
    {
        public string AjaxSubmitUrl
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string CssClass
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string CustomConfirmationMessage
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public CustomConfirmationMode CustomConfirmationMode
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Guid CustomConfirmationPageId
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Guid FormId
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsMultiStep
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public FormCollection FormCollection
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool NeedsRedirect { get; set; }

        public bool UseAjaxSubmit
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool UseCustomConfirmation
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public FormViewMode ViewMode
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool RaiseBeforeFormActionEventValue { get; set; }

        public SubmitStatus SubmitResultStatus { get; set; }

        public bool AllowRenderForm()
        {
            throw new NotImplementedException();
        }

        public IList<CacheDependencyKey> GetKeysOfDependentObjects(FormViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IContentLocationInfo> GetLocations()
        {
            throw new NotImplementedException();
        }

        public string GetRedirectPageUrl()
        {
            throw new NotImplementedException();
        }

        public string GetSubmitMessage(SubmitStatus submitedSuccessfully)
        {
            return string.Empty;
        }

        public FormViewModel GetViewModel()
        {
            throw new NotImplementedException();
        }

        public bool RaiseBeforeFormActionEvent()
        {
            return this.RaiseBeforeFormActionEventValue;
        }

        public SubmitStatus TrySubmitForm(FormCollection collection, HttpFileCollectionBase files, string userHostAddress)
        {
            return this.SubmitResultStatus;
        }
    }
}