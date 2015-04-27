using System;
using System.Linq;
using System.Web.UI;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.JavaScript
{
    public class JavaScriptModel : IJavaScriptModel
    {
        public string CustomCode { get; set; }

        public string FileUrl { get; set; }

        public EmbedPosition Position { get; set; }

        public string Description { get; set; }

        public virtual JavaScriptViewModel GetViewModel()
        {
            var script = this.BuildScriptTag();

            return new JavaScriptViewModel()
            {
                Description = this.Description,
                Position = this.Position,
                JavaScriptCode = script
            };
        }

        public virtual string BuildScriptTag()
        {
            throw new NotImplementedException();
        }

        public virtual void PlaceScriptInDocument(Page page, string script)
        {
            if (this.Position == Models.EmbedPosition.Head)
            {
                page.Header.Controls.Add(new LiteralControl(script));
            }

            if (this.Position == Models.EmbedPosition.BeforeBodyEndTag)
            {

                /* There is literal control in the FrontendMVC.aspx which is used as a mark where scripts should be placed.*/

                int insertAt = page.Controls.Count - 1;
                MasterPage master = null;
                bool hasLiteral = false;
                for (int i = page.Controls.Count - 1; i >= 0; i--)
                {
                    if (page.Controls[i] is LiteralControl)
                    {
                        insertAt = i;
                        hasLiteral = true;
                        break;
                    }
                    else if (page.Controls[i] is MasterPage)
                    {
                        master = page.Controls[i] as MasterPage;
                        break;
                    }
                }

                if (master != null)
                {
                    for (int i = master.Controls.Count - 1; i >= 0; i--)
                    {
                        if (master.Controls[i] is LiteralControl)
                        {
                            insertAt = i;
                            hasLiteral = true;
                            break;
                        }
                    }
                }

                if (hasLiteral)
                {
                    if (master != null)
                        master.Controls.AddAt(insertAt, new LiteralControl(script));
                    else
                        page.Controls.AddAt(insertAt, new LiteralControl(script));
                }
                else
                {
                    if (page.Form != null)
                        page.Form.Controls.Add(new LiteralControl(script));
                    else
                        page.Controls.Add(new LiteralControl(script));
                }
            }
        }
    }
}
