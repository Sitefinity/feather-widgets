using System.IO;
using Telerik.Sitefinity.Forms.Model;

namespace Telerik.Sitefinity.Frontend.Forms
{
    /// <summary>
    /// This interface provides API for creating form markup.
    /// </summary>
    public interface IFormRenderer
    {
        /// <summary>
        /// Writes the form by given form description.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="form">The form.</param>
        void Render(StreamWriter writer, FormDescription form);
    }
}
