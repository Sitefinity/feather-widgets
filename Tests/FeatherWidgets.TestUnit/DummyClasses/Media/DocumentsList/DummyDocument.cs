using System;
using System.Linq;
using Telerik.Sitefinity.Model;

namespace FeatherWidgets.TestUnit.DummyClasses.Media.DocumentsList
{
    public class DummyDocument : Telerik.Sitefinity.Libraries.Model.Document
    {
        public DummyDocument(string appName, Guid id)
            : base(appName, id)
        {
        }

        public override Lstring Title
        {
            get;
            set;
        }
    }
}
