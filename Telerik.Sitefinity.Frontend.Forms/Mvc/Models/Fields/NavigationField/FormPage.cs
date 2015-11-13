namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.NavigationField
{
    /// <summary>
    /// Represents a page in multi-page form
    /// </summary>
    public class FormPage
    {
        /// <summary>
        /// Gets or sets the previous page break field id
        /// </summary>
        public string PreviousPageBreakId { get; set; }

        /// <summary>
        /// Gets or sets the title of the form page
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets index of current page
        /// </summary>
        public int Index { get; set; }
    }
}