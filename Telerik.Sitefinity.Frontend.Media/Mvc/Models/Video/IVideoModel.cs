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
        /// Gets the view model.
        /// </summary>
        /// <returns></returns>
        VideoViewModel GetViewModel();
    }
}
