using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Document;
using SfDocument = Telerik.Sitefinity.Libraries.Model.Document;

namespace FeatherWidgets.TestUnit.DummyClasses.Media.Document
{
    public class DummyDocumentModel : DocumentModel
    {
        public DummyDocumentModel()
        {
        }

        public DummyDocumentModel(SfDocument doc)
        {
            this.sitefinityDoc = doc;
        }

        protected override bool TryGetDocument(out SfDocument document)
        {
            document = this.sitefinityDoc;
            return document != null;
        }

        protected override string ResolveMediaUrl(SfDocument document)
        {
            return "http://mysite.com/file.doc";
        }

        protected override string GetExtension(SfDocument document)
        {
            return "pdf";
        }

        protected override long GetFileSize(SfDocument document)
        {
            return 1;
        }

        protected override string GetTitle(SfDocument document)
        {
            return "title";
        }

        private readonly SfDocument sitefinityDoc;
    }
}
