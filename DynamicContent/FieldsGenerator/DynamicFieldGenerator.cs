using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.FieldsGenerator
{
    /// <summary>
    /// This class generates the dynamic fields markup.
    /// </summary>
    public class DynamicFieldGenerator
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicFieldGenerator"/> class.
        /// </summary>
        /// <param name="moduleType">Type of the module.</param>
        public DynamicFieldGenerator(DynamicModuleType moduleType)
        {
            this.moduleType = moduleType;

            this.FieldGenerators = new List<FieldGenerationStrategy>();
            this.FieldGenerators.Add(new LongTextAreaFieldGenerationStrategy());
            this.FieldGenerators.Add(new ImagesFieldGenerationStrategy());
            this.FieldGenerators.Add(new ShortTextFieldGenerationStrategy(moduleType));
            this.FieldGenerators.Add(new MultipleChoiceFieldGenerationStrategy());
            this.FieldGenerators.Add(new YesNoFieldGenerationStrategy());
            this.FieldGenerators.Add(new DateFieldGenerationStrategy());
            this.FieldGenerators.Add(new NumberFieldGenerationStrategy());
            this.FieldGenerators.Add(new PriceFieldGenerationStrategy());
            this.FieldGenerators.Add(new LongRichTextFieldGenerationStrategy());
            this.FieldGenerators.Add(new VideosFieldGenerationStrategy());
            this.FieldGenerators.Add(new DocumentsFieldGenerationStrategy());

            // fieldContexts.Add(new DynamicFieldContext("AddressFieldSection"));
            this.FieldGenerators.Add(new ClassificationFieldGenerationStrategy());
            this.FieldGenerators.Add(new RelatedMediaFieldGenerationStrategy());
            this.FieldGenerators.Add(new RelatedDataFieldGenerationStrategy());
        }

        #endregion

        #region Public properties

        /// <summary>
        /// The field generators
        /// </summary>
        public IList<FieldGenerationStrategy> FieldGenerators { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Generates the detail dynamic field section markup.
        /// </summary>
        /// <returns></returns>
        protected internal virtual string GenerateDetailDynamicFieldSection()
        {
            StringBuilder fieldsSectionBuilder = new StringBuilder();

            foreach (var fieldGenerator in this.FieldGenerators)
            {
                var fieldsForType = this.moduleType.Fields.Where(fieldGenerator.GetFieldCondition);
                if (fieldsForType.Count() != 0)
                {
                    foreach (DynamicModuleField currentField in fieldsForType)
                    {
                        fieldsSectionBuilder.Append(fieldGenerator.GetFieldMarkup(currentField));
                        fieldsSectionBuilder.Append(DynamicFieldGenerator.EmptyLine);
                    }
                }
            }

            return fieldsSectionBuilder.ToString();
        }

        #endregion

        #region Private fields and constants

        internal static readonly string EmptyLine = "\r\n";

        private DynamicModuleType moduleType;

        #endregion
    }
}
