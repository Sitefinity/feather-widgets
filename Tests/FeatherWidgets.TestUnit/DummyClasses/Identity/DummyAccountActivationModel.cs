using System;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.AccountActivation;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;

namespace FeatherWidgets.TestUnit.DummyClasses.Identity
{
    public class DummyAccountActivationModel : AccountActivationModel
    {
        public static readonly string Provider = "Provider name";
        public static readonly string PageUrl = "http://www.temporg.com";

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly"),
         SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public NameValueCollection QueryString { get; set; }

        protected override NameValueCollection GetQueryString()
        {
            return this.QueryString ?? new NameValueCollection();
        }

        protected override string GetPageUrl(Guid? pageId)
        {
            return PageUrl;
        }

        protected override UserManager GetUserManager()
        {
            return new MockedUserManager(Provider);
        }

        private class MockedUserManager : UserManager
        {
            private readonly DummyMembershipDataProvider provider;

            public MockedUserManager(string provider)
                : base(provider)
            {
                this.provider = new DummyMembershipDataProvider();
            }

            public override MembershipDataProvider Provider
            {
                get
                {
                    return this.provider;
                }
            }

            public override void SaveChanges()
            {
            }

            protected override void Initialize()
            {
            }

            protected override void SetProvider(string providerName, string transactionName)
            {
            }
        }

        private class DummyMembershipDataProvider : MembershipDataProvider
        {
            public override Telerik.Sitefinity.Security.Model.User CreateUser(Guid id, string userName)
            {
                return new User { FirstName = "Name" };
            }

            public override Telerik.Sitefinity.Security.Model.User CreateUser(string userName)
            {
                return new User { FirstName = "Name" };
            }

            public override void Delete(Telerik.Sitefinity.Security.Model.User item)
            {
            }

            public override User GetUser(Guid id)
            {
                return new User { FirstName = "Name" };
            }

            public override IQueryable<Telerik.Sitefinity.Security.Model.User> GetUsers()
            {
                return null;
            }
        }
    }
}
