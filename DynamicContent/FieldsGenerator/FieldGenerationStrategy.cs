using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.FieldsGenerator
{
    public abstract class FieldGenerationStrategy
    {
        public virtual bool GetFieldCondition(DynamicModuleField field)
        {
            var condition = field.FieldStatus != DynamicModuleFieldStatus.Removed && !field.IsHiddenField;

            return condition;
        }

        public abstract string GetFieldMarkup(DynamicModuleField field);
    }
}
