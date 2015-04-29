using System;
using System.Linq;
using System.Web.UI;
using System.Web.Hosting;

namespace Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.Models.JavaScript
{
    public class JavaScriptModel : IJavaScriptModel
    {
        /// <summary>
        /// Gets or sets the javascript code entered by the user.
        /// </summary>
        /// <value>The cusotm code.</value>
        public string CustomCode { get; set; }

        /// <summary>
        /// Gets or sets the URL of the file where the javascript is stored.
        /// </summary>
        /// <value>The file URL.</value>
        public string FileUrl { get; set; }

        /// <summary>
        /// Gets or sets the position in the document where the code will be inserted.
        /// </summary>
        /// <value>The embed position.</value>
        public EmbedPosition Position { get; set; }

        /// <summary>
        /// Gets or sets whether the widget will use entered by the user custom code or the url to a file.
        /// </summary>
        /// <value>The mode.</value>
        public ResourceMode Mode { get; set; }

        /// <summary>
        /// Gets or sets the description of the used code.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets the view model of the current model that will be displayed by the view.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Builds the script tag with url pointing to the script file or containig the script in its content.
        /// </summary>
        /// <returns></returns>
        public virtual string BuildScriptTag()
        {
            var scriptTag = string.Empty;
            if (this.Mode == ResourceMode.Reference && !string.IsNullOrEmpty(this.FileUrl))
            {
                var scriptUrl = this.FileUrl;
                if (scriptUrl.StartsWith("~/"))
				{
                    scriptUrl = HostingEnvironment.ApplicationVirtualPath + scriptUrl.Substring(1);
                    if (scriptUrl.StartsWith(@"//"))
                        scriptUrl = scriptUrl.Substring(1);
				}

				scriptTag = string.Format(@"<script type=""text/javascript"" src=""{0}""></script>", scriptUrl);
            }
            else if(this.Mode == ResourceMode.Inline && !string.IsNullOrEmpty(this.CustomCode))
            {
                scriptTag = string.Format(@"<script type=""text/javascript"">{0}</script>",
                                                    this.CustomCode);
            }

            return scriptTag;
        }

        /// <summary>
        /// Places the script before the end of the body.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="script">The script.</param>
        public virtual void PlaceScriptBeforeBodyEnd(Page page, string script)
        {
            /* There is a literal control in the FrontendMVC.aspx which is used as a mark where scripts should be placed.*/
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
