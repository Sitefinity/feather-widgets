using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.TextField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "MvcTextField", Title = "Text box", Toolbox = "FormControls", SectionName = "Common")]
    [DatabaseMapping(UserFriendlyDataType.ShortText)]
    [Localization(typeof(FieldResources))]
    public class TextFieldController : Controller, IFormFieldControl
    {
        #region Properties

        /// <summary>
        /// Gets the text field field model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual ITextFieldModel Model
        {
            get
            {
                if (this.model == null)
                {
                    this.model = ControllerModelFactory.GetModel<ITextFieldModel>(this.GetType());
                }

                return this.model;
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IMetaField MetaField
        {
            get
            {
                if (this.metaField == null)
                {
                    this.metaField = this.Model.GetMetaField(this);
                }

                return this.metaField;
            }
            set
            {
                this.metaField = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the template that will be displayed when widget is in write view.
        /// </summary>
        /// <value></value>
        public string WriteTemplateName
        {
            get
            {
                return this.writeTemplateName;
            }

            set
            {
                this.writeTemplateName = value;
            }
        }

        #endregion

        #region Actions

        public ActionResult Read(object value)
        {
            var stringValue = value != null ? value.ToString() : "[no value]";
            return this.Content("Read me: " + stringValue);
        }

        public ActionResult Write(object value)
        {
            this.Model.Value = value;
            var fullTemplateName = this.templateNamePrefix + this.WriteTemplateName;
            this.ViewBag.MetaField = this.MetaField;

            return this.View(fullTemplateName, this.Model);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return this.Model.IsValid();
        }

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.Write(null).ExecuteResult(this.ControllerContext);
        }

        #endregion


        private string writeTemplateName = "Default";
        private readonly string templateNamePrefix = "Write.";
        private IMetaField metaField;
        private ITextFieldModel model;
    }
}