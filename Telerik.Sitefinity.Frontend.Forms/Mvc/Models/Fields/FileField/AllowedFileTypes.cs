using System;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.FileField
{
    /// <summary>
    /// Enumeration of different validation options for file types.
    /// </summary>
    [Flags]
    public enum AllowedFileTypes
    {
        None = 0,
        Images = 1,
        Documents = 1 << 1,
        Audio = 1 << 2,
        Video = 1 << 3,
        Other = 1 << 4,
        All = 1 << 5
    }
}
