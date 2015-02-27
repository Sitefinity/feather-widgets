using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Frontend.Media.Mvc.Models.Image
{
    /// <summary>
    /// This class represent the model of a thumbnail.
    /// </summary>
    [DataContract]
    public class ThumbnailModel
    {
        /// <summary>
        /// Gets or sets the thumbnail profile's name.
        /// </summary>
        /// <value>The name.</value>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the generated URL for this thumbnial profile.
        /// </summary>
        /// <value>The URL.</value>
        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
}
