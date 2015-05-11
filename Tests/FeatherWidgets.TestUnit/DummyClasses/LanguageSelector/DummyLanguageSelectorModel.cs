using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.LanguageSelector;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Pages.Model;

namespace FeatherWidgets.TestUnit.DummyClasses.LanguageSelector
{
    public class DummyLanguageSelectorModel : LanguageSelectorModel
    {
        protected override UrlLocalizationService InitializeUrlLocalizationService()
        {
            return null;
        }

        protected override string ResolvePageUrl(PageNode pageNode, CultureInfo targetCulture, bool useNewImplementation)
        {
            return string.Empty;
        }

        protected override string ResolveUrl(string url, CultureInfo targetCulture)
        {
            return string.Empty;
        }

        protected override IEnumerable<CultureInfo> GetLanguagesToDisplay()
        {
            return new List<CultureInfo>();
        }
    }
}
