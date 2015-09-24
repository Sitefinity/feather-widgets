﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Forms.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.Recaptcha
{
    /// <summary>
    /// Implements API for working with form reCaptcha fields.
    /// </summary>
    public class RecaptchaModel : FormElementModel, IRecaptchaModel
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
                    this.dataSitekey = ConfigManager.GetManager().GetSection<FormsConfig>().Parameters[RecaptchaModel.GRecaptchaParameterDataSiteKey];
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
                    this.secret = ConfigManager.GetManager().GetSection<FormsConfig>().Parameters[RecaptchaModel.GRecaptchaParameterSecretKey];
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
        public override object Value
        {
            get
            {
                if (base.Value == null)
                    base.Value = System.Web.HttpContext.Current.Request["g-recaptcha-response"];

                return base.Value;
            }
            set
            {
                base.Value = value;
            }
        }

        /// <inheritDocs/>
        public override object GetViewModel(object value)
        {
            var viewModel = new RecaptchaViewModel()
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
            var isVisible = !(SystemManager.CurrentHttpContext.User != null &&
                    SystemManager.CurrentHttpContext.User.Identity != null &&
                    SystemManager.CurrentHttpContext.User.Identity.IsAuthenticated &&
                    this.DisplayOnlyForUnauthenticatedUsers &&
                    !SystemManager.IsDesignMode);

            return isVisible;
        }

        /// <inheritDocs/>
        public override bool IsValid(object value)
        {
            if (!this.ShouldRenderCaptcha())
                return true;

            if (string.IsNullOrEmpty(value as string))
                return false;

            try
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

                    if (responseModel.ErrorCodes != null && responseModel.ErrorCodes.Contains("invalid-input-secret"))
                        Log.Write("Invalid input secret for reCaptcha. Please configure in advanced settings parameters for Forms.");

                    return responseModel.Success;
                }
            }
            catch (Exception ex)
            {
                Log.Write("Exception while validating reCaptcha field: " + ex.Message);
                return false;
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
