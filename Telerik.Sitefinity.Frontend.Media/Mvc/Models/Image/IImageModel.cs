﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image
{
    /// <summary>
    /// This interface is used as a model for the ImageController.
    /// </summary>
    public interface IImageModel
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
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        string Title { get; set; }
        
        /// <summary>
        /// Gets or sets the alternative text.
        /// </summary>
        /// <value>
        /// The alternative text.
        /// </value>
        string AlternativeText { get; set; }

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        /// <value>
        /// The css class.
        /// </value>
        string CssClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use image as link.
        /// </summary>
        /// <value>
        ///   <c>true</c> if should use image as link; otherwise, <c>false</c>.
        /// </value>
        bool UseAsLink { get; set; }

        /// <summary>
        /// Gets or sets the page identifier to use as link.
        /// </summary>
        /// <value>
        /// The page identifier to use as link.
        /// </value>
        Guid LinkedPageId { get; set; }

        /// <summary>
        /// Gets or sets whether the Image will be displayed in its original size or in a thumbnail.
        /// </summary>
        /// <value>
        /// The display mode.
        /// </value>
        ImageDisplayMode DisplayMode { get; set; }

        /// <summary>
        /// Gets or sets the name of the thumbnail.
        /// </summary>
        /// <value>
        /// The name of the thumbnail.
        /// </value>
        string ThumbnailName { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail URL.
        /// </summary>
        /// <value>
        /// The thumbnail URL.
        /// </value>
        string ThumbnailUrl { get; set; }

        /// <summary>
        /// Gets or sets the size of the custom thumbnail that is selected.
        /// </summary>
        /// <value>
        /// The size of the custom thumbnail serialized as JSON.
        /// </value>
        string CustomSize { get; set; }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <returns></returns>
        ImageViewModel GetViewModel();

        /// <summary>
        /// Gets the information for all <see cref="Image"/> items that can be displayed by an Image widget.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IEnumerable<IContentLocationInfo> GetLocations();
    }
}