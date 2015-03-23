using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.Media.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Document;

namespace FeatherWidgets.TestUnit.DummyClasses.Media.Document
{
    public class DummyDocumentController : DocumentController
    {
        private readonly IDocumentModel model;

        public DummyDocumentController(IDocumentModel model)
        {
            this.model = model;
        }

        public override IDocumentModel Model
        {
            get
            {
                return this.model ?? base.Model;
            }
        }

        protected override bool IsDesignMode
        {
            get
            {
                return true;
            }
        }

        protected override string DocumentWasNotSelectedOrHasBeenDeletedMessage
        {
            get
            {
                return "DocumentWasNotSelectedOrHasBeenDeletedMessage";
            }
        }
    }
}
