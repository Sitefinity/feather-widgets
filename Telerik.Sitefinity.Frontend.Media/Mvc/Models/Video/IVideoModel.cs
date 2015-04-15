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
        /// Gets or sets the width of the <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        int Height { get; set; }

        /// <summary>
        /// Gets the actual width of the <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        int OriginallWidth { get; set; }

        /// <summary>
        /// Gets the actual height of the <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> item.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        int OriginalHeight { get; set; }

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

        /// <summary>
        /// Gets the information for all <see cref="Telerik.Sitefinity.Libraries.Model.Video"/> items that can be displayed by a Video widget.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IEnumerable<IContentLocationInfo> GetLocations();
    }
}
