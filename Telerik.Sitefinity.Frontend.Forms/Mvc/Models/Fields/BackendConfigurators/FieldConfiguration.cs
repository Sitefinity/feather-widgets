using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.BackendConfigurators
{
    internal class FieldConfiguration
    {
        public FieldConfiguration(Type backendFieldType, IFieldConfigurator fieldConfigurator)
        {
            this.BackendFieldType = backendFieldType;
            this.FieldConfigurator = fieldConfigurator;
        }

        public Type BackendFieldType { get; set; }
        public IFieldConfigurator FieldConfigurator { get; set; }
    }
}
