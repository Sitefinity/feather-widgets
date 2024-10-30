using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using ServiceStack.Text;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.StringResources;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.NavigationControls.Cache;

namespace Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of Navigation widget.
    /// </summary>
    [ControllerToolboxItem(
        Name = NavigationController.WidgetName,
        Title = nameof(NavigationResources.NavigationControlTitle),
        Description = nameof(NavigationResources.NavigationControlDescription),
        ResourceClassId = nameof(NavigationResources),
        SectionName = ToolboxesConfig.NavigationControlsSectionName,
        CssClass = NavigationController.WidgetIconCssClass)]
    [Localization(typeof(NavigationResources))]
    [IndexRenderMode(IndexRenderModes.NoOutput)]
    public class NavigationController : Controller, IPersonalizable
    {
        #region Properties

        /// <summary>
        /// Gets the Navigation widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual INavigationModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = this.InitializeModel();

                return this.model;
            }
        }

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
        /// Gets or sets the page links to display selection mode.
        /// </summary>
        /// <value>The page display mode.</value>
        public PageSelectionMode SelectionMode
        {
            get
            {
                return this.selectionMode;
            }

            set
            {
                this.selectionMode = value;
                this.Model.SelectionMode = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show parent page].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show parent page]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowParentPage
        {
            get
            {
                return this.showParentPage;
            }

            set
            {
                this.showParentPage = value;
                this.Model.ShowParentPage = value;
            }
        }

        /// <summary>
        /// Gets or sets the levels to include.
        /// </summary>
        public virtual int? LevelsToInclude
        {
            get
            {
                return this.levelsToInclude;
            }

            set
            {
                this.levelsToInclude = value;
                this.Model.LevelsToInclude = value;
            }
        }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the NavigationWidget (if such is presented).
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string CssClass
        {
            get
            {
                return this.cssClass;
            }

            set
            {
                this.cssClass = value;
                this.Model.CssClass = value;
            }
        }

        /// <summary>
        /// Gets or sets the identifier of the page that is selected if SelectionMode is SelectedPageChildren.
        /// </summary>
        /// <value>The identifier of the page that is selected if SelectionMode is SelectedPageChildren.</value>
        public Guid SelectedPageId
        {
            get
            {
                return this.selectedPageId;
            }

            set
            {
                this.selectedPageId = value;
                this.Model.SelectedPageId = value;
            }
        }

        /// <summary>
        /// Gets or sets a serialized array of the selected pages.
        /// </summary>
        /// <value>
        /// The a serialized array of selected pages.
        /// </value>
        public string SerializedSelectedPages
        {
            get
            {
                return this.serializedSelectedPages;
            }

            set
            {
                this.serializedSelectedPages = value;
                var selectedPages = JsonSerializer.DeserializeFromString<SelectedPageModel[]>(value);
                this.Model.SelectedPages = selectedPages;
            }
        }

        /// <summary>
        /// Gets or sets the serialized external pages.
        /// </summary>
        /// <value>
        /// The serialized external pages.
        /// </value>
        public string SerializedExternalPages { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether should open external page in new tab.
        /// </summary>
        /// <value>
        /// <c>true</c> if should open external page in new tab; otherwise, <c>false</c>.
        /// </value>
        public bool OpenExternalPageInNewTab
        {
            get
            {
                return this.openExternalPageInNewTab;
            }

            set
            {
                this.openExternalPageInNewTab = value;
                this.Model.OpenExternalPageInNewTab = value;
            }
        }

        /// <summary>
        /// Provides UI for setting navigation output cache variations
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public NavigationOutputCacheVariationSettings OutputCache
        {
            get
            {
                if (this.outputCacheSettings == null)
                {
                    this.outputCacheSettings = new NavigationOutputCacheVariationSettings();
                }

                return this.outputCacheSettings;
            }

            set
            {
                this.outputCacheSettings = value;
            }
        }

        #endregion

        #region Actions

        /// <summary>
        /// Renders appropriate view depending on the <see cref="TemplateName"/>
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            if (this.OutputCache.VaryByAuthenticationStatus || this.OutputCache.VaryByUserRoles)
            {
                PageRouteHandler.RegisterCustomOutputCacheVariation(new NavigationOutputCacheVariation(this.OutputCache));
            }

            var fullTemplateName = this.templateNamePrefix + this.TemplateName;
            this.Model.InitializeNavigationWidgetSettings();
            if (SystemManager.CurrentHttpContext != null)
            {
                if (this.Model is IHasCacheDependency cacheDependentNavigationModel)
                {
                    this.AddCacheDependencies(cacheDependentNavigationModel.GetCacheDependencyObjects());
                }
            }

            return this.View(fullTemplateName, this.Model);
        }

        /// <summary>
        /// Gets the view with provided name.
        /// </summary>
        /// <param name="viewName">
        /// Name of the view.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [ChildActionOnly]
        public ActionResult GetView(string viewName, INavigationModel model)
        {
            return this.PartialView(viewName, model);
        }

        #endregion

        #region Overridden methods

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="INavigationModel"/>.
        /// </returns>
        private INavigationModel InitializeModel()
        {
            var selectedPages = JsonSerializer.DeserializeFromString<SelectedPageModel[]>(this.SerializedSelectedPages);
            var constructorParameters = new Dictionary<string, object>
                         {
                            { "selectionMode", this.SelectionMode },
                            { "selectedPageId", this.SelectedPageId },
                            { "selectedPages", selectedPages },
                            { "levelsToInclude", this.LevelsToInclude },
                            { "showParentPage", this.ShowParentPage },
                            { "cssClass", this.CssClass },
                            { "openExternalPageInNewTab", this.OpenExternalPageInNewTab }
                         };

            return ControllerModelFactory.GetModel<INavigationModel>(this.GetType(), constructorParameters);
        }

        #endregion

        #region Private fields and constants

        internal const string WidgetIconCssClass = "sfNavigationIcn sfMvcIcn";
        private INavigationModel model;
        private int? levelsToInclude = 1;
        private PageSelectionMode selectionMode;
        private string serializedSelectedPages;
        private Guid selectedPageId;
        private string cssClass;
        private bool openExternalPageInNewTab;
        private bool showParentPage;
        private string templateName = "Horizontal";
        private string templateNamePrefix = "NavigationView.";
        private NavigationOutputCacheVariationSettings outputCacheSettings;
        private const string WidgetName = "Navigation_MVC";
        #endregion
    }
}
