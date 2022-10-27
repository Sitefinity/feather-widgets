using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.MultipleChoiceField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the MVC forms multiple choice field.
    /// </summary>
    [DatabaseMapping(UserFriendlyDataType.ShortText)]
    [Localization(typeof(FieldResources))]
    [ControllerToolboxItem(
        Name = "MvcMultipleChoiceField",
        Toolbox = FormsConstants.FormControlsToolboxName,
        SectionName = FormsConstants.CommonSectionName,
        Title = "Multiple choice",
        CssClass = "sfMultipleChoiceIcn sfMvcIcn",
        Ordinal = 0.13f)]
    public class MultipleChoiceFieldController : FormFieldControllerBase<IMultipleChoiceFieldModel>, ISupportRules, IMultipleChoiceFormField
    {
        public MultipleChoiceFieldController()
        {
            this.DisplayMode = FieldDisplayMode.Write;
        }

        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ReflectInheritedProperties]
        public override IMultipleChoiceFieldModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<IMultipleChoiceFieldModel>(this.GetType());

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

        IEnumerable<string> IMultipleChoiceFormField.Choices
        {
            get
            {
                return this.Model.DeserializeChoices();
            }
        }

        private IMultipleChoiceFieldModel model;
    }
}