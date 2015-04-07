using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;

namespace FeatherWidgets.TestUtilities.CommonOperations.Pages
{
    public class MediaOperations
    {
        /// <summary>
        /// Uploads the document in folder.
        /// </summary>
        /// <param name="folderId">The folder id.</param>
        /// <param name="documentTitle">The document title.</param>
        /// <param name="documentResource">The document resource.</param>
        /// <param name="documentExtension">The document extension.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public Guid UploadDocumentInFolder(Guid folderId, string documentTitle, string documentResource, string documentExtension = null)
        {
            var manager = LibrariesManager.GetManager();

            var folder = manager.GetFolder(folderId);
            Library library = this.GetLibraryByFolder(manager, folder);

            var document = manager.CreateDocument();
            var title = documentTitle;
            document.Parent = library;
            if (folderId != library.Id)
                document.FolderId = folderId;
            document.Title = title;
            document.UrlName = title.ToLower().Replace(' ', '-');
            document.ApprovalWorkflowState = "Published";
            manager.RecompileItemUrls<Telerik.Sitefinity.Libraries.Model.Document>(document);

            System.Reflection.Assembly thisExe;
            thisExe = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream documentStream = thisExe.GetManifestResourceStream(documentResource);

            manager.Upload(document, documentStream, documentExtension ?? Path.GetExtension(documentResource));

            manager.Lifecycle.Publish(document);

            manager.SaveChanges();

            return document.Id;
        }

        /// <summary>
        /// Get the Library of a given Folder
        /// </summary>
        /// <param name="manager">Library Manager</param>
        /// <param name="folder">The folder</param>
        /// <returns>The Library containing the folder</returns>
        private Library GetLibraryByFolder(LibrariesManager manager, IFolder folder)
        {
            while (!(folder is Library))
            {
                folder = manager.GetFolder(folder.ParentId);
            }

            var folderToBeCast = folder;

            return folderToBeCast as Library;
        }
    }
}
