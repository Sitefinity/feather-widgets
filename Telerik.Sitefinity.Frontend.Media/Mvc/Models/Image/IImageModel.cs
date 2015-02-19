﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image
{
    /// <summary>
    /// This interface is used as a model for the ImageController.
    /// </summary>
    public interface IImageModel
    {
        Guid Id { get; set; }

        string Markup { get; set; }

        string ProviderName { get; set; }

        string CssClass { get; set; }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <returns></returns>
        ImageViewModel GetViewModel();
    }
}