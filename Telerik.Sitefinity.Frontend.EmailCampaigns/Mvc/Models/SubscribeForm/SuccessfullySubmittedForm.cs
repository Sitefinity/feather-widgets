namespace Telerik.Sitefinity.Frontend.EmailCampaigns.Mvc.Models
{
    /// <summary>
    /// Gets the action that will be executed on successful submission.
    /// </summary>
    public enum SuccessfullySubmittedForm
    {
        /// <summary>
        /// Specify a message that will be displayed after successful submission.
        /// </summary>
        ShowMessage = 0,

        /// <summary>
        /// Specify a page that will be used for redirect afer successful submission.
        /// </summary>
        OpenSpecificPage
    }
}