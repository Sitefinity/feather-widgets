using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers.Base;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.TextField;
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
    /// This class represents the controller of the MVC forms text field.
    /// </summary>
    [DatabaseMapping(UserFriendlyDataType.ShortText)]
    [Localization(typeof(FieldResources))]
    [ControllerToolboxItem(
        Name = "MvcTextField", 
        Toolbox = FormsConstants.FormControlsToolboxName,
        SectionName = FormsConstants.CommonSectionName,
        Title = "Textbox", 
        CssClass = "sfTextboxIcn sfMvcIcn",
        Ordinal = 0.1f)]
    public class TextFieldController : FormFieldControllerBase<ITextFieldModel>, ISupportRules, ITextField
    {
        public TextFieldController()
        {
            this.DisplayMode = FieldDisplayMode.Write;
        }

        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [ReflectInheritedProperties]
        public override ITextFieldModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<ITextFieldModel>(this.GetType());

                return this.model;
            }
        }

        IDictionary<ConditionOperator, string> ISupportRules.Operators
        {
            get
            {
                switch (this.Model.InputType)
                {
                    case TextType.Color:
                        return new Dictionary<ConditionOperator, string>()
                        {
                            [ConditionOperator.Equal] = Res.Get<Labels>().IsOperator,
                            [ConditionOperator.NotEqual] = Res.Get<Labels>().IsNotOperator
                        };
                    case TextType.Number:
                    case TextType.Range:
                        return new Dictionary<ConditionOperator, string>()
                        {
                            [ConditionOperator.Equal] = Res.Get<Labels>().IsEqualOperator,
                            [ConditionOperator.NotEqual] = Res.Get<Labels>().IsNotEqualOperator,
                            [ConditionOperator.IsGreaterThan] = Res.Get<Labels>().IsGreaterOperator,
                            [ConditionOperator.IsLessThan] = Res.Get<Labels>().IsLessOperator,
                            [ConditionOperator.IsFilled] = Res.Get<Labels>().IsFilledOperator,
                            [ConditionOperator.IsNotFilled] = Res.Get<Labels>().IsNotFilledOperator
                        };
                    case TextType.Date:
                    case TextType.Time:
                    case TextType.Month:
                    case TextType.Week:
                    case TextType.DateTimeLocal:
                        return new Dictionary<ConditionOperator, string>()
                        {
                            [ConditionOperator.Equal] = Res.Get<Labels>().IsOperator,
                            [ConditionOperator.IsLessThan] = Res.Get<Labels>().IsBeforeOperator,
                            [ConditionOperator.IsGreaterThan] = Res.Get<Labels>().IsAfterOperator,
                            [ConditionOperator.IsFilled] = Res.Get<Labels>().IsFilledOperator,
                            [ConditionOperator.IsNotFilled] = Res.Get<Labels>().IsNotFilledOperator
                        };
                    case TextType.Hidden:
                        return new Dictionary<ConditionOperator, string>();
                    default:
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
        }

        TextType ITextField.InputType
        {
            get
            {
                return this.Model.InputType;
            }
        }

        string ISupportRules.Title
        {
            get
            {
                return this.MetaField.Title;
            }
        }

        private ITextFieldModel model;
    }
}