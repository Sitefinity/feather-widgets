using DynamicContent.FieldsGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent
{
    public class DynamicFieldContext
    {
        public DynamicFieldContext(string fieldType, FieldGenerationStrategy fieldGenerationStrategy)
        {
            this.FieldType = fieldType;
            this.FieldGenerationStrategy = fieldGenerationStrategy;
        }

        public string FieldType { get; set; }

        public FieldGenerationStrategy FieldGenerationStrategy { get; set; }
    }
}
