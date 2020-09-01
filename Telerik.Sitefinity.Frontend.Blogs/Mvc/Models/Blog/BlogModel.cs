﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ServiceStack.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Blogs;
using Telerik.Sitefinity.Services;
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
        public int MaxPostsAge
        {
            get
            {
                return this.maxPostsAge;
            }
            set
            {
                this.maxPostsAge = Math.Max(1, value);
            }
        }

        /// <inheritDoc/>
        public FilteredSelectionMode FilteredSelectionMode { get; set; }

        /// <inheritDoc/>
        public virtual ContentListViewModel CreateListViewModel(int page)
        {
            return base.CreateListViewModel(null, page);
        }

        /// <inheritDoc/>
        protected override string AdaptMultilingualFilterExpression(string filterExpression)
        {
            return filterExpression;
        }

        /// <inheritDoc/>
        protected override string AddLiveFilterExpression(string filterExpression)
        {
            return filterExpression;
        }

        /// <inheritDoc/>
        protected override ContentDetailsViewModel CreateDetailsViewModelInstance()
        {
            return new BlogDetailsViewModel();
        }

        /// <inheritDoc/>
        public override ContentDetailsViewModel CreateDetailsViewModel(IDataItem item)
        {
            var viewModel = base.CreateDetailsViewModel(item) as BlogDetailsViewModel;

            var manager = (BlogsManager)this.GetManager();
            var postsQuery = manager.GetBlogPosts().Where(bp => bp.Parent.Id == item.Id && bp.Status == ContentLifecycleStatus.Live);

            if(SystemManager.CurrentContext.AppSettings.Multilingual) 
            {
                var curentUiCulture = Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture.Name;
                postsQuery = postsQuery.Where(bp => bp.PublishedTranslations.Contains(curentUiCulture));
            }
            viewModel.PostsCount = postsQuery.Count();

            return viewModel;
        }

        /// <inheritDoc/>
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            var manager = (BlogsManager)this.GetManager();

            IQueryable<SfBlog> query = manager.GetBlogs();

            if (this.SelectionMode == Frontend.Mvc.Models.SelectionMode.FilteredItems)
            {
                if (this.FilteredSelectionMode == Blog.FilteredSelectionMode.MinPostsCount)
                {
                    var minPostsCount = this.MinPostsCount;
                    var blogIdsArray = manager.GetBlogPosts()
                        .Where(bp => bp.Status == ContentLifecycleStatus.Live && bp.Visible)
                        .GroupBy(bp => bp.Parent)
                        .Where(g => g.Count() > minPostsCount)
                        .Select(kv => kv.Key.Id)
                        .Distinct()
                        .ToArray();

                    query = query.Where(b => blogIdsArray.Contains(b.Id));
                }
                else if (this.FilteredSelectionMode == Blog.FilteredSelectionMode.MaxPostsAge)
                {
                    var minPublicationDate = DateTime.UtcNow.AddMonths(this.MaxPostsAge * -1);
                    var blogIdsArray = manager.GetBlogPosts()
                        .Where(bp => bp.Status == ContentLifecycleStatus.Live && bp.Visible && bp.PublicationDate > minPublicationDate)
                        .Select(bp => bp.Parent.Id)
                        .Distinct()
                        .ToArray();

                    query = query.Where(b => blogIdsArray.Contains(b.Id));
                }
            }

            return query;
        }

        // Base SetExpression applyes ILifecycleDataItemGeneric filter when sort is AsSetManually, which is not applicable on blogs.
        /// <inheritDoc/>
        protected override IQueryable<TItem> SetExpression<TItem>(IQueryable<TItem> query, string filterExpression, string sortExpr, int? itemsToSkip, int? itemsToTake, ref int? totalCount)
        {
            if (sortExpr == "AsSetManually")
            {
                IList<string> selectedItemsIds = new List<string>();
                if (!this.SerializedSelectedItemsIds.IsNullOrEmpty())
                {
                    selectedItemsIds = JsonSerializer.DeserializeFromString<IList<string>>(this.SerializedSelectedItemsIds);
                }

                query = DataProviderBase.SetExpressions(
                                                  query,
                                                  filterExpression,
                                                  string.Empty,
                                                  null,
                                                  null,
                                                  ref totalCount);

                query = query.Select(x => new
                    {
                        item = x,
                        orderIndex = selectedItemsIds.IndexOf(x.Id.ToString())
                    })
                    .OrderBy(x => x.orderIndex)
                    .Select(x => x.item)
                    .OfType<TItem>();

                if (itemsToSkip.HasValue && itemsToSkip.Value > 0)
                {
                    query = query.Skip(itemsToSkip.Value);
                }

                if (itemsToTake.HasValue && itemsToTake.Value > 0)
                {
                    query = query.Take(itemsToTake.Value);
                }
            }
            else
            {
                query = base.SetExpression<TItem>(query, filterExpression, sortExpr, itemsToSkip, itemsToTake, ref totalCount);
            }

            return query;
        }

        /// <inheritDoc/>
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

        protected override string GetSelectedItemsFilterExpression()
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

        private int maxPostsAge = 1;
    }
}
