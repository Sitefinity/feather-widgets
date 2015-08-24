using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RazorEngine.Templating;

namespace FeatherWidgets.TestIntegration.Views
{
    /// <summary>
    /// Provides a base implementation of an MVC-compatible template.
    /// Allows the RazorEngine to compile views containing the Html and Url helpers.
    /// </summary>
    public class MvcTemplateBase<T> : TemplateBase<T>
    {
        #region Properties
        /// <summary>
        /// Gets the <see cref="HtmlHelper{Object}"/> for this template.
        /// </summary>
        public HtmlHelper<object> Html { get; private set; }

        /// <summary>
        /// Gets the <see cref="UrlHelper"/> for this template.
        /// </summary>
        public UrlHelper Url { get; private set; }

        /// <summary>
        /// Gets the request.
        /// </summary>
        /// <value>The request.</value>
        public HttpRequestBase Request
        {
            get
            {
                return null;
            }
        }
        #endregion
    }
}
