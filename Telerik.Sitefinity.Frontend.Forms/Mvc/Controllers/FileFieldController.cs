using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.FileField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages.Web.Services;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the MVC forms file field.
    /// </summary>
    [DatabaseMapping(UserFriendlyDataType.FileUpload)]
    [Localization(typeof(FieldResources))]
    public class FileFieldController : FormFieldControllerBase<IFileFieldModel>, IRequireLibrary, ISupportRules, IFileFormField
    {
        /// <summary>
        /// Gets the form field model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ReflectInheritedProperties]
        public override IFileFieldModel Model 
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IFileFieldModel>(this.GetType());

                return this.model;
            }
        }

        IDictionary<ConditionOperator, string> ISupportRules.Operators
        {
            get
            {
                return new Dictionary<ConditionOperator, string>()
                {
                    [ConditionOperator.FileSelected] = Res.Get<Labels>().FileSelectedOperator,
                    [ConditionOperator.FileNotSelected] = Res.Get<Labels>().NoFileSelectedOperator
                };
            }
        }

        string ISupportRules.Title
        {
            get
            {
                return this.MetaField.Title;
            }
        }

        private IFileFieldModel model;
    }
}
