using System;
using System.Linq;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers;

namespace FeatherWidgets.TestUnit.DummyClasses.Identity
{
    public class DummyUsersListController : UsersListController
    {
        protected override string GetPageUrl()
        {
            return string.Empty;
        }
    }
}
