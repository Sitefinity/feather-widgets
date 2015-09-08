using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Ninject;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Mvc.Proxy;
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
            var id = this.ResolveFormDescriptionId(virtualPath);
            if (id != null && this.DescriptionExists(id.Value))
            {
                return new DataItemSystemCacheDependency(typeof(FormDescription), itemId: id.Value.ToString("D"));
            }
            else
            {
                return new DataItemSystemCacheDependency(typeof(FormDescription));
            }
        }

        public Stream Open(PathDefinition definition, string virtualPath)
        {
            var formManager = FormsManager.GetManager();

            var id = this.ResolveFormDescriptionId(virtualPath);
            var form = formManager.GetForm(id.Value);

            var output = new MemoryStream();
            var writer = new StreamWriter(output);

            FrontendModule.Current.DependencyResolver.Get<IFormRenderer>().Render(writer, form);

            writer.Flush();
            output.Seek(0, SeekOrigin.Begin);

            return output;
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