using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Forms
{
    /// <summary>
    /// This interface provides API for creating form markup.
    /// </summary>
    public class FormRazorRenderer : IFormRenderer
    {
        /// <inheritDoc/>
        public void RenderForm(StreamWriter writer, FormDescription form)
        {
            writer.WriteLine("@using Telerik.Sitefinity.UI.MVC;");
            writer.WriteLine("@using Telerik.Sitefinity.Frontend.Forms.Mvc.Helpers;");
            writer.WriteLine("@using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;");
            writer.WriteLine("<div class=\"@Model.CssClass\">");
            writer.WriteLine("@using (Html.BeginFormSitefinity(\"Index\", null)){");

            var content = new ControlPlaceholder("Body", form.Controls.ToArray());
            writer.Write(content.Render());

            writer.WriteLine("}");
            writer.WriteLine("</div>");
        }

        private class ControlPlaceholder
        {
            private readonly List<Control> children;
            private readonly ISitefinityControllerFactory controllerFactory;

            public ControlPlaceholder(string placeholderId, IEnumerable<ControlData> loadedControls)
            {
                this.controllerFactory = ControllerBuilder.Current.GetControllerFactory() as ISitefinityControllerFactory;

                var manager = FormsManager.GetManager();

                List<ControlData> relevantControls = new List<ControlData>();
                List<ControlData> notRelevantControls = new List<ControlData>();
                foreach (var controlData in loadedControls)
                {
                    if (controlData.PlaceHolder == placeholderId)
                        relevantControls.Add(controlData);
                    else
                        notRelevantControls.Add(controlData);
                }

                this.children = new List<Control>(relevantControls.Count);

                var siblingId = Guid.Empty;
                while (relevantControls.Count > 0)
                {
                    var currentControl = relevantControls.FirstOrDefault(c => c.SiblingId == siblingId);
                    if (currentControl == null)
                        break;

                    relevantControls.Remove(currentControl);
                    siblingId = currentControl.Id;

                    var controlInstance = manager.LoadControl(currentControl);
                    if (currentControl.IsLayoutControl)
                    {
                        var layoutControl = (LayoutControl)controlInstance;
                        foreach (var childPlaceholder in layoutControl.Placeholders)
                        {
                            var childControls = new ControlPlaceholder(childPlaceholder.ID, notRelevantControls).Children;
                            foreach (var childControl in childControls)
                                childPlaceholder.Controls.Add(childControl);
                        }

                        this.children.Add(layoutControl);
                    }
                    else
                    {
                        var literal = this.FormControllerLiteral(controlInstance, currentControl.Id);
                        this.children.Add(new LiteralControl(literal));
                    }
                }
            }

            public IReadOnlyList<Control> Children
            {
                get
                {
                    return this.children;
                }
            }

            public string Render()
            {
                using (var writer = new StringWriter())
                {
                    using (var htmlWriter = new HtmlTextWriter(writer))
                    {
                        for (var i = 0; i < this.Children.Count; i++)
                        {
                            this.Children[i].RenderControl(htmlWriter);
                        }
                    }

                    return writer.ToString();
                }
            }

            private string FormControllerLiteral(Control controlInstance, Guid controlDataId)
            {
                var proxy = controlInstance as MvcProxyBase;
                if (proxy != null)
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
}
