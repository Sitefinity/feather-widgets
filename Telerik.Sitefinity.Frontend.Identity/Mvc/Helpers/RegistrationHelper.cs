using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Validation.Contracts;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Helpers
{
    public static class RegistrationHelper
    {

        /// <summary>
        /// Get Profile validation attributes
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetProfileValidationAttributes(string fieldName)
        {
            var validation = RegistrationHelper.GetFieldValidatorDefinition(fieldName);
            var validationAttributes = new Dictionary<string, object>();
            var requiredErrorMessage = string.Empty;
            var lengthErrorMessage = string.Empty;

            validationAttributes.Add("data-val", "true");

            if (validation.Required.HasValue && validation.Required.Value)
            {
                if (validation.RequiredViolationMessage != null)
                {
                   requiredErrorMessage = Res.Get<ErrorMessages>(validation.RequiredViolationMessage).StartsWith(MissingResourcePrefix) ? validation.RequiredViolationMessage : Res.Get<ErrorMessages>(validation.RequiredViolationMessage);
                }

                validationAttributes.Add("data-val-required", requiredErrorMessage);
            }

            if (validation.MaxLengthViolationMessage != null)
            {
                lengthErrorMessage = Res.Get<ErrorMessages>(validation.MaxLengthViolationMessage).StartsWith(MissingResourcePrefix) ? validation.MaxLengthViolationMessage : Res.Get<ErrorMessages>(validation.MaxLengthViolationMessage);
            }

            validationAttributes.Add("data-val-length", lengthErrorMessage);
            validationAttributes.Add("data-val-length-min", validation.MinLength);

            if (validation.MaxLength > 0)
                validationAttributes.Add("data-val-length-max", validation.MaxLength);

            return validationAttributes;
        }

        /// <summary>
        /// Get validator definition for specific field
        /// </summary>
        /// <param name="fieldName">Field name</param>
        /// <returns></returns>
        public static IValidatorDefinition GetFieldValidatorDefinition(string fieldName)
        {
            return ContentViewConfig.GetFieldValidatorDefinition(typeof(SitefinityProfile), fieldName);
        }

        private const string MissingResourcePrefix = "#ResourceNotFound#";
    }
}
