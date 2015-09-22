﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend.PageEditor;

namespace FeatherWidgets.TestUI.TestCases.Navigation
{
    /// <summary>
    /// UI tests for navigation widget on semantic UI template
    /// </summary>
    [TestClass]
    public class NavigationWidgetOnSemanticTemplate : FeatherTestCase
    {
        /// <summary>
        /// UI test DragAndDropNavigationWidgetOnSemanticTemplate
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Navigation),
        TestCategory(FeatherTestCategories.SemanticUI)]
        public void DragAndDropNavigationWidgetOnSemanticTemplate()
        {
            string[] parentPages = new string[] { Page1, Page2 };

            BAT.Macros().NavigateTo().Design().PageTemplates(this.Culture);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(PageTemplateName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SwitchEditorLayoutMode(EditorLayoutMode.Layout);
            BAT.Wrappers().Backend().Pages().PageLayoutEditorWrapper().SelectAnotherTemplate();
            BAT.Wrappers().Backend().Pages().SelectTemplateWrapper().SelectATemplate("SemanticUI.default");
            BAT.Wrappers().Backend().Pages().SelectTemplateWrapper().ClickDoneButton();

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();

            BAT.Arrange(this.ArrangementClass).ExecuteArrangement(this.ArrangementMethod);

            BAT.Macros().NavigateTo().CustomPage("~/" + Page1.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(NavTemplateClass, parentPages, TemplateType.Semantic);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().ClickOnPageLinkFromNavigationMenu(Page2, TemplateType.Semantic, NavTemplateClass);
            BATFeather.Wrappers().Frontend().Navigation().NavigationWrapper().VerifyNavigationOnThePageFrontend(NavTemplateClass, parentPages, TemplateType.Semantic);        
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.ArrangementClass).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.ArrangementClass).ExecuteTearDown();
        }

        private string ArrangementClass
        {
            get { return ArrangementClassName; }
        }

        private string ArrangementMethod
        {
            get { return ArrangementMethodName; }
        }

        private const string Page1 = "SemanticUIPage1";
        private const string Page2 = "SemanticUIPage2";
        private const string PageTemplateName = "defaultNew";
        private const string ArrangementClassName = "NavigationWidgetOnSemanticTemplate";
        private const string ArrangementMethodName = "CreatePages";
        private const string WidgetName = "Navigation";
        private const string NavTemplateClass = "ui menu inverted";
    }
}
