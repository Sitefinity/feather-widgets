using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Web;
using Telerik.Sitefinity.Frontend.AgenticRAG;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Telerik.Sitefinity.Frontend.AgenticRAG")]

// Setting ComVisible to false makes the types in this assembly not visible to COM
// components.  If you need to access a type in this assembly from COM, set the ComVisible
// attribute to true on that type.

[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM.

[assembly: Guid("BBE97FF0-A402-4C99-8283-719AC25219AE")]

[assembly: PreApplicationStartMethod(typeof(Startup), "OnPreApplicationStart")]
[assembly: ControllerContainer]
