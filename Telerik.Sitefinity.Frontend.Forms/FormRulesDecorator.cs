using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Modules.Forms.Web.UI;
using Telerik.Sitefinity.Mvc.Proxy;

namespace Telerik.Sitefinity.Frontend.Forms
{
    /// <summary>
    /// Decorates forms markup in MVC
    /// </summary>
    /// <seealso cref="Telerik.Sitefinity.Modules.Forms.IFormRulesDecorator" />
    internal class FormRulesDecorator : IFormRulesDecorator
    {
        /// <inheritdoc />
        public void SetForm(FormDescription form)
        {
            this.hasRules = !string.IsNullOrWhiteSpace(form.Rules);
            this.hiddenFields = FormsHelpers.GetHiddenFields(form.Controls);
            this.fieldNames = FormsHelpers.GetFieldNames(form.Controls);
        }

        /// <inheritdoc />
        public string GetInFormMarkup()
        {
            StringBuilder inFormMarkup = new StringBuilder();

            if (this.hiddenFields.Any() || this.hasRules)
            {
                inFormMarkup.AppendFormat(CultureInfo.InvariantCulture, @"<input type=""hidden"" data-sf-role=""form-rules-hidden-fields"" name=""sf_FormHiddenFields"" value=""{0}"" autocomplete=""off""/>", string.Join(",", this.hiddenFields));
                inFormMarkup.Append(@"<input type='hidden' data-sf-key=""@Model.FormId"" />");

                if (this.hasRules)
                {
                    inFormMarkup.Append(@"<input type=""hidden"" data-sf-role=""form-rules"" value='@Model.FormRules' />");
                    inFormMarkup.Append(@"<input type=""hidden"" data-sf-role=""form-rules-skip-fields"" name=""sf_FormSkipFields"" autocomplete=""off""/>");
                    inFormMarkup.Append(@"<input type=""hidden"" data-sf-role=""form-rules-message"" name=""sf_FormMessage"" autocomplete=""off""/>");
                    inFormMarkup.Append(@"<input type=""hidden"" data-sf-role=""form-rules-redirect-page"" name=""sf_FormRedirectPage"" autocomplete=""off""/>");
                }
            }

            return inFormMarkup.ToString();
        }

        /// <inheritdoc />
        public string WrapFormMarkup(string formMarkup)
        {
            StringBuilder wrappedFormMarkup = new StringBuilder(formMarkup);

            if (this.hiddenFields.Any() || this.hasRules)
            {
                if (this.hasRules)
                {
                    wrappedFormMarkup.Insert(0, @"@Html.Script(Url.EmbeddedResource(""Telerik.Sitefinity.Modules.Forms.FormsModule"", ""Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.form-rules.js""), ""top"", false)");
                }

                wrappedFormMarkup.Insert(0, @"@Html.Script(ScriptRef.JQuery, ""top"", false)");

                wrappedFormMarkup.Append(@"@Html.Script(Url.EmbeddedResource(""Telerik.Sitefinity.Modules.Forms.FormsModule"", ""Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.form-hidden-fields.js""), null, false, false)");
                wrappedFormMarkup.Append(@"<script>formHiddenFieldsInitialization(""@Model.FormId"")</script>");
                wrappedFormMarkup.Insert(0, "<div data-sf-role='form-visibility-wrapper' style='display: none'>");
                wrappedFormMarkup.Append("</div>");
            }

            return wrappedFormMarkup.ToString();
        }

        public string WrapFieldMarkup(MvcControllerProxy mvcControllerProxy, string fieldMarkup)
        {
            StringBuilder markup = new StringBuilder(fieldMarkup);

            if (this.hasRules || this.hiddenFields.Any())
            {
                var formFieldController = mvcControllerProxy.Controller as IFormElementController<IFormElementModel>;
                if (formFieldController != null && formFieldController.Model is IHideable)
                {
                    var fieldName = this.fieldNames.ContainsKey(mvcControllerProxy.ID) ? this.fieldNames[mvcControllerProxy.ID] : string.Empty;
                    markup.Insert(0, string.Format(CultureInfo.InvariantCulture, @"<script data-sf-role='start_field_{0}' data-sf-role-field-name='{1}'></script>", mvcControllerProxy.ID, fieldName));
                    markup.AppendFormat(CultureInfo.InvariantCulture, @"<script data-sf-role='end_field_{0}'></script>", mvcControllerProxy.ID);
                }
            }

            return markup.ToString();
        }

        private bool hasRules;
        private List<string> hiddenFields;
        private Dictionary<string, string> fieldNames;
    }
}