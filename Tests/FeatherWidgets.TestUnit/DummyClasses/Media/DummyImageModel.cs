using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image;
using Telerik.Sitefinity.Libraries.Model;

namespace FeatherWidgets.TestUnit.DummyClasses.Media
{
    internal class DummyImageModel : ImageModel
    {
        private readonly Image sitefinityImage;

        public DummyImageModel()
        {
        }

        public DummyImageModel(Image image)
        {
            this.sitefinityImage = image;
        }

        protected override Image GetImage()
        {
            return this.sitefinityImage;
        }

        protected override string GetSelectedSizeUrl(Image image)
        {
            return "GetSelectedSizeUrl";
        }
    }
}
