using System.Web.Mvc;
using DynamicContent.FieldsGenerator;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace DynamicContent.Mvc.Helpers
{
    public static class DynamicFieldHelper
    {
        public static MvcHtmlString GenerateFieldsSection(DynamicModuleType moduleType)
        {
            var generator = new DynamicFieldGenerator(moduleType);
            var dynamicfieldHelper = generator.GenerateDetailDynamicFieldSection();

            return new MvcHtmlString(dynamicfieldHelper);
        }      
    }
}
