using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.Mvc.Views.Shared.Fields
{
    public partial class DateFieldTemplate
    {
        public DateFieldTemplate(DynamicModuleField fieldData)
        {
            this.fieldData = fieldData;
        }

        private DynamicModuleField fieldData;
    }
}
