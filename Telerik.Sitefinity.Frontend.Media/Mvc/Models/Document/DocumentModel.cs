using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Libraries;
using SfDocument = Telerik.Sitefinity.Libraries.Model.Document;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Document
{
    public class DocumentModel :IDocumentModel
    {
        #region Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentModel" /> class.
        /// </summary>
        public DocumentModel()
        {
        }
        #endregion

        #region IDocumentModel implementation
        /// <inheritdoc />
        public Guid Id { get; set; }

        /// <inheritdoc />
        public string ProviderName { get; set; }

        /// <inheritdoc />
        public string CssClass { get; set; }

        /// <inheritdoc />
        public DocumentViewModel GetViewModel()
        {
            var viewModel = new DocumentViewModel()
            {               
                CssClass = this.CssClass
            };

            if (this.Id != Guid.Empty)
            {
                var document = this.GetDocument();
                viewModel.MediaUrl = document.ResolveMediaUrl();
                viewModel.Title = document.Title;
            }
            else
            {
                viewModel.DocumentWasNotFound = true;
            }

            return viewModel;
        }

        /// <inheritdoc />
        public IEnumerable<IContentLocationInfo> GetLocations()
        {
            var location = new ContentLocationInfo();
            location.ContentType = typeof(SfDocument);
            location.ProviderName = this.ProviderName;

            var imageItem = LibrariesManager.GetManager(this.ProviderName).GetImage(this.Id);
            var filterExpression = string.Format("(Id = {0} OR OriginalContentId = {1})", this.Id.ToString("D"), imageItem.OriginalContentId);
            location.Filters.Add(new BasicContentLocationFilter(filterExpression));

            return new[] { location };
        }
        #endregion

        #region Protected methods
        /// <summary>
        /// Gets the document.
        /// </summary>
        /// <returns></returns>
        protected virtual SfDocument GetDocument()
        {
            LibrariesManager librariesManager = LibrariesManager.GetManager(this.ProviderName);
            return librariesManager.GetDocuments()
                .Where(i => i.Id == this.Id)
                .Where(PredefinedFilters.PublishedItemsFilter<SfDocument>())
                .FirstOrDefault();
        }
        #endregion
    }
}
