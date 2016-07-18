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
    public class SearchBoxDummyHttpRequest : DummyHttpRequest
    {
        private readonly NameValueCollection queryString;

        public SearchBoxDummyHttpRequest(HttpContextBase httpContext, string appPath, NameValueCollection queryString)
                : base(httpContext, appPath)
        {
            this.queryString = queryString;
        }

        public override NameValueCollection QueryString
        {
            get
            {
                return this.queryString;
            }
        }
    }
}
