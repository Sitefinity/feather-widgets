using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Telerik.Sitefinity.ContentLocations;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Video
{
    /// <summary>
    /// Defines API for working with <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> items.
    /// </summary>
    public interface IVideoModel
    {
        /// <summary>
        /// Gets or sets the Id  of the selected <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item.
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
        /// Gets or sets the ascpect ration of the selected <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item.
        /// </summary>
        /// <value>
        /// The aspect ratio.
        /// </value>
        string AspectRatio { get; set; }

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
        VideoViewModel GetViewModel();
    }
}
