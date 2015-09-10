using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Configuration;
using Newtonsoft.Json;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Forms.Configuration;

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
                if (string.IsNullOrEmpty(this.dataSitekey))
                {
                    this.dataSitekey = ConfigManager.GetManager().GetSection<FormsConfig>().Parameters[RecaptchaFieldModel.GRecaptchaParameterDataSiteKey];
                }

                return this.dataSitekey;
            }
            set
            {
                this.dataSitekey = value;
            }
        }

        /// <inheritDocs/>
        public string Secret
        {
            get
            {
                if (string.IsNullOrEmpty(this.secret))
                {
                    this.secret = ConfigManager.GetManager().GetSection<FormsConfig>().Parameters[RecaptchaFieldModel.GRecaptchaParameterSecretKey];
                }

                return this.secret;
            }
            set
            {
                this.secret = value;
            }
        }

        /// <inheritDocs/>
        public long ValidationTimeoutMiliseconds
        {
            get
            {
                return this.validationTimeoutMiliseconds;
            }
            set
            {
                this.validationTimeoutMiliseconds = value;
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
                DataSitekey = this.DataSitekey
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
                    this.DisplayOnlyForUnauthenticatedUsers && 
                    !SystemManager.IsDesignMode)
            {
                isVisible = false;
            }

            return isVisible;
        }

        public override bool IsValid(object value)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(this.ValidationTimeoutMiliseconds);

                var values = new Dictionary<string, string>
                {
                   { "secret", this.Secret },
                   { "response", value as string }
                };

                var response = client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(values)).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                var responseModel = JsonConvert.DeserializeObject<GRecaptchaValidationResponseModel>(responseString);

                return responseModel.Success;
            }
        }

        internal const string GRecaptchaParameterDataSiteKey = "GRecaptchaDataSiteKey";
        internal const string GRecaptchaParameterSecretKey = "GRecaptchaSecretKey";

        private string size = "normal";
        private string dataType = "image";
        private string theme = "light";
        private string dataSitekey = string.Empty;
        private string secret = string.Empty;
        private long validationTimeoutMiliseconds = 10000;

        private class GRecaptchaValidationResponseModel
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("error-codes")]
            public IEnumerable<string> ErrorCodes { get; set; }
        }
    }
}
