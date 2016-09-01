using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.Controls.HtmlControls;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Widgets
{
    public class WidgetCalendarScreen : WidgetDesignerScreen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetCalendarScreen" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public WidgetCalendarScreen(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets Single item settings tab  
        /// </summary>
        public HtmlAnchor SingleItemSettings
        {
            get
            {
                return this.Get<HtmlAnchor>("class=~ng-binding", "Innertext=Single item settings");
            }
        }

        /// <summary>
        /// Gets Selected existing page in Single item settings tab  
        /// </summary>
        public HtmlInputRadioButton ExistingPage
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("id=existingPage");
            }
        }

        /// <summary>
        /// Gets Monday radio button in List Settings tab
        /// </summary>
        public HtmlInputRadioButton WeekStartsOnMonday
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("id=mondaySelect");
            }
        }

        /// <summary>
        /// Gets Sunday radio button in List Settings tab
        /// </summary>
        public HtmlInputRadioButton WeekStartsOnSunday
        {
            get
            {
                return this.Get<HtmlInputRadioButton>("id=sundaySelect");
            }
        }

        /// <summary>
        /// Gets the default view dropdown.
        /// </summary>
        /// <value>
        /// The default view dropdown.
        /// </value>
        public HtmlSelect DefaultViewDropdown
        {
            get
            {
                return this.Get<HtmlSelect>("id=defaultSchedulerView");
            }
        }

        /// <summary>
        /// Gets the allow users to switch views CheckBox.
        /// </summary>
        /// <value>
        /// The allow users to switch views CheckBox.
        /// </value>
        public HtmlInputCheckBox AllowUsersToSwitchViewsCheckBox
        {
            get
            {
                return this.Get<HtmlInputCheckBox>("type=checkbox", "ng-model=properties.AllowChangeCalendarView.PropertyValue");
            }
        }
    }
}
