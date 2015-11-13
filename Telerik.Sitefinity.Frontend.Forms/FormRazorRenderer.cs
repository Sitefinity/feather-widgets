﻿using System;
using System.IO;
using System.Linq;
using System;
using System.Text;
using System.Web.Hosting;
using System.Web.UI;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Helpers;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;


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
            var virtualPath = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend.Forms/Mvc/Views/Form/Index.cshtml";
            string formIndexView;
            using (StreamReader reader = new StreamReader(HostingEnvironment.VirtualPathProvider.GetFile(virtualPath).Open()))
            {
                formIndexView = reader.ReadToEnd();
            }

            var formControlsArray = form.Controls.ToArray();
            var isMultiPageForm = formControlsArray.Any(c => (c.GetControlType().ImplementsInterface(typeof(IFormPageBreak))));
            StringBuilder formControlsMarkup = new StringBuilder();
            formControlsMarkup.Append(this.GetFieldsMarkup("Body", formControlsArray));

            if (isMultiPageForm)
            {
                formControlsMarkup.Insert(0, this.pageBreakSeparatorBegin);
                formControlsMarkup.Append(this.pageBreakSeparatorEnd);
            }

            formControlsMarkup.Insert(0, this.GetFieldsMarkup("Header", formControlsArray));
            formControlsMarkup.Append(this.GetFieldsMarkup("Footer", formControlsArray));

            var result = formIndexView.Replace("@* Fields Markup *@", formControlsMarkup.ToString());
            writer.Write(result);
        }

        protected override string GetFieldMarkup(Control controlInstance, Guid controlDataId)
        {
            if (controlInstance is MvcProxyBase)
            {
                var controlInstanceString = string.Format("@Html.FormController(new Guid(\"{0}\"), (FormViewMode)Model.ViewMode, null)", controlDataId.ToString("D"));
                var controllerName = ((MvcProxyBase)controlInstance).ControllerName;
                var controlType = Telerik.Sitefinity.Utilities.TypeConverters.TypeResolutionService.ResolveType(controllerName, throwOnError: false);

                if (controlType.ImplementsInterface(typeof(IFormPageBreak)))
                {
                    controlInstanceString += string.Concat(this.pageBreakSeparatorEnd, this.pageBreakSeparatorBegin);
                }

                return controlInstanceString;
            }
            else
            {
                return "<span>[Non-MVC Form control]</span>";
            }
        }


        public string pageBreakSeparatorBegin = "<div class='separator'>";
        public string pageBreakSeparatorEnd = "</div>";
    }
}