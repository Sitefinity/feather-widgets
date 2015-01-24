using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Telerik.Sitefinity.Frontend.DynamicContent")]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("9700a490-2e30-4ca8-a1ef-0fcb9aa54f06")]

[assembly: ControllerContainer(typeof(Telerik.Sitefinity.Frontend.DynamicContent.DynamicWidgetInitializer), "Initialize")]