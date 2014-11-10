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

            FieldContexts.Add(new DynamicFieldContext("LongTextField", new LongTextFieldGenerationStrategy()));
            //fieldContexts.Add(new DynamicFieldContext("MediaTypeImagesSection"));
            FieldContexts.Add(new DynamicFieldContext("ShortTextField", new ShortTextFieldGenerationStrategy(moduleType)));
            //fieldContexts.Add(new DynamicFieldContext("MultipleChoiceSection"));
            //fieldContexts.Add(new DynamicFieldContext("YesNoSection"));
            FieldContexts.Add(new DynamicFieldContext("DateField", new DateFieldGenerationStrategy()));
            FieldContexts.Add(new DynamicFieldContext("NumberField", new NumberFieldGenerationStrategy()));
            FieldContexts.Add(new DynamicFieldContext("PriceField", new PriceFieldGenerationStrategy()));
            //fieldContexts.Add(new DynamicFieldContext("LongFieldsTypeTextAreaSection"));
            //fieldContexts.Add(new DynamicFieldContext("MediaVideoSection"));
            //fieldContexts.Add(new DynamicFieldContext("MediaFilesSection"));
            //fieldContexts.Add(new DynamicFieldContext("AddressFieldSection"));
            //fieldContexts.Add(new DynamicFieldContext("ClassificationSection"));
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

            foreach (var fieldContext in this.FieldContexts)
            {
                var fieldsForType = this.moduleType.Fields.Where(fieldContext.FieldGenerationStrategy.GetFieldCondition);
                if (fieldsForType.Count() != 0)
                {
                    foreach (DynamicModuleField currentField in fieldsForType)
                    {
                        fieldsSectionBuilder.Append(fieldContext.FieldGenerationStrategy.GetFieldMarkup(currentField));
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
        /// The field contexts
        /// </summary>
        public IList<DynamicFieldContext> FieldContexts = new List<DynamicFieldContext>();
        private static readonly string emptyLine = "\r\n";

        private DynamicModuleType moduleType;
    }
}
