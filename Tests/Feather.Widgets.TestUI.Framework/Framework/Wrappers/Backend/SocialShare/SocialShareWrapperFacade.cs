using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend
{
    public class SocialShareWrapperFacade
    {       
        /// <summary>
        /// Socials the share widget edit wrapper.
        /// </summary>
        /// <returns></returns>
        public SocialShareWidgetEditWrapper SocialShareWidgetEditWrapper()
        {
            return new SocialShareWidgetEditWrapper();
        }

        /// <summary>
        /// Socials share page editor wrapper.
        /// </summary>
        /// <returns></returns>
        public SocialSharePageEditorWrapper SocialSharePageEditorWrapper()
        {
            return new SocialSharePageEditorWrapper();
        }
    }
}
