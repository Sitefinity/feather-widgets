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
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicFieldGenerator"/> class.
        /// </summary>
        /// <param name="moduleType">Type of the module.</param>
        public DynamicFieldGenerator(DynamicModuleType moduleType)
        {
            this.moduleType = moduleType;

            FieldGenerators.Add(new LongTextAreaFieldGenerationStrategy());
            //fieldContexts.Add(new DynamicFieldContext("MediaTypeImagesSection"));
            FieldGenerators.Add(new ShortTextFieldGenerationStrategy(moduleType));
            //fieldContexts.Add(new DynamicFieldContext("MultipleChoiceSection"));
            //fieldContexts.Add(new DynamicFieldContext("YesNoSection"));
            FieldGenerators.Add(new DateFieldGenerationStrategy());
            FieldGenerators.Add(new NumberFieldGenerationStrategy());
            FieldGenerators.Add(new PriceFieldGenerationStrategy());
            //fieldContexts.Add(new DynamicFieldContext("LongFieldsTypeTextAreaSection"));
            //fieldContexts.Add(new DynamicFieldContext("MediaVideoSection"));
            //fieldContexts.Add(new DynamicFieldContext("MediaFilesSection"));
            //fieldContexts.Add(new DynamicFieldContext("AddressFieldSection"));
            FieldGenerators.Add(new ClassificationFieldGenerationStrategy());
            //fieldContexts.Add(new DynamicFieldContext("RelatedMediaSection"));
            //fieldContexts.Add(new DynamicFieldContext("RelatedDataSection"));
        }

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
                        fieldsSectionBuilder.Append(DynamicFieldGenerator.emptyLine);
                    }                   
                }
                else
                {
                    fieldsSectionBuilder.Append(DynamicFieldGenerator.emptyLine);
                }
            }

            return fieldsSectionBuilder.ToString();
        }

        /// <summary>
        /// The field generators
        /// </summary>
        public IList<FieldGenerationStrategy> FieldGenerators = new List<FieldGenerationStrategy>();
        private static readonly string emptyLine = "\r\n";

        private DynamicModuleType moduleType;
    }
}
