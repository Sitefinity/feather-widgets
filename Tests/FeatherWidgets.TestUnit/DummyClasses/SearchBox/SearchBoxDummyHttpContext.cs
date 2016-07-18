using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Telerik.Sitefinity.Frontend.TestUtilities.DummyClasses.HttpContext;

namespace FeatherWidgets.TestUnit.DummyClasses.SearchBox
{
    public class SearchBoxDummyHttpContext : DummyHttpContext
    {
        private HttpRequestBase request;

        public SearchBoxDummyHttpContext(NameValueCollection queryString)
        {
            this.request = new SearchBoxDummyHttpRequest(this, "/", queryString);
        }

        public override HttpRequestBase Request
        {
            get
            {
                return this.request;
            }
        }
    }
}
