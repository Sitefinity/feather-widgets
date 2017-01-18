using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Card.Mvc.Models.Card;
using Telerik.Sitefinity.Frontend.Card.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Card.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of the Card widget.
    /// </summary>
    [Localization(typeof(CardResources))]
    [ControllerToolboxItem(Name = "Card_MVC", Title = "Card", SectionName = ToolboxesConfig.ContentToolboxSectionName, CssClass = CardController.WidgetIconCssClass)]
    public class CardController : Controller, ICustomWidgetVisualizationExtended, IPersonalizable
    {
        #region Properties

        /// <summary>
        /// Gets the Card widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual ICardModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = ControllerModelFactory.GetModel<ICardModel>(this.GetType());

                return this.model;
            }
        }

        /// <summary>
        /// Gets or sets the name of the template that widget will be displayed.
        /// </summary>
        /// <value></value>
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

        /// <summary>
        /// Gets a value indicating whether widget is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if widget has no image selected; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return this.Model.IsEmpty();
            }
        }

        /// <inheritdoc />
        public string WidgetCssClass
        {
            get
            {
                return WidgetIconCssClass;
            }
        }

        /// <inheritdoc />
        public string EmptyLinkText
        {
            get
            {
                return Res.Get<CardResources>().CreateContent;
            }
        }

        /// <summary>
        /// Gets the is design mode.
        /// </summary>
        /// <value>The is design mode.</value>
        protected virtual bool IsDesignMode
        {
            get
            {
                return SystemManager.IsDesignMode;
            }
        }

        /// <summary>
        /// Gets the image was not selected or has been deleted message.
        /// </summary>
        /// <value>The image was not selected or has been deleted message.</value>
        protected virtual string ImageWasNotSelectedOrHasBeenDeletedMessage
        {
            get
            {
                return Res.Get<CardResources>().ImageWasNotSelectedOrHasBeenDeleted;
            }
        }

        #endregion

        #region Actions

        /// <summary>
        /// Renders appropriate list view depending on the <see cref="ListTemplateName" />
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index()
        {
            if(this.IsEmpty)
            {
                return new EmptyResult();
            }

            var viewModel = this.Model.GetViewModel();

            return View(this.TemplateName, viewModel);
        }

        /// <inheritDoc/>
        protected override void HandleUnknownAction(string actionName)
        {
            this.Index().ExecuteResult(this.ControllerContext);
        } 

        #endregion

        #region Private fields and constants

        internal const string WidgetIconCssClass = "sfCardIcn sfMvcIcn";
        private ICardModel model;

        private string templateName = "Card";

        #endregion
    }
}
