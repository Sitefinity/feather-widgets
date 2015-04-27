using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.UsersList;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.Attributes;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers
{
    /// <summary>
    /// This class represents the controller of Users list widget.
    /// </summary>
    [ControllerToolboxItem(Name = "UsersList", Title = "Users list", SectionName = "MvcWidgets")]
    [Localization(typeof(UsersListResources))]
    public class UsersListController :  Controller
    {
        #region Properties

         /// <summary>
        /// Gets or sets the name of the template that will be displayed when widget is in List view.
        /// </summary>
        /// <value></value>
        public string ListTemplateName
        {
            get
            {
                return this.listTemplateName;
            }

            set
            {
                this.listTemplateName = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the template that will be displayed when widget is in Detail view.
        /// </summary>
        /// <value></value>
        public string DetailTemplateName
        {
            get
            {
                return this.detailTemplateName;
            }

            set
            {
                this.detailTemplateName = value;
            }
        }

         /// <summary>
        /// Gets or sets a value indicating whether detail view for a user should be opened in the same page.
        /// </summary>
        /// <value>
        /// <c>true</c> if details link should be opened in the same page; otherwise, (if should redirect to custom selected page)<c>false</c>.
        /// </value>
        public bool OpenInSamePage
        {
            get
            {
                return this.openInSamePage;
            }

            set
            {
                this.openInSamePage = value;
            }
        }

        /// <summary>
        /// Gets or sets the id of the page where will be displayed details view for selected user.
        /// </summary>
        /// <value>The details page id.</value>
        public Guid DetailsPageId { get; set; }

        /// <summary>
        /// Gets the Users list widget model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IUsersListModel Model 
        {
            get
            {
                if (this.model == null)
                    this.model = this.InitializeModel();

                return this.model;
            }
        }

        #endregion

        #region Actions
        
        /// <summary>
        /// Renders appropriate list view depending on the <see cref="ListTemplateName" />
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult Index(int? page)
        {
            var fullTemplateName = this.listTemplateNamePrefix + this.ListTemplateName;
            this.ViewBag.CurrentPageUrl = this.GetCurrentPageUrl();
            this.ViewBag.RedirectPageUrlTemplate = this.ViewBag.CurrentPageUrl + "/{0}";
            this.ViewBag.DetailsPageId = this.DetailsPageId;
            this.ViewBag.OpenInSamePage = this.OpenInSamePage;

            var viewModel = this.Model.CreateListViewModel(page: page ?? 1);
            ////if (SystemManager.CurrentHttpContext != null)
            ////    this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

            return this.View(fullTemplateName, viewModel);
        }

        /// <summary>
        /// Renders appropriate list view depending on the <see cref="DetailTemplateName"/>
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public ActionResult Details(User user)
        {
            var fullTemplateName = this.detailTemplateNamePrefix + this.DetailTemplateName;
            this.ViewBag.UserName = user.UserName;

            var viewModel = this.Model.CreateDetailsViewModel(user);
            ////if (SystemManager.CurrentHttpContext != null)
            ////    this.AddCacheDependencies(this.Model.GetKeysOfDependentObjects(viewModel));

            return this.View(fullTemplateName, viewModel);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>
        /// The <see cref="IUsersListModel"/>.
        /// </returns>
        private IUsersListModel InitializeModel()
        {
            return ControllerModelFactory.GetModel<IUsersListModel>(this.GetType());
        }

        #endregion

        #region Private fields and constants

        private IUsersListModel model;
        private string listTemplateName = "UsersList";
        private string listTemplateNamePrefix = "List.";
        private string detailTemplateName = "UserDetails";
        private string detailTemplateNamePrefix = "Detail.";
        private bool openInSamePage = true;

        ////private bool? disableCanonicalUrlMetaTag;

        #endregion

    }
}
