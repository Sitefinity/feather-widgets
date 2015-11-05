using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.PageBreak;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the MVC forms PageBreak field.
    /// </summary>
    [Localization(typeof(FieldResources))]
    public class PageBreakController : FormElementControllerBase<IPageBreakModel>
    {
        public PageBreakController()
        {
            this.DisplayMode = FieldDisplayMode.Read;
        }

        /// <summary>
        /// This form element doens't support write mode so redirect to read.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override ActionResult Write(object value)
        {
            return this.Read(value);
        }

        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ReflectInheritedProperties]
        public override IPageBreakModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IPageBreakModel>(this.GetType());

                return this.model;
            }
        }

        private IPageBreakModel model;
    }
}