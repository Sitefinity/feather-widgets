using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.LanguageSelector;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.StringResources;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of Language selector widget.
    /// </summary>
    [ControllerToolboxItem(Name = "LanguageSelector_MVC",
        Title = "Language selector",
        SectionName = ToolboxesConfig.NavigationControlsSectionName,
        CssClass = LanguageSelectorController.WidgetIconCssClass)]
    [Localization(typeof(LanguageSelectorResources))]
    [IndexRenderMode(IndexRenderModes.NoOutput)]
    public class LanguageSelectorController : Controller
    {
        #region Properties
        
        /// <summary>
        /// Gets or sets the name of the template that will be displayed.
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
        /// Gets the Language selector widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual ILanguageSelectorModel Model 
        {
            get
            {
                if (this.model == null)
                    this.model = this.InitializeModel();
                
                return this.model;
            }
        }
        
        /// <summary>
        /// Gets the Language selector widget on template message.
        /// </summary>
        /// <returns></returns>
        protected virtual string LanguageSelectorOnTemplateMessage
        {
            get
            {
                return Res.Get<LanguageSelectorResources>().LanguageSelectorOnTemplateMessage;
            }
        }
        
        /// <summary>
        /// Gets the current HTTP context from the SystemManager.
        /// </summary>
        /// <value>The current HTTP context.</value>
        protected virtual HttpContextBase CurrentHttpContext
        {
            get
            {
                return SystemManager.CurrentHttpContext;
            }
        }
        
        /// <summary>
        /// Gets whether the page is in edit mode.
        /// </summary>
        /// <value>The is edit.</value>
        private bool IsEdit
        {
            get
            {
                var isEdit = false;
                if (SystemManager.IsDesignMode && !SystemManager.IsPreviewMode)
                {
                    isEdit = true;
                }

                return isEdit;
            }
        }
        
        #endregion
        
        #region Action
        
        /// <summary>
        /// Renders appropriate view depending on the <see cref="TemplateName"/>
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            var context = this.CurrentHttpContext;
            
            if (context.Items.Contains("IsTemplate") && (bool)context.Items["IsTemplate"])
            {
                return this.Content(this.LanguageSelectorOnTemplateMessage);
            }
            
            this.viewModel = this.Model.CreateViewModel();

            var page = context.CurrentHandler.GetPageHandler();
            
            if (page != null && !this.IsEdit)
            {
                page.PreRenderComplete += this.PagePreRenderCompleteHandler;
            }
            
            var fullTemplateName = this.templateNamePrefix + this.TemplateName;

            return this.View(fullTemplateName, this.viewModel);
        }
        
        #endregion
        
        #region Overridden methods
        
        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.Index().ExecuteResult(this.ControllerContext);
        }
        
        #endregion
        
        #region Private methods
        
        /// <summary>
        /// Appends the detail item and parameters to URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns></returns>
        private string AppendDetailItemAndParamsToUrl(string url, CultureInfo culture)
        {
            var query = this.HttpContext.Request.QueryString.ToQueryString();
            var detailItem = SystemManager.CurrentHttpContext.Items["detailItem"];
            var extendedItem = detailItem as ILocatableExtended;
            
            var dataItemAsLifecycleDataItem = detailItem as ILifecycleDataItem;
            
            if (extendedItem == null || dataItemAsLifecycleDataItem == null)
                return string.Concat(url, query);
            
            string currentCultureContentItemUrl = string.Empty;
            var isPublishedLiveItem = dataItemAsLifecycleDataItem.IsPublishedInCulture(culture);
            if (isPublishedLiveItem)
            {
                currentCultureContentItemUrl = extendedItem.ItemDefaultUrl.GetString(culture, false);
            }
            
            var urlWithParams = string.Concat(url, currentCultureContentItemUrl, query);
            
            return urlWithParams;
        }
        
        /// <summary>
        /// Handler called when the Page's PreRenderComplete event is fired.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void PagePreRenderCompleteHandler(object sender, EventArgs e)
        {
            var page = (Page)sender;

            foreach (var item in this.viewModel.Languages)
            {
                var link = this.AppendDetailItemAndParamsToUrl(item.Url, new CultureInfo(item.Culture));

                page.Controls.Add(new LiteralControl()
                    {
                        Text = string.Format(CultureInfo.InvariantCulture, "<input data-sf-role='{0}' type='hidden' value='{1}'>", item.Culture, link)
                    });
            }
        }
        
        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="ILanguageSelectorModel"/>.
        /// </returns>
        private ILanguageSelectorModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<ILanguageSelectorModel>(this.GetType());
        }
        
        #endregion
        
        #region Private fields and constants
        
        private ILanguageSelectorModel model;
        internal const string WidgetIconCssClass = "sfLanguageSelectorIcn sfMvcIcn";
        private string templateNamePrefix = "LanguageSelector.";
        private string templateName = "LanguageLinks";
        private LanguageSelectorViewModel viewModel;

        #endregion
    }
}