using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Navigation.Mvc.Models
{
    /// <summary>
    /// Classes that implement this interface could be used as model for the Navigation widget.
    /// </summary>
    public interface INavigationModel
    {
        /// <summary>
        /// Gets or sets the list of site map nodes that will be displayed in the navigation widget.
        /// </summary>
        /// <value>
        /// The nodes.
        /// </value>
        IList<NodeViewModel> Nodes { get; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the NavigationWidget (if such is presented).
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        string CssClass { set; get; }
    }
}
