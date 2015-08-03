using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "MvcTextField", Title = "MvcTextField", Toolbox = "FormControls", SectionName = "Common")]
    [DatabaseMapping(UserFriendlyDataType.ShortText)]
    public class TextFieldController : Controller, IFormFieldControl, IHasValue
    {
        public ActionResult Read()
        {
            var stringValue = this.Value != null ? this.Value.ToString() : "[no value]";
            return this.Content("Read me: " + stringValue);
        }

        public ActionResult Write()
        {
            var content = string.Format("<input type=\"text\" name=\"{0}\"></input>", this.MetaField.FieldName ?? string.Empty);
            return this.Content(content);
        }

        public bool IsValid()
        {
            return true;
        }

        public object Value { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IMetaField MetaField
        {
            get
            {
                if (this.metaField == null)
                {
                    this.metaField = this.LoadDefaultMetaField();
                }

                return this.metaField;
            }
            set
            {
                this.metaField = value;
            }
        }

        private IMetaField metaField;

        protected override void HandleUnknownAction(string actionName)
        {
            this.Write().ExecuteResult(this.ControllerContext);
        }
    }
}