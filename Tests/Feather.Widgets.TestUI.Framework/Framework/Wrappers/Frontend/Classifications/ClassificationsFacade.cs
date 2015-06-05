using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Frontend
{
    public class ClassificationsWrapperFacade
    {
        /// <summary>
        /// Provides unified access to the ClassificationsWrapper
        /// </summary>
        /// <returns>Returns the NewsWrapper</returns>
        public ClassificationsWrapper ClassificationsWrapper()
        {
            return new ClassificationsWrapper();
        }
    }
}
