using System;
using System.Web.Mvc;
using MbUnit.Framework;
using RazorGenerator.Mvc;

namespace FeatherWidgets.TestIntegration.Views
{
    /// <summary>
    /// This attribute ensures that all views that are served during request are precompiled.
    /// </summary>
    /// <remarks>
    /// For test purposes only.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Precompilation"), AttributeUsage(AttributeTargets.All)]
    public sealed class PrecompilationFilterAttribute : ActionFilterAttribute
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            var actionViewResult = filterContext.Result as ViewResult;
            if (actionViewResult != null)
            {
                Assert.IsInstanceOfType<PrecompiledMvcView>(actionViewResult.View, string.Format("The view {0} is not precompiled.", actionViewResult.ViewName));
            }
        }
    }
}
