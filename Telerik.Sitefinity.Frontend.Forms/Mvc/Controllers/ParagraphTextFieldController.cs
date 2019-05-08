using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.ParagraphTextField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the MVC forms paragraph text field.
    /// </summary>
    [DatabaseMapping(UserFriendlyDataType.LongText)]
    [Localization(typeof(FieldResources))]
    public class ParagraphTextFieldController : FormFieldControllerBase<IParagraphTextFieldModel>, ISupportRules, IParagraphFormField
    {
        public ParagraphTextFieldController()
        {
            this.DisplayMode = FieldDisplayMode.Write;
        }

        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ReflectInheritedProperties]
        public override IParagraphTextFieldModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IParagraphTextFieldModel>(this.GetType());

                return this.model;
            }
        }

        IDictionary<ConditionOperator, string> ISupportRules.Operators
        {
            get
            {
                return new Dictionary<ConditionOperator, string>()
                {
                    [ConditionOperator.Equal] = Res.Get<Labels>().IsOperator,
                    [ConditionOperator.NotEqual] = Res.Get<Labels>().IsNotOperator,
                    [ConditionOperator.Contains] = Res.Get<Labels>().ContainsOperator,
                    [ConditionOperator.NotContains] = Res.Get<Labels>().NotContainsOperator,
                    [ConditionOperator.IsFilled] = Res.Get<Labels>().IsFilledOperator,
                    [ConditionOperator.IsNotFilled] = Res.Get<Labels>().IsNotFilledOperator
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
       
        private IParagraphTextFieldModel model;
    }
}