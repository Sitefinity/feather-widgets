using System;
using System.ComponentModel;
using System.Reflection;
using System.Web;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Captcha.DTO;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.Captcha
{
    /// <summary>
    /// Implements API for working with form Captcha fields.
    /// </summary>
    public class CaptchaModel : FormElementModel, ICaptchaModel
    {
        /// <inheritDocs/>
        public bool DisplayOnlyForUnauthenticatedUsers
        {
            get;
            set;
        }

        /// <inheritDocs/>
        public bool EnableAudioCode
        {
            get
            {
                return this.enableAudioCode;
            }

            set
            {
                this.enableAudioCode = value;
            }
        }

        /// <inheritDocs/>
        public override object Value
        {
            get
            {
                if (base.Value == null)
                    base.Value = System.Web.HttpContext.Current.Request[CaptchaModel.CaptchaAnswerFormKey];

                return base.Value;
            }

            set
            {
                base.Value = value;
            }
        }

        /// <summary>
        /// Gets or sets a validation mechanism for the field.
        /// </summary>
        /// <value>The validation.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public override ValidatorDefinition ValidatorDefinition
        {
            get
            {
                if (this.validatorDefinition == null)
                {
                    this.validatorDefinition = new ValidatorDefinition();
                    this.validatorDefinition.RequiredViolationMessage = Res.Get<FormResources>().RequiredInputErrorMessage;
                }

                return this.validatorDefinition;
            }

            set
            {
                this.validatorDefinition = value;
            }
        }

        /// <inheritDocs/>
        public override object GetViewModel(object value)
        {
            return new CaptchaViewModel()
            {
                GenerateUrl = RouteHelper.ResolveUrl(CaptchaModel.CaptchaGetService, UrlResolveOptions.Rooted),
                CaptchaAnswerFormKey = CaptchaModel.CaptchaAnswerFormKey,
                CaptchaKeyFormKey = CaptchaModel.CaptchaKeyFormKey,
                ValidatorDefinition = this.BuildValidatorDefinition(this.ValidatorDefinition, "Captcha"),
                CssClass = this.CssClass,
                EnableAudioCode = this.EnableAudioCode
            };
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

            var strValue = value as string;
            if (string.IsNullOrEmpty(strValue))
                return false;

            if (HttpContext.Current == null || HttpContext.Current.Request == null)
                return false;

            var key = HttpContext.Current.Request[CaptchaModel.CaptchaKeyFormKey];

            if (string.IsNullOrEmpty(key))
                return false;

            bool isValid = key.Contains(",") && strValue.Contains(",") ? this.ValidateCaptchas(key, strValue) :
                this.ValidateCaptcha(key, strValue);

            return isValid;
        }

        private bool ValidateCaptcha(string key, string answer)
        {
            var commentWebServiceType = Type.GetType("Telerik.Sitefinity.Services.Captcha.CaptchaWebService, Telerik.Sitefinity");
            var commentWebServiceInstance = Activator.CreateInstance(commentWebServiceType);
            var validateMethodInfo = commentWebServiceType.GetMethod("Validate", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(string), typeof(string) }, null);

            CaptchaValidationResponse result;
            // Workaround culture issues in the Captcha validation. Remove this region for Sitefinity 9.0.
            using (new CultureRegion(System.Globalization.CultureInfo.InvariantCulture))
            {
                result = validateMethodInfo.Invoke(commentWebServiceInstance, new object[] { key, answer }) as CaptchaValidationResponse;
            }

            return result != null && result.IsValid;
        }

        private bool ValidateCaptchas(string keys, string answers)
        {
            var keysArr = keys.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var answersArr = answers.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (keysArr.Length == answersArr.Length)
            {
                for (int i = 0; i < keysArr.Length; i++)
                {
                    if (!this.ValidateCaptcha(keysArr[i], answersArr[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return this.ValidateCaptcha(keys, answers);
            }
        }

        private ValidatorDefinition validatorDefinition;
        private const string CaptchaGetService = "RestApi/captcha/";

        private const string CaptchaAnswerFormKey = "captcha-a";
        private const string CaptchaKeyFormKey = "captcha-k";
        private bool enableAudioCode = true;
    }
}
