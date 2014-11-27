using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicContent.TemplateGeneration.Fields.Helpers;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace DynamicContent.TemplateGeneration.Fields.Implementation
{
    /// <summary>
    /// This class represents field generation strategy for classification dynamic fields.
    /// </summary>
    public class ClassificationField : Field
    {
        /// <inheritdoc/>
        public override bool GetCondition(DynamicModuleField field)
        {
            var condition = base.GetCondition(field)
                && field.FieldType == FieldType.Classification;

            return condition;
        }

        /// <inheritdoc/>
        protected override string GetTemplatePath(DynamicModuleField field)
        {
            var taxonomyType = FieldExtensions.GetTaxonomyType(field.ClassificationId);

            if (taxonomyType == Telerik.Sitefinity.Taxonomies.Model.TaxonomyType.Flat)
                return ClassificationField.FlatTaxonomyTemplatePath;
            else if (taxonomyType == Telerik.Sitefinity.Taxonomies.Model.TaxonomyType.Hierarchical)
                return ClassificationField.HierarchicalTaxonomyTemplatePath;

            return null;
        }

        private const string FlatTaxonomyTemplatePath = "~/Frontend-Assembly/DynamicContent/TemplateGeneration/Fields/Templates/FlatTaxonomyField.cshtml";
        private const string HierarchicalTaxonomyTemplatePath = "~/Frontend-Assembly/DynamicContent/TemplateGeneration/Fields/Templates/HierarchicalTaxonomyField.cshtml";
    }
}
