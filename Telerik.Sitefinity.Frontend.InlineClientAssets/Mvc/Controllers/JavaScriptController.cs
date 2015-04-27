using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.JavaScript;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Controllers
{
    public class JavaScriptController : Controller
    {
        #region Properties

        /// <summary>
        /// Gets the Account Activation widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IJavaScriptModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = this.InitializeModel();

                return this.model;
            }
        }

        /// <summary>
        /// Gets or sets the name of the template that widget will be displayed.
        /// </summary>
        /// <value>
        /// The name of the template that widget will be displayed.
        /// </value>
        public string TemplateName
        {
            get
            {
                return this.templateName;
            }

            set
            {
                this.templateName = value;
            }
        }

        #endregion

        #region Actions
        /// <summary>
        /// Renders appropriate view depending on the <see cref="TemplateName" />
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index()
        {
            var model = this.Model.GetViewModel();
            var fullTemplateName = this.templateNamePrefix + this.TemplateName;

            return this.View(fullTemplateName, model);
        }
        #endregion

        #region Private methods
        private IJavaScriptModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IJavaScriptModel>(this.GetType());
        }
        #endregion

        #region Private fields and constants
        private IJavaScriptModel model;
        private string templateName;
        private readonly string templateNamePrefix = "JavaScript.";
        #endregion
    }
}
