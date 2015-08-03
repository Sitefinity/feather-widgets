using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Forms
{
    public class FormsVirtualRazorResolver : IVirtualFileResolver
    {
        public const string Path = "~/Mvc-Form-View/";

        public bool Exists(PathDefinition definition, string virtualPath)
        {
            var id = this.ResolveFormDescriptionId(virtualPath);
            return id != null && this.DescriptionExists(id.Value);
        }

        public System.Web.Caching.CacheDependency GetCacheDependency(PathDefinition definition, string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            return new System.Web.Caching.CacheDependency("D:\\changeme.txt");
        }

        public Stream Open(PathDefinition definition, string virtualPath)
        {
            var formManager = FormsManager.GetManager();

            var id = this.ResolveFormDescriptionId(virtualPath);
            var form = formManager.GetForm(id.Value);

            using (var output = new MemoryStream())
            {
                using (var writer = new StreamWriter(output))
                {
                    writer.WriteLine("@using Telerik.Sitefinity.UI.MVC;");
                    writer.WriteLine("@using Telerik.Sitefinity.Frontend.Forms.Mvc.Helper;");
                    writer.WriteLine("@using (Html.BeginFormSitefinity(\"Submit\", null)){");

                    var content = new ControlPlaceholder("Body", form.Controls.ToArray());
                    writer.Write(content.Render());

                    writer.WriteLine("}");

                    writer.Flush();
                    output.Seek(0, SeekOrigin.Begin);
                }

                return output;
            }
        }

        private Guid? ResolveFormDescriptionId(string virtualPath)
        {
            const string Extension = ".cshtml";

            if (!virtualPath.EndsWith(Extension, StringComparison.OrdinalIgnoreCase))
                return null;

            var fn = VirtualPathUtility.GetFileName(virtualPath);

            var idString = fn.Left(fn.Length - Extension.Length);
            Guid id;
            Guid.TryParse(idString, out id);

            return id;
        }

        private bool DescriptionExists(Guid id)
        {
            var formManager = FormsManager.GetManager();
            return formManager.GetForms().Any(f => f.Id == id);
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
                    return string.Format("@Html.FormController(new Guid(\"{0}\"), (string)Model.Mode, null)", controlDataId.ToString("D"));
                }
                else
                {
                    return "[Non-MVC Form control]";
                }
            }
        }
    }
}