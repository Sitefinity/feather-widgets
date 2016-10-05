﻿using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning.Comparison;

namespace Telerik.Sitefinity.Frontend.Identity.Mvc.Models.UsersList
{
    /// <summary>
    /// This class represents model used for Users list widget.
    /// </summary>
    public class UsersListModel : IUsersListModel
    {
        public UsersListModel()
        {
            this.ProfileTypeFullName = this.ContentType.FullName;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the type of content that is loaded.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public Type ContentType
        {
            get
            {
                return typeof(SitefinityProfile);
            }
        }

        /// <summary>
        /// Gets the list of items to be displayed inside the widget when option "Selected items" is enabled.
        /// </summary>
        /// <value>
        /// The selected item ids.
        /// </value>
        public string SerializedSelectedItemsIds
        {
            get
            {
                return this.serializedSelectedItemsIds;
            }

            set
            {
                if (this.serializedSelectedItemsIds != value)
                {
                    this.serializedSelectedItemsIds = value;
                    if (!this.serializedSelectedItemsIds.IsNullOrEmpty())
                    {
                        this.selectedItemsIds = JsonSerializer.DeserializeFromString<IList<string>>(this.serializedSelectedItemsIds);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapping element of the widget when it is in List view.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string ListCssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class that will be applied on the wrapper div of the widget when it is in Details view.
        /// </summary>
        /// <value>
        /// The CSS class.
        /// </value>
        public string DetailCssClass { get; set; }

        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        /// <value>The name of the provider.</value>
        public string ProviderName
        {
            get
            {
                this.providerName = this.providerName ?? UserProfileManager.GetDefaultProviderName();
                return this.providerName;
            }
            set
            {
                this.providerName = value;
            }
        }

        /// <summary>
        /// Gets or sets which items to be displayed in the list view.
        /// </summary>
        /// <value>The page display mode.</value>
        public SelectionMode SelectionMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to divide items in the list.
        /// </summary>
        /// <value>
        /// The display mode.
        /// </value>
        public ListDisplayMode DisplayMode { get; set; }

        /// <summary>
        /// Gets or sets the items count per page.
        /// </summary>
        /// <value>
        /// The items per page.
        /// </value>
        public int? ItemsPerPage
        {
            get
            {
                return this.itemsPerPage;
            }

            set
            {
                this.itemsPerPage = value;
            }
        }

        /// <summary>
        /// Gets or sets the items limit count.
        /// </summary>
        /// <value>
        /// The items limit.
        /// </value>
        public int? LimitCount
        {
            get
            {
                return this.limitCount;
            }

            set
            {
                this.limitCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the sort expression.
        /// </summary>
        /// <value>
        /// The sort expression.
        /// </value>
        public string SortExpression
        {
            get
            {
                return this.sortExpression;
            }

            set
            {
                this.sortExpression = value;
            }
        }

        /// <summary>
        /// Gets or sets the additional filter expression.
        /// </summary>
        /// <value>
        /// The filter expression.
        /// </value>
        public string FilterExpression { get; set; }

        /// <summary>
        /// Gets or sets the serialized additional filters.
        /// </summary>
        /// <value>
        /// The serialized additional filters.
        /// </value>
        public string SerializedAdditionalFilters
        {
            get
            {
                return this.serializedAdditionalFilters;
            }
            set
            {
                if (this.serializedAdditionalFilters != value)
                {
                    this.serializedAdditionalFilters = value;
                    if (!this.serializedAdditionalFilters.IsNullOrEmpty())
                    {
                        this.selectedRolesFilter = JsonSerializer.DeserializeFromString<IList<Role>>(this.serializedAdditionalFilters);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the serialized date filters.
        /// </summary>
        /// <value>
        /// The serialized date filters.
        /// </value>
        public string SerializedDateFilters
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the a configured instance of the UserProfileManager.
        /// </summary>
        /// <value>The manager.</value>
        public UserProfileManager Manager
        {
            get
            {
                if (this.manager == null)
                    this.manager = UserProfileManager.GetManager(this.ProviderName);

                return this.manager;
            }
        }

        /// <inheritDoc/>
        public string ProfileTypeFullName { get; set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Creates a view model for use in list views.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>A view model for use in list views.</returns>
        /// <exception cref="System.ArgumentException">'page' argument has to be at least 1.;page</exception>
        public UsersListViewModel CreateListViewModel(int page)
        {
            if (page < 1)
                throw new ArgumentException("'page' argument has to be at least 1.", "page");

            var viewModel = this.CreateListViewModelInstance();
            this.PopulateListViewModel(page, viewModel);

            return viewModel;
        }

        /// <summary>
        /// Creates the details view model.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A view model for use in detail views.</returns>
        public UserDetailsViewModel CreateDetailsViewModel(IDataItem item)
        {
            var viewModel = this.CreateDetailsViewModelInstance();

            viewModel.CssClass = this.DetailCssClass;
            viewModel.Item = this.CreateItemViewModelInstance(item);
            viewModel.ContentType = this.ContentType;
            viewModel.ProviderName = this.ProviderName;

            return viewModel;
        }

        /// <inheritDoc/>
        public IEnumerable<CacheDependencyKey> GetKeysOfDependentObjects(UsersListViewModel viewModel)
        {
            var contentResolvedType = this.ContentType;
            var result = new List<CacheDependencyKey>(1);
            result.Add(new CacheDependencyKey { Key = contentResolvedType.FullName, Type = contentResolvedType });

            return result;
        }

        /// <inheritDoc/>
        public IEnumerable<CacheDependencyKey> GetKeysOfDependentObjects(UserDetailsViewModel viewModel)
        {
            var contentResolvedType = this.ContentType;
            var result = new List<CacheDependencyKey>(1);
            if (viewModel.Item != null && viewModel.Item.Fields.Id != Guid.Empty)
            {
                result.Add(new CacheDependencyKey { Key = viewModel.Item.Fields.Id.ToString(), Type = contentResolvedType });
            }

            return result;
        }
        #endregion

        #region Protected methods

        /// <summary>
        /// Creates a blank instance of a list view model.
        /// </summary>
        /// <returns>The list view model.</returns>
        protected UsersListViewModel CreateListViewModelInstance()
        {
            return new UsersListViewModel();
        }

        /// <summary>
        /// Creates a blank instance of a details view model.
        /// </summary>
        /// <returns>The details view model.</returns>
        protected UserDetailsViewModel CreateDetailsViewModelInstance()
        {
            return new UserDetailsViewModel();
        }

        /// <summary>
        /// Creates an instance of the item view model by given data item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        protected ItemViewModel CreateItemViewModelInstance(IDataItem item)
        {
            if (item.GetType() == typeof(SitefinityProfile))
            {
                return new SitefinityProfileItemViewModel(item);
            }
            else
            {
                return new ItemViewModel(item);
            }
        }

        /// <summary>
        /// Populates the list ViewModel.
        /// </summary>
        /// <param name="page">The current page.</param>
        /// <param name="viewModel">The view model.</param>
        protected void PopulateListViewModel(int page, UsersListViewModel viewModel)
        {
            int? totalPages = null;
            if (this.SelectionMode == SelectionMode.SelectedItems && this.selectedItemsIds.Count == 0)
            {
                viewModel.Items = Enumerable.Empty<SitefinityProfileItemViewModel>();
            }
            else
            {
                viewModel.Items = this.ApplyListSettings(page, out totalPages);
            }

            this.SetViewModelProperties(viewModel, page, totalPages);
        }

        /// <summary>
        /// Sets the view model properties.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="page">The page.</param>
        /// <param name="totalPages">The total pages.</param>
        protected void SetViewModelProperties(UsersListViewModel viewModel, int page, int? totalPages)
        {
            viewModel.CurrentPage = page;
            viewModel.TotalPagesCount = totalPages;
            viewModel.ProviderName = this.ProviderName;
            viewModel.ContentType = TypeResolutionService.ResolveType(this.ProfileTypeFullName);
            viewModel.CssClass = this.ListCssClass;
            viewModel.ShowPager = this.DisplayMode == ListDisplayMode.Paging && totalPages.HasValue && totalPages > 1;
        }

        /// <summary>
        /// Applies the list settings.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="totalPages">The total pages.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#")]
        protected IEnumerable<ItemViewModel> ApplyListSettings(int page, out int? totalPages)
        {
            if (page < 1)
                throw new ArgumentException("'page' argument has to be at least 1.", "page");

            int? itemsToSkip = (page - 1) * this.ItemsPerPage;
            itemsToSkip = this.DisplayMode == ListDisplayMode.Paging ? ((page - 1) * this.ItemsPerPage) : null;
            int? totalCount = 0;
            int? take = null;

            if (this.DisplayMode == ListDisplayMode.Limit)
            {
                take = this.LimitCount;
            }
            else if (this.DisplayMode == ListDisplayMode.Paging)
            {
                take = this.ItemsPerPage;
            }

            var result = new List<ItemViewModel>();

            var query = this.UpdateExpression(itemsToSkip, take, ref totalCount);

            if (query != null)
            {
                var queryResult = query.Cast<IDataItem>().ToArray<IDataItem>();

                foreach (var item in queryResult)
                {
                    result.Add(this.CreateItemViewModelInstance(item));
                }
            }

            totalPages = (int)Math.Ceiling(totalCount.Value / (double)this.ItemsPerPage.Value);
            totalPages = this.DisplayMode == ListDisplayMode.Paging ? totalPages : null;

            return result;
        }

        /// <summary>
        /// Updates the expression.
        /// </summary>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "3#")]
        protected IQueryable UpdateExpression(int? skip, int? take, ref int? totalCount)
        {
            var compiledFilterExpression = this.CompileFilterExpression();

            if (this.SelectionMode != Frontend.Mvc.Models.SelectionMode.AllItems && compiledFilterExpression.IsNullOrEmpty())
            {
                return null;
            }
            else
            {
                IQueryable query = this.SetExpression(
                    compiledFilterExpression,
                    this.SortExpression,
                    skip,
                    take,
                    ref totalCount);

                return query;
            }
        }

        /// <summary>
        /// Modifies the given query with the given filter, sort expression and paging.
        /// </summary>
        /// <param name="filterExpression">The filter expression.</param>
        /// <param name="sortExpr">The sort expression.</param>
        /// <param name="itemsToSkip">The items to skip.</param>
        /// <param name="itemsToTake">The items to take.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns>Resulting filtered query.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Expr"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "5#")]
        protected IQueryable SetExpression(string filterExpression, string sortExpr, int? itemsToSkip, int? itemsToTake, ref int? totalCount)
        {
            IQueryable query = null;

            var profileType = TypeResolutionService.ResolveType(this.ProfileTypeFullName);

            var skip = itemsToSkip.HasValue ? itemsToSkip.Value : 0;
            var take = itemsToTake.HasValue ? itemsToTake.Value : 0;

            try
            {
                query = this.Manager.Provider.GetItems(
                                                    profileType,
                                                    filterExpression,
                                                    sortExpr,
                                                    skip,
                                                    take,
                                                    ref totalCount).AsQueryable();
            }
            catch (MemberAccessException)
            {
                this.SortExpression = DefaultSortExpression;
                query = this.Manager.Provider.GetItems(
                                                    profileType,
                                                    filterExpression,
                                                    null,
                                                    skip,
                                                    take,
                                                    ref totalCount).AsQueryable();
            }

            return query;
        }

        /// <summary>
        /// Compiles a filter expression based on the widget settings.
        /// </summary>
        /// <returns>Filter expression that will be applied on the query.</returns>
        protected string CompileFilterExpression()
        {
            var elements = new List<string>();

            if (this.SelectionMode == SelectionMode.FilteredItems)
            {
                var selectedUsersByRolesFilterExpression = this.GetUsersByRolesFilterExpression();
                if (!selectedUsersByRolesFilterExpression.IsNullOrEmpty())
                {
                    elements.Add(selectedUsersByRolesFilterExpression);
                }
            }
            else if (this.SelectionMode == SelectionMode.SelectedItems)
            {
                var selectedItemsFilterExpression = this.GetSelectedItemsFilterExpression();
                if (!selectedItemsFilterExpression.IsNullOrEmpty())
                {
                    elements.Add(selectedItemsFilterExpression);
                }
            }

            if (!this.FilterExpression.IsNullOrEmpty())
            {
                elements.Add(this.FilterExpression);
            }

            return string.Join(" AND ", elements.Select(el => "(" + el + ")"));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Telerik.Sitefinity", "SF1002:AvoidToListOnIEnumerable")]
        private string GetSelectedItemsFilterExpression()
        {
            var selectedItemGuids = this.selectedItemsIds.Select(id => new Guid(id));

            List<Guid> userProfilesGuids = new List<Guid>();
            foreach (var id in selectedItemGuids)
            {
                var currentProfile = this.Manager.GetUserProfile(id, this.ProfileTypeFullName);
                if (currentProfile != null && !userProfilesGuids.Contains(currentProfile.Id))
                    userProfilesGuids.Add(currentProfile.Id);
            }

            var selectedItemConditions = userProfilesGuids.Select(id => "Id = {0}".Arrange(id.ToString("D")));
            var selectedItemsFilterExpression = string.Join(" OR ", selectedItemConditions);

            return selectedItemsFilterExpression;
        }

        private string GetUsersByRolesFilterExpression()
        {
            List<Telerik.Sitefinity.Security.Model.User> allUsers = new List<Telerik.Sitefinity.Security.Model.User>();
            foreach (var role in this.selectedRolesFilter)
            {
                var roleManager = RoleManager.GetManager(role.ProviderName);
                var usersInRole = roleManager.GetUsersInRole(role.Id);
                allUsers.AddRange(usersInRole);
            }

            List<Guid> userProfilesGuids = new List<Guid>();
            foreach (var user in allUsers)
            {
                var currentProfile = this.Manager.GetUserProfile(user, this.ProfileTypeFullName);
                if (currentProfile != null && !userProfilesGuids.Contains(currentProfile.Id))
                    userProfilesGuids.Add(currentProfile.Id);
            }

            var selectedItemConditions = userProfilesGuids.Select(id => "Id = {0}".Arrange(id.ToString("D")));
            var selectedItemsFilterExpression = string.Join(" OR ", selectedItemConditions);

            return selectedItemsFilterExpression;
        }
        #endregion

        #region Private fields and constants

        private const string DefaultSortExpression = "FirstName ASC";

        private UserProfileManager manager;
        private string providerName;
        private int? itemsPerPage = 20;
        private int? limitCount = 20;
        private string sortExpression = DefaultSortExpression;
        private string serializedSelectedItemsIds;
        private string serializedAdditionalFilters;
        private IList<string> selectedItemsIds = new List<string>();
        private IList<Role> selectedRolesFilter = new List<Role>();

        #endregion

        private class Role
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string ProviderName { get; set; }
        }

    }
}
