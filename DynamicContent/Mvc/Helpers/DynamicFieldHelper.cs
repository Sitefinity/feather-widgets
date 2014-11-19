using System.Linq;
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

        public static MvcHtmlString MainPictureSection(DynamicModuleType moduleType)
        {
            var firstMediaFieldTypeImage = moduleType.Fields.FirstOrDefault(f => f.FieldType == FieldType.Media
                                                                                && f.FieldStatus != DynamicModuleFieldStatus.Removed
                                                                                && f.MediaType == "image");
            var fieldMarkup = DynamicFieldGenerator.EmptyLine;

            if (firstMediaFieldTypeImage != null)
            {
                fieldMarkup = DynamicFieldHelper.GetFieldMarkup(firstMediaFieldTypeImage);
            }

            return new MvcHtmlString(fieldMarkup);
        }

        private static string GetFieldMarkup(DynamicModuleField field)
        {
            var markup = string.Empty;
            if (field.AllowMultipleImages)
            {
                markup = string.Format(DynamicFieldHelper.MultiImageFieldMarkupTempalte, field.Name);
            }
            else
            {
                markup = string.Format(DynamicFieldHelper.SingleImageFieldMarkupTempalte, field.Name);
            }

            return markup;
        }

        private const string SingleImageFieldMarkupTempalte = @"@Html.Sitefinity().ImageField(((IEnumerable<ContentLink>)item.{0}).FirstOrDefault(), ""{0}"")";
        private const string MultiImageFieldMarkupTempalte = @"@Html.Sitefinity().ImageField((IEnumerable<ContentLink>)item.{0}, ""{0}"")";

    }
}
