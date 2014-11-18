using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUtilities.CommonOperations
{
    /// <summary>
    /// Provides access to Dynamic modules common operations.
    /// </summary>
    public class DynamicModulesOperations
    {
        /// <summary>
        /// Imports a dynamic module.
        /// </summary>
        /// <param name="moduleResource">The dynamic module resource file.</param>
        public void ImportModule(string moduleResource)
        {
            var assembly = this.GetTestUtilitiesAssembly();

            Stream moduleStream = assembly.GetManifestResourceStream(moduleResource);
            using (Stream stream = moduleStream)
            {
                ServerOperations.ModuleBuilder().ImportModule(stream);
            }
        }

        /// <summary>
        /// Gets FeatherWidgets.TestUtilities assembly.
        /// </summary>
        /// <returns>TestUtilities assembly.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public Assembly GetTestUtilitiesAssembly()
        {
            var testUtilitiesAssembly = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name.Equals("FeatherWidgets.TestUtilities")).FirstOrDefault();
            if (testUtilitiesAssembly == null)
            {
                throw new DllNotFoundException("Assembly wasn't found");
            }

            return testUtilitiesAssembly;
        }

        /// <summary>
        /// Adds new layout file to a selected resource package.
        /// </summary>
        /// <param name="packageName">The name of the package.</param>
        /// <param name="layoutFileName">The name of the layout file.</param>
        /// <param name="fileResource">The file resource.</param>
        public void AddNewResource(string fileResource, string filePath)
        {
            var assembly = this.GetTestUtilitiesAssembly();
            Stream source = assembly.GetManifestResourceStream(fileResource);

            Stream destination = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            this.CopyStream(source, destination);

            destination.Dispose();
        }

        /// <summary>
        /// Copies file stream to another file stream
        /// </summary>
        /// <param name="input">The input file.</param>
        /// <param name="output">The destination file.</param>
        private void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[32768];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }
    }
}
