﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.MultipleChoiceField
{
    /// <summary>
    /// Implements API for working with form multiple choice fields.
    /// </summary>
    public class MultipleChoiceFieldModel : FormFieldModel, IMultipleChoiceFieldModel
    {
        /// <inheritDocs />
        public bool HasOtherChoice { get; set; }

        /// <inheritDocs />
        public string SerializedChoices
        {
            get
            {
                if (string.IsNullOrEmpty(this.serializedChoices))
                {
                    this.serializedChoices = this.BuildInitialChoicesString();
                }

                return this.serializedChoices;
            }
            set
            {
                this.serializedChoices = value;
            }
        }

        /// <inheritDocs />
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public override ValidatorDefinition ValidatorDefinition
        {
            get
            {
                if (this.validatorDefinition == null)
                {
                    this.validatorDefinition = base.ValidatorDefinition;

                    this.validatorDefinition.RequiredViolationMessage = Res.Get<FieldResources>().RequiredErrorMessageValue;
                }

                return this.validatorDefinition;
            }
            set
            {
                this.validatorDefinition = value;
            }
        }

        /// <inheritDocs />
        public override IMetaField GetMetaField(IFormFieldControl formFieldControl)
        {
            var metaField = base.GetMetaField(formFieldControl);

            if (string.IsNullOrEmpty(metaField.Title))
            {
                metaField.Title = Res.Get<FieldResources>().SelectChoice;
            }

            return metaField;
        }

        /// <inheritDocs />
        public override object GetViewModel(object value, IMetaField metaField)
        {
            this.Value = value;
            var viewModel = new MultipleChoiceFieldViewModel()
            {
                Choices = this.DeserializeChoices(),
                Value = value,
                MetaField = this.MetaField,
                HasOtherChoice = this.HasOtherChoice,
                IsRequired = this.ValidatorDefinition.Required.HasValue ? this.ValidatorDefinition.Required.Value : false,
                ValidationAttributes = this.BuildValidationAttributesString(),
                RequiredViolationMessage = this.ValidatorDefinition.RequiredViolationMessage,
                CssClass = this.CssClass
            };

            return viewModel;
        }

        /// <summary>
        /// Prepopulates the initial choices.
        /// </summary>
        /// <returns></returns>
        public virtual string BuildInitialChoicesString()
        {
            var initialChoices = new string[]
            {
                Res.Get<FieldResources>().OptionFirstChoice,
                Res.Get<FieldResources>().OptionSecondChoice,
                Res.Get<FieldResources>().OptionThirdChoice
            };

            return JsonConvert.SerializeObject(initialChoices);
        }

        /// <inheritDocs />
        public override bool IsValid(object value)
        {
            if (this.ValidatorDefinition.Required.HasValue && this.ValidatorDefinition.Required.Value && !this.HasOtherChoice)
            {
                var strValue = value as string;
                if (!string.IsNullOrEmpty(strValue))
                {
                    var choices = this.DeserializeChoices();
                    if (choices.Contains(strValue))
                    {
                        return base.IsValid(value);
                    }
                }

                return false;
            }
            else
            {
                return base.IsValid(value);
            }
        }

        public IEnumerable<string> DeserializeChoices()
        {
            if (string.IsNullOrEmpty(this.SerializedChoices))
            {
                return Enumerable.Empty<string>();
            }

            return JsonConvert.DeserializeObject<IEnumerable<string>>(this.SerializedChoices);
        }

        /// <inheritDocs />
        private string BuildValidationAttributesString()
        {
            if (this.ValidatorDefinition.Required.HasValue && this.ValidatorDefinition.Required.Value)
            {
                return "required='required'";
            }

            return string.Empty;
        }

        private ValidatorDefinition validatorDefinition;
        private string serializedChoices;
    }
}
