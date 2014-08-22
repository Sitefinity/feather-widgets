namespace News.Mvc.Models
{
    /// <summary>
    /// The rendering options for the News widget. 
    /// </summary>
    /// <remarks>
    /// Each option describes different selection of items that will be included while rendering the News widget.
    /// </remarks>
    public enum NewsSelectionMode
    {
        /// <summary>
        /// Refers to all News items.
        /// </summary>
        AllNews, 

        /// <summary>
        /// Refers to custom selection of pages.
        /// </summary>
        SelectedNews,

        /// <summary>
        /// The filtered news.
        /// </summary>
        FilteredNews
    }
}