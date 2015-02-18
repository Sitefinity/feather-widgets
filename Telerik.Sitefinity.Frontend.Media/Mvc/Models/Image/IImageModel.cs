﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models
{
    /// <summary>
    /// This interface is used as a model for the ImageController.
    /// </summary>
    public interface IImageModel
    {
        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <returns></returns>
        ImageViewModel GetViewModel();
    }
}