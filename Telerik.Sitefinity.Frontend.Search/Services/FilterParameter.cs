using System.Collections.Generic;

namespace Telerik.Sitefinity.Frontend.Search.Services
{
    internal class FilterParameter
    {
        public FilterParameter()
        {
            this.AppliedFilters = new List<FilterModel>();
        }

        public List<FilterModel> AppliedFilters { get; set; }

        public bool IsDeselected { get; set; }

        public string LastSelectedFilterGropName { get; set; }
    }
}
