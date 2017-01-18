using System.Security.Principal;
using Telerik.Sitefinity.Frontend.TestUtilities.DummyClasses.HttpContext;

namespace FeatherWidgets.TestUnit.DummyClasses.Forms.NavigationField
{
    /// <summary>
    /// DummyHttpContext for form navigation field
    /// </summary>
    public class NavigationDummyHttpContext : DummyHttpContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationDummyHttpContext"/> class.
        /// </summary>
        public NavigationDummyHttpContext()
            : base()
        {
            this.user = new GenericPrincipal(new GenericIdentity("admin"), new string[] { "Administrator" });
        }

        /// <inheritDocs />
        public override IPrincipal User
        {
            get
            {
                return this.user;
            }

            set
            {
                this.user = value;
            }
        }

        private IPrincipal user;
    }
}
