using System;
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
    public class TextFieldController : Controller, IFormFieldControl
    {
        public ActionResult Read(object value)
        {
            var stringValue = value != null ? value.ToString() : "[no value]";
            return this.Content("Read me: " + stringValue);
        }

        public ActionResult Write()
        {
            return this.View("Write", this.MetaField);
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
                    string validFieldName;
                    if (MetaDataExtensions.TryCreateValidFieldName(this.GetType().Name, out validFieldName))
                    {
                        this.metaField.FieldName = validFieldName + Guid.NewGuid().ToString("N");
                    }
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