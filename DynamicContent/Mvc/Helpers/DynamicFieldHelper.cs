using DynamicContent.FieldsGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
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

        public static MvcHtmlString LongTextField(this HtmlHelper helper, string dynamicFieldItem, string cssClass = "")
        {
            helper.ViewBag.CssClass = cssClass;
            return ASP.PartialExtensions.Partial(helper, "LongTextField", dynamicFieldItem);
        }
    }
}
