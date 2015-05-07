using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Identity
{
    /// <summary>
    /// Elements from UsersListFrontend.
    /// </summary>
    public class UsersListFrontend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersListFrontend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public UsersListFrontend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets list of div elements containing users.
        /// </summary>
        public List<HtmlDiv> UsersDivsHybridPage
        {
            get
            {
                return this.Find.AllByTagName<HtmlDiv>("div").Where(d => d.ChildNodes.Count == 3
                                                                        && d.ChildNodes[0].TagName.Equals("img")
                                                                        && d.ChildNodes[1].TagName.Equals("h3")
                                                                        && d.ChildNodes[2].TagName.Equals("p")).ToList();
            }
        }

        /// <summary>
        /// Gets list of div elements containing users.
        /// </summary>
        /// <param name="firstLastUserName">first and last name of user</param>
        public HtmlAnchor GetSingleUserLink(string firstLastUserName)
        {
            return this.Get<HtmlAnchor>("tagname=a", "innerText=" + firstLastUserName);
        }

        /// <summary>
        /// Div element containing user on single user page.
        /// </summary>
        public HtmlDiv SingleUserDivHybridPage
        {
            get
            {
                return this.Find.AllByTagName<HtmlDiv>("div").Where(d => d.ChildNodes.Count == 4
                                                                        && d.ChildNodes[0].TagName.Equals("img")
                                                                        && d.ChildNodes[1].TagName.Equals("h3")
                                                                        && d.ChildNodes[2].TagName.Equals("p")
                                                                        && d.ChildNodes[3].TagName.Equals("p")).SingleOrDefault();
            }
        }

        /// <summary>
        /// Gets list of div elements containing users on bootstrap page.
        /// </summary>
        public List<HtmlDiv> UsersDivsBootstrapPage
        {
            get
            {
                return this.Find.AllByTagName<HtmlDiv>("div").Where(d => d.ChildNodes.Count == 2
                                                                        && d.ChildNodes[0].TagName.Equals("div")
                                                                        && d.ChildNodes[1].TagName.Equals("div")
                                                                        && d.ChildNodes[0].ChildNodes[0].TagName.Equals("img")
                                                                        && d.ChildNodes[1].ChildNodes[0].TagName.Equals("h3")
                                                                        && d.ChildNodes[1].ChildNodes[1].TagName.Equals("p")).ToList();
            }
        }

        /// <summary>
        /// Div element containing user on single user page on bootstrap page.
        /// </summary>
        public HtmlDiv SingleUserDivBootstrapPage
        {
            get
            {
                return this.Find.AllByTagName<HtmlDiv>("div").Where(d => d.ChildNodes.Count == 2
                                                                        && d.ChildNodes[0].TagName.Equals("div")
                                                                        && d.ChildNodes[1].TagName.Equals("div")
                                                                        && d.ChildNodes[0].ChildNodes[0].TagName.Equals("img")
                                                                        && d.ChildNodes[1].ChildNodes[0].TagName.Equals("h3")
                                                                        && d.ChildNodes[1].ChildNodes[1].TagName.Equals("p")
                                                                        && d.ChildNodes[1].ChildNodes[2].TagName.Equals("p")).SingleOrDefault();
            }
        }
    }
}
