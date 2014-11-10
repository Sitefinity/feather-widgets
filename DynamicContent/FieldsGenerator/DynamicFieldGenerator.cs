using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.FieldsGenerator
{
    public class DynamicFieldGenerator
    {
        public DynamicFieldGenerator(DynamicModuleType moduleType)
        {
            this.moduleType = moduleType;

            fieldContexts.Add(new DynamicFieldContext("LongTextField", new LongTextFieldGenerationStrategy()));
            //fieldContexts.Add(new DynamicFieldContext("MediaTypeImagesSection"));
            fieldContexts.Add(new DynamicFieldContext("ShortTextField", new ShortTextFieldGenerationStrategy(moduleType)));
            //fieldContexts.Add(new DynamicFieldContext("MultipleChoiceSection"));
            //fieldContexts.Add(new DynamicFieldContext("YesNoSection"));
            fieldContexts.Add(new DynamicFieldContext("DateField", new DateFieldGenerationStrategy()));
            fieldContexts.Add(new DynamicFieldContext("NumberField", new NumberFieldGenerationStrategy()));
            fieldContexts.Add(new DynamicFieldContext("PriceField", new PriceFieldGenerationStrategy()));
            //fieldContexts.Add(new DynamicFieldContext("LongFieldsTypeTextAreaSection"));
            //fieldContexts.Add(new DynamicFieldContext("MediaVideoSection"));
            //fieldContexts.Add(new DynamicFieldContext("MediaFilesSection"));
            //fieldContexts.Add(new DynamicFieldContext("AddressFieldSection"));
            //fieldContexts.Add(new DynamicFieldContext("ClassificationSection"));
            //fieldContexts.Add(new DynamicFieldContext("RelatedMediaSection"));
            //fieldContexts.Add(new DynamicFieldContext("RelatedDataSection"));
        }

        protected internal virtual string GenerateDetailDynamicFieldSection()
        {
            StringBuilder fieldsSectionBuilder = new StringBuilder();

            foreach (var fieldContext in this.fieldContexts)
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

        public IList<DynamicFieldContext> fieldContexts = new List<DynamicFieldContext>();
        private static readonly string emptyLine = "\r\n";

        private DynamicModuleType moduleType;
    }
}
