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
    }
}
