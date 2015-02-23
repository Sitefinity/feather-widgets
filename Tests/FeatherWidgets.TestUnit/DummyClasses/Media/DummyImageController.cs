using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.Media.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;

namespace FeatherWidgets.TestUnit.DummyClasses.Media
{
    public class DummyImageController : ImageController
    {
        private readonly IImageModel model;

        public DummyImageController(IImageModel model)
        {
            this.model = model;
        }

        public override IImageModel Model
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

        protected override string ImageWasNotSelectedOrHasBeenDeletedMessage
        {
            get
            {
                return "ImageWasNotSelectedOrHasBeenDeletedMessage";
            }
        }
    }
}
