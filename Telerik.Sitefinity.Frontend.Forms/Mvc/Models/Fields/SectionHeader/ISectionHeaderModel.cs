﻿using System.ComponentModel;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.SectionHeader
{
    /// <summary>
    /// This interface provides API for form section header element.
    /// </summary>
    public interface ISectionHeaderModel : IFormElementModel
    {
        /// <summary>
        /// Gets or sets the placeholder text.
        /// </summary>
        /// <value>
        /// The placeholder text.
        /// </value>
        string PlaceholderText { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        string Text { get; set; }

        /// <summary>
        /// Gets or sets the type of the heading.
        /// </summary>
        /// <value>
        /// The type of the heading.
        /// </value>
        HeadingType HeadingType { get; set; }
    }
}
