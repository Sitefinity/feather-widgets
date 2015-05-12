using System;
using System.Linq;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Media.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestIntegration.DocumentsList
{
    /// <summary>
    /// This is a class with Documents list tests for paging and limit options.
    /// </summary>
    [TestFixture]
    [Description("This is a class with Documents list paging and limit tests.")]
    public class DocumentsListWidgetPagingLimitTests
    {
        /// <summary>
        /// Create library with documents
        /// </summary>
        [SetUp]
        public void Setup()
        {
            ServerOperations.Documents().CreateDocumentLibrary(LibraryTitle);
            ServerSideUpload.UploadDocument(LibraryTitle, DocumentTitle + 1, DocumentResource1);
            ServerSideUpload.UploadDocument(LibraryTitle, DocumentTitle + 2, DocumentResource2);
            ServerSideUpload.UploadDocument(LibraryTitle, DocumentTitle + 3, DocumentResource3);
        }

        /// <summary>
        /// Delete library
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            ServerOperations.Libraries().DeleteAllDocumentLibrariesExceptDefaultOne();
        }

        /// <summary>
        /// Verify Paging (1 item per page) and All documents in Documents list widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.Team7)]
        public void DocumentsList_VerifyPaging()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(DocumentsListController).FullName;
            var documentsListController = new DocumentsListController();
            documentsListController.Model.SelectionMode = SelectionMode.AllItems;
            documentsListController.Model.DisplayMode = ListDisplayMode.Paging;
            documentsListController.Model.SortExpression = "Title ASC";
            documentsListController.Model.ItemsPerPage = 1;
            mvcProxy.Settings = new ControllerSettings(documentsListController);

            var docsPage1 = documentsListController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(docsPage1.Length.Equals(1), "Number of docs is not correct");
            Assert.AreEqual(DocumentTitle + 1, docsPage1[0].Fields.Title.Value, "Wrong title");

            var docsPage2 = documentsListController.Model.CreateListViewModel(null, 2).Items.ToArray();
            Assert.IsTrue(docsPage2.Length.Equals(1), "Number of docs is not correct");
            Assert.AreEqual(DocumentTitle + 2, docsPage2[0].Fields.Title.Value, "Wrong title");

            var docsPage3 = documentsListController.Model.CreateListViewModel(null, 3).Items.ToArray();
            Assert.IsTrue(docsPage3.Length.Equals(1), "Number of docs is not correct");
            Assert.AreEqual(DocumentTitle + 3, docsPage3[0].Fields.Title.Value, "Wrong title");
        }

        /// <summary>
        /// Verify Limit to 1 and All documents in Documents list widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.Team7)]
        public void DocumentsList_VerifyLimit()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(DocumentsListController).FullName;
            var documentsListController = new DocumentsListController();
            documentsListController.Model.SelectionMode = SelectionMode.AllItems;
            documentsListController.Model.DisplayMode = ListDisplayMode.Limit;
            documentsListController.Model.SortExpression = "Title ASC";
            documentsListController.Model.ItemsPerPage = 1;
            mvcProxy.Settings = new ControllerSettings(documentsListController);

            var docs = documentsListController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(docs.Length.Equals(1), "Number of docs is not correct");
            Assert.AreEqual(DocumentTitle + 1, docs[0].Fields.Title.Value, "Wrong title");
        }

        /// <summary>
        /// Verify No limit and All documents in Documents list widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.Team7)]
        public void DocumentsList_VerifyNoLimit()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(DocumentsListController).FullName;
            var documentsListController = new DocumentsListController();
            documentsListController.Model.SelectionMode = SelectionMode.AllItems;
            documentsListController.Model.DisplayMode = ListDisplayMode.All;
            documentsListController.Model.SortExpression = "Title ASC";
            documentsListController.Model.ItemsPerPage = 1;
            mvcProxy.Settings = new ControllerSettings(documentsListController);

            var docs = documentsListController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(docs.Length.Equals(3), "Number of docs is not correct");
            Assert.AreEqual(DocumentTitle + 1, docs[0].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(DocumentTitle + 2, docs[1].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(DocumentTitle + 3, docs[2].Fields.Title.Value, "Wrong title");
        }

        private const string LibraryTitle = "TestDocumentLibrary";
        private const string DocumentTitle = "Document";
        private const string DocumentResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string DocumentResource2 = "Telerik.Sitefinity.TestUtilities.Data.Images.2.jpg";
        private const string DocumentResource3 = "Telerik.Sitefinity.TestUtilities.Data.Images.3.jpg";
    }
}
