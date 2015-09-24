﻿using System;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// FilterDynamicItemWithCategoryTagAndDateOnPage arrangement class.
    /// </summary>
    public class FilterDynamicItemWithCategoryTagAndDateOnPage : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);

            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperations.Taxonomies().CreateCategory(CategoryTitle + "0");

            for (int i = 1; i < 5; i++)
            {
                ServerOperations.Taxonomies().CreateCategory(CategoryTitle + i, CategoryTitle + (i - 1));
                ServerOperations.Taxonomies().CreateTag(TagTitle + i);
                var tag = TaxonomyManager.GetManager().GetTaxa<FlatTaxon>().SingleOrDefault(t => t.Title == TagTitle + i);
                var category = TaxonomyManager.GetManager().GetTaxa<HierarchicalTaxon>().SingleOrDefault(t => t.Title == CategoryTitle + i);              
                ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(ItemsTitle + i, ItemsTitle + i + "Url", tag.Id, category.Id);         
            }

            DateTime publicationDate = DateTime.UtcNow.AddDays(-10);

            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();
            ServerOperationsFeather.DynamicModulePressArticle().PublishPressArticleWithSpecificDate(dynamicCollection[0], publicationDate);

            ServerOperationsFeather.Pages().AddDynamicWidgetToPage(pageId, "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle", "PressArticle", "Press Articles MVC");
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();           
            ServerOperations.Taxonomies().ClearAllCategories(TaxonomiesConstants.CategoriesTaxonomyId);
            ServerOperations.Taxonomies().ClearAllTags(TaxonomiesConstants.TagsTaxonomyId);

            var providerName = string.Empty;
            if (ServerOperations.MultiSite().CheckIsMultisiteMode())
            {
                providerName = "dynamicContentProvider";
            }

            ServerOperationsFeather.DynamicModulePressArticle().DeleteAllDynamicItemsInProvider(providerName);
        }

        private const string PageName = "TestPage";
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";
        private const string ItemsTitle = "Title";
        private const string CategoryTitle = "Category";
        private const string TagTitle = "Tag";
    }
}
