using ServiceStack.Text;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.NavigationField
{
    /// <summary>
    /// Implements API for working with form navigation fields.
    /// </summary>
    public class NavigationFieldModel : FormElementModel, INavigationFieldModel
    {
        /// <inheritDocs />
        public string SerializedPages
        {
            get
            {
                if (this.serializedPages == null)
                {
                    string defaultPageTitle = Res.Get<FieldResources>().PageName + "1";
                    var defaultPage = new FormPage() { Title = defaultPageTitle, Index = 0 };
                    IEnumerable<FormPage> defaultPages = new FormPage[] { defaultPage };
                    this.serializedPages = JsonSerializer.SerializeToString(defaultPages);
                }

                return serializedPages;
            }
            set
            {
                this.serializedPages = value;
            }
        }

        /// <inheritDocs />
        public IEnumerable<FormPage> DeserializePages()
        {
            if (string.IsNullOrEmpty(this.SerializedPages))
            {
                return Enumerable.Empty<FormPage>();
            }

            return JsonSerializer.DeserializeFromString<IEnumerable<FormPage>>(this.SerializedPages);
        }

        /// <inheritDocs />
        public override object GetViewModel(object value)
        {
            this.Value = value;
            var viewModel = new NavigationFieldViewModel()
            {
                Pages = this.DeserializePages(),
                Value = value,
                CssClass = this.CssClass
            };

            return viewModel;
        }

        private string serializedPages;
    }
}
