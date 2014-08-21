using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.News.Model;

namespace News.Mvc.Models
{
    public interface INewsModel
    {
        /// <summary>
        /// Gets the list of news to be displayed inside the widget.
        /// </summary>
        /// <value>
        /// The news collection.
        /// </value>
        [Browsable(false)]
        IList<NewsItem> News { get; }

        /// <summary>
        /// Gets the list of news to be displayed inside the widget when option "Selected news" is enabled.
        /// </summary>
        /// <value>
        /// The selected news items.
        /// </value>
        [Browsable(false)]
        IList<NewsItem> SelectedNews { get; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the NewsWidget when it is in List view.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string ListCssClass { get; set; }

        /// <summary>
        /// Gets or sets the detail news.
        /// </summary>
        /// <value>
        /// The detail news.
        /// </value>
        [Browsable(false)]
        NewsItem DetailNews { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the NewsWidget when it is in Details view.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string DetailCssClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable social sharing.
        /// </summary>
        /// <value>
        ///   <c>true</c> if should enable social sharing; otherwise, <c>false</c>.
        /// </value>
        bool EnableSocialSharing { get; set; }

        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        /// <value>The name of the provider.</value>
        string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable paging.
        /// </summary>
        /// <value>
        ///   <c>true</c> if paging is enabled; otherwise, <c>false</c>.
        /// </value>
        bool EnablePaging { get; set; }

        /// <summary>
        /// Gets or sets the items count per page.
        /// </summary>
        /// <value>
        /// The items per page.
        /// </value>
        int ItemsPerPage { get; set; }

        /// <summary>
        /// Populates the news.
        /// </summary>
        /// <param name="page">The page.</param>
        void PopulateNews(int? page);
    }
}
