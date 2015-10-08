using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Frontend.Card.Mvc.StringResources
{
    /// <summary>
    /// Localizable strings for the Blog widget
    /// </summary>
    [ObjectInfo(typeof(CardResources), ResourceClassId = "CardResources", Title = "CardResourcesTitle", Description = "CardResourcesDescription")]
    public class CardResources : Resource
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BlogListResources"/> class. 
        /// Initializes new instance of <see cref="BlogListResources"/> class with the default <see cref="ResourceDataProvider"/>.
        /// </summary>
        public CardResources()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogListResources"/> class.
        /// </summary>
        /// <param name="dataProvider">The data provider.</param>
        public CardResources(ResourceDataProvider dataProvider)
            : base(dataProvider)
        {
        }
        #endregion

        #region Meta resources
        /// <summary>
        /// Title for the Card widget resources class.
        /// </summary>
        [ResourceEntry("CardResourcesTitle",
            Value = "Card widget resources",
            Description = "Title for the Card widget resources class.",
            LastModified = "2015/09/22")]
        public string CardResourcesTitle
        {
            get
            {
                return this["CardResourcesTitle"];
            }
        }

        /// <summary>
        /// Description for the Card widget resources class
        /// </summary>
        [ResourceEntry("CardResourcesDescription",
            Value = "Localizable strings for the Card widget.",
            Description = "Description for the Card widget resources class",
            LastModified = "2015/09/22")]
        public string CardResourcesDescription
        {
            get
            {
                return this["CardResourcesDescription"];
            }
        }
        #endregion

        /// <summary>
        /// This phrase is displayed when image was unpublished or has been deleted.
        /// </summary>
        /// <value>An image was not selected or has been deleted. Please select another one.</value>
        [ResourceEntry("ImageWasNotSelectedOrHasBeenDeleted",
            Value = "An image was not selected or has been deleted. Please select another one.",
            Description = "This phrase is displayed when image was unpublished or has been deleted.",
            LastModified = "2015/09/22")]
        public string ImageWasNotSelectedOrHasBeenDeleted
        {
            get
            {
                return this["ImageWasNotSelectedOrHasBeenDeleted"];
            }
        }

        /// <summary>
        /// phrase : More options
        /// </summary>
        /// <value>More options</value>
        [ResourceEntry("MoreOptions",
            Value = "More options",
            Description = "phrase : More options",
            LastModified = "2015/09/22")]
        public string MoreOptions
        {
            get
            {
                return this["MoreOptions"];
            }
        }

        /// <summary>
        /// word: Template
        /// </summary>
        /// <value>Template</value>
        [ResourceEntry("Template",
            Value = "Template",
            Description = " word: Template",
            LastModified = "2015/09/22")]
        public string Template
        {
            get
            {
                return this["Template"];
            }
        }

        /// <summary>
        /// phrase : CSS classes
        /// </summary>
        /// <value>CSS classes</value>
        [ResourceEntry("CssClasses",
            Value = "CSS classes",
            Description = "phrase : CSS classes",
            LastModified = "2015/09/22")]
        public string CssClasses
        {
            get
            {
                return this["CssClasses"];
            }
        }

        /// <summary>
        /// word: Style
        /// </summary>
        /// <value>Style</value>
        [ResourceEntry("Style",
            Value = "Style",
            Description = "word: Style",
            LastModified = "2015/09/22")]
        public string Style
        {
            get
            {
                return this["Style"];
            }
        }

        /// <summary>
        /// phrase: Create content
        /// </summary>
        /// <value>Create content</value>
        [ResourceEntry("CreateContent",
            Value = "Create content",
            Description = "phrase: Create content",
            LastModified = "2015/10/06")]
        public string CreateContent
        {
            get
            {
                return this["CreateContent"];
            }
        }

        /// <summary>
        /// word: Heading
        /// </summary>
        /// <value>Heading</value>
        [ResourceEntry("Heading",
            Value = "Heading",
            Description = "word: Heading",
            LastModified = "2015/10/06")]
        public string Heading
        {
            get
            {
                return this["Heading"];
            }
        }

        /// <summary>
        /// word: Description
        /// </summary>
        /// <value>Description</value>
        [ResourceEntry("Description",
            Value = "Description",
            Description = "word: Description",
            LastModified = "2015/10/06")]
        public string Description
        {
            get
            {
                return this["Description"];
            }
        }

        /// <summary>
        /// word: Text
        /// </summary>
        /// <value>Text</value>
        [ResourceEntry("Text",
            Value = "Text",
            Description = "word: Text",
            LastModified = "2015/10/07")]
        public string Text
        {
            get
            {
                return this["Text"];
            }
        }

        /// <summary>
        /// word: Image
        /// </summary>
        /// <value>Image</value>
        [ResourceEntry("Image",
            Value = "Image",
            Description = "word: Image",
            LastModified = "2015/10/06")]
        public string Image
        {
            get
            {
                return this["Image"];
            }
        }

        /// <summary>
        /// phrase: Example: Learn more
        /// </summary>
        /// <value>Example: Learn more</value>
        [ResourceEntry("LearnMore",
            Value = "Example: Learn more",
            Description = "phrase: Example: Learn more",
            LastModified = "2015/10/06")]
        public string LearnMore
        {
            get
            {
                return this["LearnMore"];
            }
        }

        /// <summary>
        /// phrase: Page within the site...
        /// </summary>
        /// <value>Page within the site...</value>
        [ResourceEntry("PageWithinLabel",
            Value = "Page within the site...",
            Description = "phrase: Page within the site...",
            LastModified = "2015/10/06")]
        public string PageWithinLabel
        {
            get
            {
                return this["PageWithinLabel"];
            }
        }

        /// <summary>
        /// phrase: External URL...
        /// </summary>
        /// <value>External URL...</value>
        [ResourceEntry("ExternalURL",
            Value = "External URL...",
            Description = "phrase: External URL...",
            LastModified = "2015/10/06")]
        public string ExternalURL
        {
            get
            {
                return this["ExternalURL"];
            }
        }

        /// <summary>
        /// phrase: Example: http://sitefinity.com
        /// </summary>
        /// <value>Example: http://sitefinity.com</value>
        [ResourceEntry("Example",
            Value = "Example: http://sitefinity.com",
            Description = "phrase: Example: http://sitefinity.com",
            LastModified = "2015/10/06")]
        public string Example
        {
            get
            {
                return this["Example"];
            }
        }
    }
}
