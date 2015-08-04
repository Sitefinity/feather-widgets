namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models
{
    /// <summary>
    /// Determines if the form is in Read or Write mode.
    /// </summary>
    public enum FormViewMode
    {
        /// <summary>
        /// The form can be edited and filled.
        /// </summary>
        Write = 0,

        /// <summary>
        /// The form is only viewed and values can not be changed.
        /// </summary>
        Read = 1
    }
}
