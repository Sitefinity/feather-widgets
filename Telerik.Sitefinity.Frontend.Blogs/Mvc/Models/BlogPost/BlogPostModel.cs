﻿using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Text;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Blogs;
using SfBlog = Telerik.Sitefinity.Blogs.Model.Blog;
using SfBlogPost = Telerik.Sitefinity.Blogs.Model.BlogPost;

namespace Telerik.Sitefinity.Frontend.Blogs.Mvc.Models.BlogPost
{
    /// <summary>
    /// Provides API for working with <see cref="Telerik.Sitefinity.Blogs.Model.BlogPost"/> items.
    /// </summary>
    public class BlogPostModel : ContentModelBase, IBlogPostModel
    {
        /// <inheritdoc />
        public ParentFilterMode ParentFilterMode { get; set; }

        /// <inheritdoc />
        public string SerializedSelectedParentsIds { get; set; }

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
                return typeof(SfBlogPost);
            }

            set
            {
            }
        }

        /// <inheritdoc />
        public virtual ContentListViewModel CreateListViewModelByParent(SfBlog parentItem, int page)
        {
            if (page < 1)
                throw new ArgumentException("'page' argument has to be at least 1.", "page");

            var query = ((BlogsManager)this.GetManager()).GetBlogPosts().Where(bp => bp.Parent.Id == parentItem.Id);
            if (query == null)
                return this.CreateListViewModelInstance();

            var viewModel = this.CreateListViewModelInstance();
            this.PopulateListViewModel(page, query, viewModel);

            return viewModel;
        }

        /// <summary>
        /// Gets the items query.
        /// </summary>
        /// <returns>The query.</returns>
        protected override IQueryable<IDataItem> GetItemsQuery()
        {
            return ((BlogsManager)this.GetManager()).GetBlogPosts();
        }

        /// <inheritdoc />
        protected override string CompileFilterExpression()
        {
            var baseExpression = base.CompileFilterExpression();

            if (this.ParentFilterMode == ParentFilterMode.Selected && this.SerializedSelectedParentsIds.IsNullOrEmpty() == false)
            {
                var selectedItemIds = JsonSerializer.DeserializeFromString<IList<string>>(this.SerializedSelectedParentsIds);
                var parentFilterExpression = string.Join(" OR ", selectedItemIds.Select(id => "Parent.Id = " + id.Trim()));

                if (parentFilterExpression.IsNullOrEmpty())
                {
                    return null;
                }
                else
                {
                    if (baseExpression.IsNullOrEmpty())
                        return "({0})".Arrange(parentFilterExpression);
                    else
                        return "({0}) and ({1})".Arrange(baseExpression, parentFilterExpression);
                }
            }
            else
            {
                return baseExpression;
            }
        }
    }
}
