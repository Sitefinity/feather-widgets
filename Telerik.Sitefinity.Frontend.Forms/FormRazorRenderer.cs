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
            writer.WriteLine("@using Telerik.Sitefinity.UI.MVC;");
            writer.WriteLine("@using Telerik.Sitefinity.Frontend.Forms.Mvc.Helpers;");
            writer.WriteLine("@using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;");
            writer.WriteLine("<div class=\"@Model.CssClass\">");
            writer.WriteLine("@using (Html.BeginFormSitefinity(\"Index\", null, null, FormMethod.Post, new { enctype = \"multipart/form-data\" })){");

            writer.Write(this.GetFieldsMarkup("Body", form.Controls.ToArray()));

            writer.WriteLine("}");
            writer.WriteLine("</div>");
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