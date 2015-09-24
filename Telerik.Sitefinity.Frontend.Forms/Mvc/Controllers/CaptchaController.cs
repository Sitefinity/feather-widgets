using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.Captcha;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Mvc.ActionFilters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the MVC forms Captcha field.
    /// </summary>
    [ControllerToolboxItem(Name = "MvcCaptchaField", Title = "CAPTCHA", Toolbox = FormsConstants.FormControlsToolboxName, SectionName = FormsConstants.CommonSectionName, CssClass = RecaptchaController.WidgetIconCssClass)]
    [Localization(typeof(FieldResources))]
    [IndexRenderMode(IndexRenderModes.NoOutput)]
    public class CaptchaController : FormElementControllerBase<ICaptchaModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CaptchaController"/> class.
        /// </summary>
        public CaptchaController()
        {
            this.DisplayMode = FieldDisplayMode.Write;
        }

        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ReflectInheritedProperties]
        public override ICaptchaModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<ICaptchaModel>(this.GetType());

                return this.model;
            }
        }

        /// <summary>
        /// Write action of the controller.
        /// </summary>
        /// <param name="value">The input value.</param>
        /// <returns>The action result.</returns>
        public override ActionResult Write(object value)
        {
            if (this.Model.ShouldRenderCaptcha())
                return base.Write(value);
            else
                return new EmptyResult();
        }

        internal const string WidgetIconCssClass = "sfCaptchaIcn sfMvcIcn";
        private ICaptchaModel model;
    }
}