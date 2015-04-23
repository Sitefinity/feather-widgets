using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Blogs;
using SfBlog = Telerik.Sitefinity.Blogs.Model.Blog;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.Models.Blog
{
    /// <summary>
    /// Provides API for working with <see cref="Telerik.Sitefinity.Blogs.Model.Blog"/> items.
    /// </summary>
    public class BlogModel : ContentModelBase, IBlogModel
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
                return typeof(SfBlog);
            }

            set
            {
            }
        }

        /// <inheritDoc/>
        public int MinPostsCount { get; set; }

        /// <inheritDoc/>
        public int MaxPostsAge { get; set; }

        /// <inheritDoc/>
        public FilteredSelectionMode FilteredSelectionMode { get; set; }

        /// <inheritDoc/>
        public ContentListViewModel CreateListViewModel(int page)
        {
            return base.CreateListViewModel(null, page);
        }
        
        protected override string AdaptMultilingualFilterExpression(string filterExpression)
        {
            return filterExpression;
        }

        protected override string AddLiveFilterExpression(string filterExpression)
        {
            return filterExpression;
        }

        /// <summary>
        /// Gets the items query.
        /// </summary>
        /// <returns>The query.</returns>
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            return ((BlogsManager)this.GetManager()).GetBlogs();
        }

        /// <summary>
        /// Compiles a filter expression based on the widget settings.
        /// </summary>
        /// <returns>Filter expression that will be applied on the query.</returns>
        protected override string CompileFilterExpression()
        {
            var elements = new List<string>();

            if (this.SelectionMode == SelectionMode.SelectedItems)
            {
                var selectedItemsFilterExpression = this.GetSelectedItemsFilterExpression();
                if (!selectedItemsFilterExpression.IsNullOrEmpty())
                {
                    elements.Add(selectedItemsFilterExpression);
                }
            }

            if (!this.FilterExpression.IsNullOrEmpty())
            {
                elements.Add(this.FilterExpression);
            }

            return string.Join(" AND ", elements.Select(el => "(" + el + ")"));
        }

        private string GetSelectedItemsFilterExpression()
        {
            IList<string> selectedItemsIds = new List<string>();
            if (!this.SerializedSelectedItemsIds.IsNullOrEmpty())
            {
                selectedItemsIds = JsonSerializer.DeserializeFromString<IList<string>>(this.SerializedSelectedItemsIds);
            }

            var selectedItemGuids = selectedItemsIds.Select(id => new Guid(id));
            var masterIds = this.GetItemsQuery()
                .Where(c => selectedItemGuids.Contains(c.Id))
                .Select(n => n.Id)
                .Distinct();

            var selectedItemConditions = masterIds.Select(id => "Id = {0}".Arrange(id.ToString("D")));
            var selectedItemsFilterExpression = string.Join(" OR ", selectedItemConditions);

            return selectedItemsFilterExpression;
        }
    }
}
