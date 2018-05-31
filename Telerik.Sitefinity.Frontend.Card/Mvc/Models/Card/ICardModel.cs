using System;

namespace Telerik.Sitefinity.Frontend.Card.Mvc.Models.Card
{
    /// <summary>
    /// This interface defines API for working with <see cref="Telerik.Sitefinity.Card.Model.Blog"/> items.
    /// </summary>
    public interface ICardModel
    {
        /// <summary>
        /// Gets or sets the image identifier.
        /// </summary>
        Guid ImageId { get; set; }

        /// <summary>
        /// Gets or sets the name of the image provider.
        /// </summary>
        string ImageProviderName { get; set; }

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the HTML.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets whether the widget is in page select mode.
        /// </summary>
        /// <value>
        /// Whether the widget is in page select mode.
        /// </value>
        bool IsPageSelectMode { get; set; }

        /// <summary>
        /// Gets or sets the page identifier to use as link.
        /// </summary>
        Guid LinkedPageId { get; set; }

        /// <summary>
        /// Gets or sets the page url to use as link.
        /// </summary>
        string LinkedUrl { get; set; }

        /// <summary>
        /// Gets or sets the action name.
        /// </summary>
        string ActionName { get; set; }

        /// <summary>
        /// Gets or sets the heading.
        /// </summary>
        string Heading { get; set; }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <returns></returns>
        CardViewModel GetViewModel();

        /// <summary>
        /// Checks if the model is empty.
        /// </summary>
        /// <returns></returns>
        bool IsEmpty();
    }
}
