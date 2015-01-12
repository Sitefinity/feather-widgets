using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Services.Search.Data;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    /// <summary>
    /// This class represents the DTO used to serve search results
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>
        /// The results.
        /// </value>
        public IList<IDocument> Data { get; set; }

        /// <summary>
        /// Gets or sets the total results count.
        /// </summary>
        /// <value>
        /// The total results count.
        /// </value>
        public int TotalCount { get; set; }
    }
}
