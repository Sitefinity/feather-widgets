using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Lists.Mvc.Models;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;

namespace Telerik.Sitefinity.Frontend.Lists.Mvc.Controllers
{
    public class ListsController : Controller
    {
        #region Properties
        /// <summary>
        /// Gets the Lists widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public virtual IListsModel Model
        {
            get
            {
                if (this.model == null)
                    this.model = this.InitializeModel();

                return this.model;
            }
        }

        #endregion

        #region Private methods
        private IListsModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IListsModel>(this.GetType());
        }
        #endregion

        #region Private fields and constants
        private IListsModel model;
        #endregion
    }
}
