using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Frontend.Forms;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Telerik.Sitefinity.Frontend.Forms")]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("65045d37-e495-4e45-8794-1d5f2cf344ca")]

[assembly: ControllerContainer(typeof(Initializer), "Initialize")]

[assembly: InternalsVisibleTo("FeatherWidgets.TestUnit")]