using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Telerik.Sitefinity.ContentLocations;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Document
{
    /// <summary>
    /// This interface is used as a model for the DocumentController.
    /// </summary>
    public interface IDocumentModel
    {
        /// <summary>
        /// Gets or sets the image identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        /// <value>
        /// The name of the provider.
        /// </value>
        string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        /// <value>
        /// The css class.
        /// </value>
        string CssClass { get; set; }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <returns></returns>
        DocumentViewModel GetViewModel();

        /// <summary>
        /// Gets the information for all <see cref="Document"/> items that can be displayed by an Document link widget.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IEnumerable<IContentLocationInfo> GetLocations();
    }
}
