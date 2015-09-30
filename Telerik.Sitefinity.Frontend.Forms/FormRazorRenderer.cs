using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Mvc.Proxy;

namespace Telerik.Sitefinity.Frontend.Forms
{
    /// <summary>
    /// This interface provides API for creating form markup.
    /// </summary>
    public class FormRazorRenderer : FormRendererBase, IFormRenderer
    {
        /// <inheritDoc/>
        public override void Render(StreamWriter writer, FormDescription form)
        {
            var fileStream = typeof(FormRazorRenderer).Assembly.GetManifestResourceStream("Telerik.Sitefinity.Frontend.Forms.Mvc.Views.Form.Index.cshtml");
            string formIndexView;
            using (var streamReader = new StreamReader(fileStream))
            {
                formIndexView = streamReader.ReadToEnd();
            }

            var result = formIndexView.Replace("## Fields Markup ##", this.GetFieldsMarkup("Body", form.Controls.ToArray()));
            writer.Write(result);
        }

        protected override string GetFieldMarkup(Control controlInstance, Guid controlDataId)
        {
            if (controlInstance is MvcProxyBase)
            {
                return string.Format("@Html.FormController(new Guid(\"{0}\"), (FormViewMode)Model.ViewMode, null)", controlDataId.ToString("D"));
            }
            else
            {
                return "<span>[Non-MVC Form control]</span>";
            }
        }
    }
}