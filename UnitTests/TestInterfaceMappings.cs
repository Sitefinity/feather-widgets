using ContentBlock;
using ContentBlock.Mvc.Models;
using Navigation.Mvc.Models;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.DummyClasses.ContentBlock;
using UnitTests.DummyClasses.Navigation;

namespace UnitTests
{
    public class TestInterfaceMappings : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IContentBlockModel>().To<DummyContentBlockModel>().When( request => true);
            Bind<INavigationModel>().To<DummyNavigationModel>().When(request => true);
        }
    }
}
