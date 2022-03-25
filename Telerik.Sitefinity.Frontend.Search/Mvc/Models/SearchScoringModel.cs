using System;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Search.Mvc.Models
{
    /// <summary>
    /// This class contains the search scoring settings
    /// </summary>
    [Serializable]
    public class SearchScoringModel
    {
        /// <inheritdoc />
        public string ScoringProfile
        {
            get
            {
                return ControlUtilities.Sanitize(this.scoringProfile);
            }

            set
            {
                this.scoringProfile = value;
            }
        }

        /// <inheritdoc />
        public string ScoringParameters
        {
            get
            {
                return ControlUtilities.Sanitize(this.scoringParameters);
            }
            set
            {
                this.scoringParameters = value;
            }
        }

        /// <summary>
        /// Returns base64 hash representation of the search scoring model
        /// </summary>
        /// <returns>The base64 hash of the scoring settings</returns>
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(this.ScoringProfile))
            {
                string scoringSettingsString = this.ScoringProfile;
                if (!string.IsNullOrEmpty(this.ScoringParameters))
                {
                    scoringSettingsString = $"{scoringSettingsString};{this.ScoringParameters}";
                }

                var encodedScoringSettings = SystemExtensions.Base64Encode(scoringSettingsString);
                return encodedScoringSettings;
            }

            return string.Empty;
        }

        private string scoringProfile;
        private string scoringParameters;
    }
}
