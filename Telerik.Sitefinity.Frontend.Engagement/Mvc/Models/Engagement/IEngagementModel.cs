using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Frontend.Engagement.Mvc.Models.Engagement
{
    /// <summary>
    /// This interface defines API for working with <see cref="Telerik.Sitefinity.Engagement.Model.Blog"/> items.
    /// </summary>
    public interface IEngagementModel
    {
        /// <summary>
        /// Gets or sets the image identifier.
        /// </summary>
        Guid ImageId { get; set; }

        /// <summary>
        /// Gets or sets the name of the image provider.
        /// </summary>
        string ImageProviderName { get; set; }

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the HTML.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the page identifier to use as link.
        /// </summary>
        Guid LinkedPageId { get; set; }

        /// <summary>
        /// Gets or sets the page url to use as link.
        /// </summary>
        string LinkedUrl { get; set; }

        /// <summary>
        /// Gets or sets the action name.
        /// </summary>
        string ActionName { get; set; }

        /// <summary>
        /// Gets or sets the heading.
        /// </summary>
        string Heading { get; set; }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <returns></returns>
        EngagementViewModel GetViewModel();
    }
}
