namespace SocialShare.Mvc.Models
{
    /// <summary>
    /// SocialButton Model
    /// </summary>
    public class SocialButtonModel
    {
        #region Public members

        /// <summary>
        /// Gets or sets the name of the button.
        /// </summary>
        /// <value>
        /// The name of the button.
        /// </value>
        public string ButtonName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [add text].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add text]; otherwise, <c>false</c>.
        /// </value>
        public bool AddText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [big size].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [big size]; otherwise, <c>false</c>.
        /// </value>
        public bool BigSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [display counters].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [display counters]; otherwise, <c>false</c>.
        /// </value>
        public bool DisplayCounters { get; set; }

        #endregion 

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialButtonModel"/> class.
        /// </summary>
        public SocialButtonModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialButtonModel"/> class.
        /// </summary>
        /// <param name="buttonName">Name of the button.</param>
        /// <param name="addText">if set to <c>true</c> [add text].</param>
        /// <param name="displayCounters">if set to <c>true</c> [display counters].</param>
        /// <param name="bigSize">if set to <c>true</c> [big size].</param>
        public SocialButtonModel(string buttonName, bool addText,  bool displayCounters, bool bigSize)
        {
            this.ButtonName = buttonName;
            this.AddText = addText;
            this.BigSize = bigSize;
            this.DisplayCounters = displayCounters;
        }
    }
}
