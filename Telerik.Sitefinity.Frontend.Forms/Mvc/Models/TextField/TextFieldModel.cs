using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.TextField
{
    public class TextFieldModel : FormsFieldModel, ITextFieldModel
    {
        /// <inheritDocs />
        public string PlaceholderText
        {
            get;
            set;
        }
    }
}