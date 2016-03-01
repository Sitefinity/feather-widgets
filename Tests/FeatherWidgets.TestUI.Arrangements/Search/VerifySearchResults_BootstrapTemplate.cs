﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for VerifySearchResults_BootstrapTemplate
    /// </summary>
    public class VerifySearchResults_BootstrapTemplate : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            ServerOperations.News().CreatePublishedNewsItemLiveId(NewsTitle1, NewsContent, NewsAuthor, NewsSource);
            ServerOperations.News().CreatePublishedNewsItemLiveId(NewsTitle2, NewsContent, NewsAuthor, NewsSource);

            Guid searchIndexId = ServerOperations.Search().CreateIndex(SearchIndexName, new[] { SearchContentType.News });
            ServerOperations.Search().Reindex(searchIndexId);

            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            ServerOperations.Pages().CreatePage(SearchPageTitle, templateId);
            Guid newsPageId = ServerOperations.Pages().CreatePage(NewsPageTitle);
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(newsPageId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Search().DeleteAllIndexes();
            ServerOperations.News().DeleteAllNews();
        }

        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string SearchIndexName = "VerifySearchResults_BootstrapTemplate";
        private const string NewsTitle1 = "test news";
        private const string NewsTitle2 = "another news";
        private const string SearchPageTitle = "BootstrapPage";
        private const string PageTemplateName = "Bootstrap.default";
        private const string NewsPageTitle = "NewsPage";
        private const string NewsAuthor = "TestNewsAuthor";
        private const string NewsSource = "TestNewsSource";
        private const string NewsContent = "News content";
    }
}
