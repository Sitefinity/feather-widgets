using System;
using System.Linq;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.Breadcrumb
{
    public class BreadcrumbNodeModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreadcrumbNodeModel" /> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="title">The title.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#")]
        public BreadcrumbNodeModel(string url, string title)
        {
            this.Url = url;

            this.Title = title;
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string Url { get; private set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; private set; }
    }
}
