using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.News.Model;

namespace Telerik.Sitefinity.Frontend.News.Mvc.Models
{
    /// <summary>
    /// This class represents model used for News widget.
    /// </summary>
    public class NewsModel : ContentModelBase, INewsModel
    {
        /// <summary>
        /// Gets or sets the type of content that is loaded.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public override Type ContentType
        {
            get
            {
                return typeof(NewsItem);
            }

            set
            {
            }
        }

        /// <summary>
        /// Gets the items query.
        /// </summary>
        /// <returns>The query.</returns>
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            return ((NewsManager)this.GetManager()).GetNewsItems();
        }
    }
}