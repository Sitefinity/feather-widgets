using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm
{
    /// <summary>
    /// Class used to store external provider information from configuration file
    /// </summary>
    public class LoginFormExternalProvidersViewModel
    {
        /// <summary>
        /// Gets or sets provider name.
        /// </summary>
        /// <value>
        /// Provider name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets css class.
        /// </summary>
        /// <value>
        /// Css class.
        /// </value>
        public string CssClass { get; set; }
    }
}
