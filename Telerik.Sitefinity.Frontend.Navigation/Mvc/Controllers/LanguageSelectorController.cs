using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.LanguageSelector;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.StringResources;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of Language selector widget.
    /// </summary>
    [ControllerToolboxItem(Name = "LanguageSelector_MVC", Title = "Language selector", SectionName = ToolboxesConfig.NavigationControlsSectionName)]
    [Localization(typeof(LanguageSelectorResources))]
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
            var viewModel = this.Model.CreateViewModel();

            foreach (var item in viewModel.Languages)
            {
                item.Url = this.AppendDetailItemAndParamsToUrl(item.Url, new CultureInfo(item.Culture));
            }

            var fullTemplateName = this.templateNamePrefix + this.TemplateName;

            return this.View(fullTemplateName, viewModel);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Appends the detail item and parameters to URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        protected string AppendDetailItemAndParamsToUrl(string url, CultureInfo culture)
        {
            var query = this.HttpContext.Request.QueryString;
            var detailItem = SystemManager.CurrentHttpContext.Items["detailItem"];
            var extendedItem = detailItem as ILocatableExtended;

            var dataItemAsLifecycleDataItem = detailItem as ILifecycleDataItem;

            if (extendedItem == null || dataItemAsLifecycleDataItem != null)
                return url + query;

            string currentCultureContentItemUrl = string.Empty;
            var isPublishedLiveItem = dataItemAsLifecycleDataItem.IsPublishedInCulture(culture);
            if (isPublishedLiveItem)
            {
                currentCultureContentItemUrl = extendedItem.ItemDefaultUrl.GetString(culture, false);
            }

            var urlWithParams = url + currentCultureContentItemUrl + query;

            return urlWithParams;
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
        private string templateNamePrefix = "LanguageSelector.";
        private string templateName = "LanguageLinks";

        #endregion
    }
}
