using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Recommendations;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.AttributeConfigurator.Attributes;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Recommendations.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of Recommendations widget.
    /// </summary>
    [ControllerToolboxItem(Name = "Recommendations", Title = "Content recommendations", SectionName = "Marketing", CssClass = WidgetCssClassIcon)]
    public class RecommendationsController : Controller, ICustomWidgetVisualizationExtended
    {
        /// <summary>
        /// Gets or sets the conversion.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Recommendation settings", 1)]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Placeholder("Select")]
        [Description("Recommendations aim to improve visitor conversions. Select a conversion defined in Sitefinity Insight that you want to improve. Sitefinity Insight AI will generate different content recommendations for each visitor that will likely influence that visitor to complete the conversion.")]
        [DisplayName("Conversion to be improved")]
        [Choice(ServiceUrl = "/Default.GetConversions()", ServiceWarningMessage = "No conversions are found in Sitefinity Insight.")]
        public int Conversion { get; set; }

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Display settings", 1)]
        [ViewSelector(RegexFilter = "^(?!Sample).*(?<!Sample)$")]
        [DisplayName("Template")]
        [DefaultValue(DefaultViewName)]
        public string SfViewName { get; set; }

        /// <summary>
        /// Gets or sets the header that will be displayed above the recommendations e.g. Read more
        /// </summary>
        [Progress.Sitefinity.Renderer.Designers.Attributes.ContentSection("Display settings", 2)]
        [DefaultValue(DefaultHeader)]
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the button.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [DisplayName("CSS class")]
        public string CssClass { get; set; }

        [Browsable(false)]
        public int MaxNumberOfItems { get; set; } = 1;

        /// <summary>
        /// Gets the empty link text.
        /// </summary>
        /// <value>
        /// The empty link text.
        /// </value>
        [Browsable(false)]
        public string EmptyLinkText
        {
            get
            {
                return "Configure recommendations";
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
                var recommendationsFeatureState = this.GetRecommendationsFeatureState();

                return recommendationsFeatureState.HasInsight && !recommendationsFeatureState.LostConnectionToInsight && recommendationsFeatureState.IsContentRecommendationsFeatureEnabled && recommendationsFeatureState.HasValidConnectionForCurrentSite && this.Conversion <= 0;
            }
        }

        /// <summary>
        /// Gets the widget CSS class that will be used in the page toolbox.
        /// </summary>
        /// <value>
        /// The widget CSS class that will be used in the page toolbox.
        /// </value>
        [Browsable(false)]
        public string WidgetCssClass
        {
            get
            {
                return WidgetCssClassIcon;
            }
        }

        /// <summary>
        /// Renders appropriate list view depending on the <see cref="SfViewName" />
        /// </summary>
        /// <returns>The <see cref="ActionResult" />.</returns>
        public ActionResult Index()
        {
            this.ViewBag.Header = this.Header ?? DefaultHeader;
            this.ViewBag.MaxNumberOfItems = this.MaxNumberOfItems;
            this.ViewBag.UniqueId = Guid.NewGuid();
            this.ViewBag.CssClass = this.CssClass;
            this.ViewBag.SiteId = SystemManager.CurrentContext.CurrentSite.Id;

            var recommendationsFeatureState = this.GetRecommendationsFeatureState();

            if (SystemManager.IsDesignMode)
            {
                return this.GetViewForDesignMode(recommendationsFeatureState);
            }

            if (!recommendationsFeatureState.HasValidConnectionForCurrentSite || !recommendationsFeatureState.IsContentRecommendationsFeatureEnabled || !recommendationsFeatureState.ConversionExists || recommendationsFeatureState.LostConnectionToInsight || this.IsEmpty)
            {
                return new EmptyResult();
            }

            this.ViewBag.ConversionId = this.Conversion;
            this.ViewBag.BaseUrl = UrlPath.GetDomainUrl();

            return this.View(this.SfViewName ?? DefaultViewName);
        }

        /// <summary>
        /// Called when a request matches this controller, but no method with the specified action name is found in the controller.
        /// </summary>
        /// <param name="actionName">The name of the attempted action.</param>
        protected override void HandleUnknownAction(string actionName)
        {
            this.ActionInvoker.InvokeAction(this.ControllerContext, "Index");
        }

        private RecommendationsFeatureState GetRecommendationsFeatureState()
        {
            var recommendationsFeatureState = new RecommendationsFeatureState();
            if (ObjectFactory.Container.IsRegistered<IRecommendationProvider>())
            {
                var recommendationProvider = ObjectFactory.Container.Resolve<IRecommendationProvider>();
                recommendationsFeatureState = recommendationProvider.GetRecommendationsFeatureState(this.Conversion);
                recommendationsFeatureState.HasInsight = true;
            }

            return recommendationsFeatureState;
        }

        private ActionResult GetViewForDesignMode(RecommendationsFeatureState recommendationsFeatureState)
        {
            // No connected Insight or no sites set for tracking
            if (!recommendationsFeatureState.HasValidConnectionForCurrentSite)
            {
                return new ContentResult()
                {
                    Content = "This widget works only if there is an active connection to Sitefinity Insight. Configure a connection in Administration > Sitefinity Insight."
                };
            }

            // Lost connection to Insight
            if (recommendationsFeatureState.LostConnectionToInsight)
            {
                return new ContentResult()
                {
                    Content = "No recommendations can be displayed because the connection to Sitefinity Insight has been lost. Try to restore the connection."
                };
            }

            // Connected Insight but no Premium subscription
            if (!recommendationsFeatureState.IsContentRecommendationsFeatureEnabled)
            {
                return new ContentResult()
                {
                    Content = "Content recommendations are available only to Sitefinity Insight Premium subscriptions."
                };
            }

            // Empty (no convesion selected)
            if (this.IsEmpty)
            {
                return new EmptyResult();
            }

            // Deleted conversion
            if (!recommendationsFeatureState.ConversionExists)
            {
                return new ContentResult()
                {
                    Content = "The selected conversion is not found. No recommendations can be displayed."
                };
            }

            ViewBag.IsPreviewMode = SystemManager.IsPreviewMode;
            return this.View(DefaultDesignViewName);
        }

        private const string WidgetCssClassIcon = "sfListitemsIcn sfMvcIcn";

        private const string DefaultHeader = "Read more";
        private const string DefaultViewName = "Recommendations";
        private const string DefaultDesignViewName = "Sample.Index";
        private const string IncludeUnsafeInlineEvalContextItem = "sf-csp-include-unsafe-inline-eval";
    }
}
