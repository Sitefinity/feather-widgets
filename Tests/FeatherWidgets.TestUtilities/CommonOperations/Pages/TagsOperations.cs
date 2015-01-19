using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUtilities.CommonOperations.Pages
{
    public class TagsOperations
    {
        public void EditTag(string oldTitle, string newTitle)
        {
            var manager = TaxonomyManager.GetManager();
            var tag = manager.GetTaxonomies<FlatTaxonomy>().Where(t => t.Name == "Tags").SingleOrDefault();
            if (tag != null)
            {
                FlatTaxon taxon = manager.GetTaxa<FlatTaxon>().Where(t => t.Title == oldTitle).SingleOrDefault();

                if (taxon != null)
                {
                    //// Code below is commented, because code below is not editing the title, which is relevant for UI tests
                    taxon.Title = newTitle;
                   
                    //// taxon.Title = new Lstring(newTitle, CultureInfo.InvariantCulture);
                    //// taxon.Name = Regex.Replace(newTitle, " ", string.Empty);
                  
                    taxon.UrlName = Regex.Replace(newTitle.ToLower(CultureInfo.CurrentCulture), ArrangementConstants.UrlNameCharsToReplace, ArrangementConstants.UrlNameReplaceString);
                    taxon.Taxonomy = tag;

                    manager.SaveChanges();
                }
            }
        }
    }
}
