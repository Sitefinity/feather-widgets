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
        /// Initializes a new instance of the <see cref="ResultModel"/> class.
        /// </summary>
        public ResultModel()
        {
            this.Data = new List<IDocument>();
            this.TotalCount = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultModel"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="totalCount">The total count.</param>
        public ResultModel(IList<IDocument> data, int totalCount)
        {
            this.Data = data;
            this.TotalCount = totalCount;
        }

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
