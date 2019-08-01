using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Helpers
{
    internal static class FormsHelpers
    {
        public static Dictionary<string, string> GetFieldNames(IList<FormControl> formControls)
        {
            var fieldNames = new Dictionary<string, string>();

            foreach (var formControl in formControls)
            {
                var model = GetControlPropertyModel(formControl);
                if (model != null && model.ChildProperties != null)
                {
                    var metaField = model.ChildProperties.FirstOrDefault(p => p.Name == MetaFieldPropertyName);
                    if (metaField != null && metaField.ChildProperties != null)
                    {
                        var id = formControl.Properties.FirstOrDefault(p => p.Name == IdPropertyName);
                        var fieldName = metaField.ChildProperties.FirstOrDefault(p => p.Name == FieldNamePropertyName);

                        if (id != null && fieldName != null)
                        {
                            fieldNames.Add(id.Value, fieldName.Value);
                        }
                    }
                }
            }

            return fieldNames;
        }

        public static List<string> GetHiddenFields(IList<FormControl> formControls)
        {
            var hiddenFields = new List<string>();

            foreach (var formControl in formControls)
            {
                var model = GetControlPropertyModel(formControl);
                if (model != null && model.ChildProperties != null)
                {
                    var hiddenProperty = model.ChildProperties.FirstOrDefault(p => p.Name == HiddenPropertyName);
                    if (hiddenProperty != null && bool.TryParse(hiddenProperty.Value, out bool hidden) && hidden)
                    {
                        var id = formControl.Properties.FirstOrDefault(p => p.Name == IdPropertyName);
                        if (id != null)
                        {
                            hiddenFields.Add(id.Value);
                        }
                    }
                }
            }

            return hiddenFields;
        }

        private static ControlProperty GetControlPropertyModel(FormControl formControl)
        {
            ControlProperty property = null;

            if (formControl.Properties != null)
            {
                var settings = formControl.Properties.FirstOrDefault(p => p.Name == SettingsPropertyName);
                if (settings != null && settings.ChildProperties != null)
                {
                    var model = settings.ChildProperties.FirstOrDefault(p => p.Name == ModelPropertyName);
                    if (model != null)
                    {
                        property = model;
                    }
                }
            }

            return property;
        }

        private const string IdPropertyName = "ID";
        private const string SettingsPropertyName = "Settings";
        private const string ModelPropertyName = "Model";
        private const string MetaFieldPropertyName = "MetaField";
        private const string FieldNamePropertyName = "FieldName";
        private const string HiddenPropertyName = "Hidden";
    }
}