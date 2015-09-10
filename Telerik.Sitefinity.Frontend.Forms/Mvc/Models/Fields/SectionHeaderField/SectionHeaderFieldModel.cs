﻿using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SectionHeaderField
{
    /// <summary>
    /// Implements API for working with form section header fields.
    /// </summary>
    public class SectionHeaderFieldModel : FormFieldModel, ISectionHeaderFieldModel, IFormFieldModel
    {
        /// <inheritDocs />
        public string Text { get; set; }

        /// <inheritDocs />
        public string PlaceholderText
        {
            get
            {
                if (string.IsNullOrEmpty(this.placeholderText))
                {
                    return Res.Get<FieldResources>().SectionHeaderPlaceholderText;
                }

                return this.placeholderText;
            }
            set
            {
                this.placeholderText = value;
            }
        }

        /// <inheritDocs />
        public HeadingType HeadingType { get; set; }

        /// <inheritDocs />
        public SectionHeaderFieldViewModel GetViewModel()
        {
            return new SectionHeaderFieldViewModel()
            {
                CssClass = this.CssClass,
                HeadingType = this.HeadingType,
                Text = this.Text
            };
        }

        private string placeholderText;
    }
}
