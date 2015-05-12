using System.Linq;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Media.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestIntegration.DocumentsList
{
    /// <summary>
    /// This is a class with Documents list tests for Sorting options.
    /// </summary>
    [TestFixture]
    [Description("This is a class with Documents list sorting tests.")]
    public class DocumentsListWidgetSortingTests
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
        /// Verify Last published sorting and All documents in Documents list widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.Team7)]
        public void DocumentsList_VerifySortingLastPublished()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(DocumentsListController).FullName;
            var documentsListController = new DocumentsListController();
            documentsListController.Model.SelectionMode = SelectionMode.AllItems;
            documentsListController.Model.SortExpression = "PublicationDate DESC";
            mvcProxy.Settings = new ControllerSettings(documentsListController);

            var docs = documentsListController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(docs.Length.Equals(3), "Number of docs is not correct");

            //// expected: Document3, Document2, Document1
            Assert.AreEqual(DocumentTitle + 3, docs[0].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(DocumentTitle + 2, docs[1].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(DocumentTitle + 1, docs[2].Fields.Title.Value, "Wrong title");
        }

        /// <summary>
        /// Verify Title A-Z sorting and All documents in Documents list widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.Team7)]
        public void DocumentsList_VerifySortingTitleAZ()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(DocumentsListController).FullName;
            var documentsListController = new DocumentsListController();
            documentsListController.Model.SelectionMode = SelectionMode.AllItems;
            documentsListController.Model.SortExpression = "Title ASC";
            mvcProxy.Settings = new ControllerSettings(documentsListController);

            var docs = documentsListController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(docs.Length.Equals(3), "Number of docs is not correct");

            //// expected: Document1, Document2, Document3
            Assert.AreEqual(DocumentTitle + 1, docs[0].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(DocumentTitle + 2, docs[1].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(DocumentTitle + 3, docs[2].Fields.Title.Value, "Wrong title");
        }

        /// <summary>
        /// Verify Title Z-A sorting and All documents in Documents list widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.Team7)]
        public void DocumentsList_VerifySortingTitleZA()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(DocumentsListController).FullName;
            var documentsListController = new DocumentsListController();
            documentsListController.Model.SelectionMode = SelectionMode.AllItems;
            documentsListController.Model.SortExpression = "FirstName DESC";
            mvcProxy.Settings = new ControllerSettings(documentsListController);

            var docs = documentsListController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(docs.Length.Equals(3), "Number of docs is not correct");

            //// expected: Document3, Document2, Document1
            Assert.AreEqual(DocumentTitle + 3, docs[0].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(DocumentTitle + 2, docs[1].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(DocumentTitle + 1, docs[2].Fields.Title.Value, "Wrong title");
        }

        /// <summary>
        /// Verify Last modified sorting and All documents in Documents list widget
        /// </summary>
        [Test]
        [Category(TestCategories.Media)]
        [Author(FeatherTeams.Team7)]
        public void DocumentsList_VerifySortingLastModified()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(DocumentsListController).FullName;
            var documentsListController = new DocumentsListController();
            documentsListController.Model.SelectionMode = SelectionMode.AllItems;
            documentsListController.Model.SortExpression = "LastModified DESC";
            mvcProxy.Settings = new ControllerSettings(documentsListController);

            this.ChangeModifiedDateOfDocument2();

            var docs = documentsListController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.IsTrue(docs.Length.Equals(3), "Number of docs is not correct");

            //// expected: Document21, Document3, Document1
            Assert.AreEqual(DocumentTitle + 2 + 1, docs[0].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(DocumentTitle + 3, docs[1].Fields.Title.Value, "Wrong title");
            Assert.AreEqual(DocumentTitle + 1, docs[2].Fields.Title.Value, "Wrong title");
        }
 
        private void ChangeModifiedDateOfDocument2()
        {
            var librariesManager = LibrariesManager.GetManager();
            Document modified = librariesManager.GetDocuments().Where<Document>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == DocumentTitle + 2).FirstOrDefault();
            Document temp = librariesManager.Lifecycle.CheckOut(modified) as Document;

            temp.Title = DocumentTitle + 2 + 1;

            modified = librariesManager.Lifecycle.CheckIn(temp) as Document;

            var document = librariesManager.GetDocument(modified.Id);
            librariesManager.Lifecycle.Publish(document);
            librariesManager.SaveChanges();
        }

        private const string LibraryTitle = "TestDocumentLibrary";
        private const string DocumentTitle = "Document";
        private const string DocumentResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string DocumentResource2 = "Telerik.Sitefinity.TestUtilities.Data.Images.2.jpg";
        private const string DocumentResource3 = "Telerik.Sitefinity.TestUtilities.Data.Images.3.jpg";
    }
}