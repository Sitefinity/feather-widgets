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
        /// Gets ParagraphTextFieldController Div 
        /// </summary>
        public HtmlDiv ParagraphTextFieldControllerDiv
        {
            get
            {
                return this.BodyDropZone.Find.ByExpression<HtmlDiv>("class=RadDock RadDock_Default zeControlDock", "behaviourobjecttype=~ParagraphTextFieldController");
            }
        }

        /// <summary>
        /// Gets CaptchaController div
        /// </summary>
        public HtmlDiv CaptchaControllerDiv
        {
            get
            {
                return this.BodyDropZone.Find.ByExpression<HtmlDiv>("class=RadDock RadDock_Default zeControlDock", "behaviourobjecttype=~CaptchaController");
            }
        }

        /// <summary>
        /// Gets MultipleChoiceFieldController div
        /// </summary>
        public HtmlDiv MultipleChoiceFieldControllerDiv
        {
            get
            {
                return this.BodyDropZone.Find.ByExpression<HtmlDiv>("class=RadDock RadDock_Default zeControlDock", "behaviourobjecttype=~MultipleChoiceFieldController");
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
        /// Gets Save Draft Button
        /// </summary>
        public HtmlAnchor SaveDraftButton
        {
            get
            {
                return this.Get<HtmlAnchor>("onclick=editorToolBar.saveDraft();");
            }
        }

        /// <summary>
        /// Gets Preview Button from within Create dialog
        /// </summary>
        public HtmlAnchor PreviewButton
        {
            get
            {
                return this.Get<HtmlAnchor>("href=~Preview");
            }
        }

        /// <summary>
        /// Gets "The draft is successfully saved" message
        /// </summary>
        public HtmlSpan PositiveMessageDraftIsSaved
        {
            get
            {
               return this.Get<HtmlSpan>("id=?messageControl", "class=sfMessage sfMsgPositive sfMsgVisible", "Innertext=The draft is successfully saved");
           }
        }
    }
}
