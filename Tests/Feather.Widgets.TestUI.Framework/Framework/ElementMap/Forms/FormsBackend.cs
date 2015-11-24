using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.TestTemplates;

namespace Feather.Widgets.TestUI.Framework.Framework.ElementMap.Forms
{
    /// <summary>
    /// Elements from Froms backend.
    /// </summary>
    public class FormsBackend : HtmlElementContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormsBackend" /> class.
        /// </summary>
        /// <param name="find">The find.</param>
        public FormsBackend(Find find)
            : base(find)
        {
        }

        /// <summary>
        /// Gets the checkbox reponse field in backend
        /// </summary>
        public HtmlDiv GetResponseCheckboxesField
        {
            get
            {
                return this.ResponseDetailsPane.Find.ByExpression<HtmlDiv>("TagName=div", "class=sfChoiceContent", "id=~frmRspnsesCntView_formsBackendListDetail");
            }
        }

        /// <summary>
        /// Gets the dropdown list reponse field in backend
        /// </summary>
        public HtmlDiv GetResponseDropdownListField
        {
            get
            {
                return this.ResponseDetailsPane.Find.ByExpression<HtmlDiv>("TagName=div", "class=sfChoiceContent", "id=~frmRspnsesCntView_formsBackendListDetail");
            }
        }

        /// <summary>
        /// Gets the text field in designer of section header field
        /// </summary>
        public HtmlTableCell SectionHeaderText
        {
            get
            {
                return this.Get<HtmlTableCell>("TagName=td", "class=k-editable-area");
            }
        }

        /// <summary>
        /// Provides access to the response details pane for the selected response
        /// </summary>
        public HtmlControl ResponseDetailsPane
        {
            get
            {
                return this.Get<HtmlControl>("tagname=div", "class=sfResponceDetailsWrp");
            }
        }

        /// <summary>
        /// Gets the Body Drop Zone in the Forms Edit Screen.
        /// </summary>
        public HtmlDiv BodyDropZone
        {
            get
            {
                return this.Find.AssociatedBrowser.GetControl<HtmlDiv>("id=PublicWrapper");
            }
        }

        /// <summary>
        /// Gets required field checkbox.
        /// </summary>
        public HtmlInputCheckBox RequiredFieldCheckBox
        {
            get
            {
                return this.Get<HtmlInputCheckBox>("type=checkbox", "ng-model=properties.Model.ValidatorDefinition.Required.PropertyValue");
            }
        }

        /// <summary>
        /// Gets next step input.
        /// </summary>
        public HtmlInputText NextStepInput
        {
            get
            {
                return this.Find.AssociatedBrowser.GetControl<HtmlInputText>("id=nextStep");
            }
        }

        /// <summary>
        /// Gets SubmitButtonController div
        /// </summary>
        public HtmlDiv SubmitButtonControllerDiv
        {
            get
            {
                return this.BodyDropZone.Find.ByExpression<HtmlDiv>("class=RadDock RadDock_Default zeControlDock", "behaviourobjecttype=~SubmitButtonController");
            }
        }

        /// <summary>
        /// Gets CheckboxesFieldController div
        /// </summary>
        public HtmlDiv CheckboxesFieldControllerDiv
        {
            get
            {
                return this.BodyDropZone.Find.ByExpression<HtmlDiv>("class=RadDock RadDock_Default zeControlDock", "behaviourobjecttype=~CheckboxesFieldController");
            }
        }

        /// <summary>
        /// Gets CheckboxesFieldController div
        /// </summary>
        public HtmlDiv DropdownListFieldControllerDiv
        {
            get
            {
                return this.BodyDropZone.Find.ByExpression<HtmlDiv>("class=RadDock RadDock_Default zeControlDock", "behaviourobjecttype=~DropdownListFieldController");
            }
        }
        /// <summary>
        /// Gets All Controller's divs
        /// </summary>
        public List<HtmlDiv> FormControlList
        {
            get
            {
                return this.BodyDropZone.Find.AllByExpression<HtmlDiv>("class=RadDock RadDock_Default zeControlDock").ToList();
            }
        }

        /// <summary>
        /// Gets PageBreakController div
        /// </summary>
        public HtmlDiv PageBreakControllerDiv
        {
            get
            {
                return this.BodyDropZone.Find.ByExpression<HtmlDiv>("class=RadDock RadDock_Default zeControlDock", "behaviourobjecttype=~PageBreakController");
            }
        }

        /// <summary>
        /// Gets common header
        /// </summary>
        public HtmlDiv CommonHeaderDiv
        {
          get
            {
                return this.Get<HtmlDiv>("placeholderid=" + "Header");
            }
        }

        /// <summary>
        /// Gets common footer
        /// </summary>
        public HtmlDiv CommonFooterDiv
        {
            get
            {
                return this.Get<HtmlDiv>("placeholderid=" + "Footer");
            }
        }

        /// <summary>
        /// Gets cancel button.
        /// </summary>
        public HtmlAnchor CancelButton
        {
            get
            {
                return this.Get<HtmlAnchor>("tagname=a", "class=btn btn-link pull-left ng-scope");
            }
        }
    }
}
