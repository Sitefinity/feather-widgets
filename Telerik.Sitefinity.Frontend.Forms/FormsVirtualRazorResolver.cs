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
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;

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

                    var controllerFactory = ControllerBuilder.Current.GetControllerFactory() as ISitefinityControllerFactory;

                    foreach (var formControl in form.Controls)
                    {
                        var control = formManager.LoadControl(formControl);
                        var proxy = control as MvcProxyBase;
                        if (proxy != null && controllerFactory != null)
                        {
                            var controllerName = controllerFactory.GetControllerName(controllerFactory.ResolveControllerType(proxy.ControllerName));

                            var fieldControl = proxy.GetController() as IFormFieldControl;
                            writer.WriteLine("@Html.FormController(new Guid(\"{0}\"), (string)Model.Mode, null)", formControl.Id.ToString("D"));
                        }
                        else
                        {
                            writer.WriteLine("[Non-MVC Form control]");
                        }
                    }

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
    }
}