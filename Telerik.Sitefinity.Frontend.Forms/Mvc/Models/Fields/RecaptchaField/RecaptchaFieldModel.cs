using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.RecaptchaField
{
    /// <summary>
    /// Implements API for working with form reCaptcha fields.
    /// </summary>
    public class RecaptchaFieldModel : FormFieldModel, IRecaptchaFieldModel
    {
        /// <inheritDocs/>
        public string Theme
        {
            get
            {
                return this.theme;
            }
            set
            {
                this.theme = value;
            }
        }

        /// <inheritDocs/>
        public string DataType
        {
            get
            {
                return this.dataType;
            }
            set
            {
                this.dataType = value;
            }
        }

        /// <inheritDocs/>
        public string Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }

        /// <inheritDocs/>
        public string DataSitekey
        {
            get
            {
                return this.dataSitekey;
            }
            set
            {
                this.dataSitekey = value;
            }
        }

        /// <inheritDocs/>
        public bool DisplayOnlyForUnauthenticatedUsers
        {
            get;
            set;
        }

        /// <inheritDocs/>
        public RecaptchaFieldViewModel GetViewModel(object value, IMetaField metaField)
        {
            var viewModel = new RecaptchaFieldViewModel()
            {
                 DataType = this.DataType,
                 Size = this.Size,
                 Theme = this.Theme,
                 DataSitekey = this.DataSitekey,

            };

            return viewModel;
        }

        /// <inheritDocs/>
        public bool ShouldRenderCaptcha()
        {
            var isVisible = true;

            if (SystemManager.CurrentHttpContext.User != null &&
                    SystemManager.CurrentHttpContext.User.Identity != null &&
                    SystemManager.CurrentHttpContext.User.Identity.IsAuthenticated &&
                    this.DisplayOnlyForUnauthenticatedUsers)
            {
                isVisible = false;
            }

            return isVisible;
        }

        private string size = "normal";
        private string dataType = "image";
        private string theme = "light";
        private string dataSitekey = "6Ldj-gsTAAAAAJ3yIz0mOEVoLIl4FLbGZr7e-sc_";
    }
}
