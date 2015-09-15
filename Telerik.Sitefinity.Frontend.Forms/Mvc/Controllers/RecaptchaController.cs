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
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.Recaptcha;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the MVC forms reCaptcha field.
    /// </summary>
    [ControllerToolboxItem(Name = "MvcReCaptchaField", Title = "reCAPTCHA", Toolbox = FormsConstants.FormControlsToolboxName, SectionName = FormsConstants.CommonSectionName)]
    [DatabaseMapping(UserFriendlyDataType.YesNo)]
    [Localization(typeof(FieldResources))]
    public class RecaptchaController : FormElementControllerBase<IRecaptchaModel>
    {
        public RecaptchaController()
        {
            this.DisplayMode = FieldDisplayMode.Write;
        }

        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public override IRecaptchaModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IRecaptchaModel>(this.GetType());

                return this.model;
            }
        }

        private IRecaptchaModel model;
    }
}