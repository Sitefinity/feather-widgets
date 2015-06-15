using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.StringResources;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models.UnsubscribeForm
{
    /// <summary>
    /// This class represents the model used for Unsubscribe widget.
    /// </summary>
    public class UnsubscribeFormModel : IUnsubscribeFormModel
    {
        #region Properties
        
        /// <inheritDoc/>
        public Guid ListId { get; set; }
        
        /// <inheritDoc/>
        public string ProviderName { get; set; }
        
        /// <inheritDoc/>
        public string WidgetTitle { get; set; }
        
        /// <inheritDoc/>
        public string WidgetDescription { get; set; }
        
        /// <inheritDoc/>
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                if (this.message != value)
                {
                    this.message = value;
                }
            }
        }
        
        /// <inheritDoc/>
        public UnsubscribeMode UnsubscribeMode { get; set; }
        
        /// <inheritDoc/>
        public string LinkCssClass { get; set; }
        
        /// <inheritDoc/>
        public string EmailAddressCssClass { get; set; }
        
        #endregion
        
        public UnsubscribeFormViewModel CreateViewModel()
        {
            var viewModel = new UnsubscribeFormViewModel();
            
            if (this.UnsubscribeMode == UnsubscribeMode.Link)
            {
                viewModel.Message = this.message;
                viewModel.CssClass = this.LinkCssClass;
            }
            
            return viewModel;
        }
        
        #region Private fields and constants

        private string message = Res.Get<UnsubscribeFormResources>().UnsubscribeMessageOnSuccess;

        #endregion
    }
}