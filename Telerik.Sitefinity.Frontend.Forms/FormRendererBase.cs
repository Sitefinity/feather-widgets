using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Frontend.Forms
{
    /// <summary>
    /// This class provides sample implementation for <see cref="IFormRenderer"/> for creating form markup.
    /// </summary>
    public abstract class FormRendererBase : IFormRenderer
    {
        /// <inheritDoc/>
        public abstract void Render(StreamWriter writer, FormDescription form);

        /// <summary>
        /// Builds a single field markup.
        /// </summary>
        protected abstract string GetFieldMarkup(Control controlInstance, Guid controlDataId);

        /// <summary>
        /// Iterates trough all fields and returns their markup.
        /// </summary>
        protected string GetFieldsMarkup(string placeholderId, IEnumerable<ControlData> loadedControls)
        {
            var children = this.BuildControls(placeholderId, loadedControls, FormsManager.GetManager());

            using (var writer = new StringWriter())
            {
                using (var htmlWriter = new HtmlTextWriter(writer))
                {
                    foreach (var child in children)
                    {
                        child.RenderControl(htmlWriter);
                    }
                }

                return writer.ToString();
            }
        }

        private List<Control> BuildControls(string placeholderId, IEnumerable<ControlData> loadedControls, FormsManager manager)
        {
            List<ControlData> relevantControls = new List<ControlData>();
            List<ControlData> notRelevantControls = new List<ControlData>();
            Dictionary<string, string> placeholders = new Dictionary<string, string>();
            var formsContextCounter = SystemManager.CurrentHttpContext?.Items[FormController.FormsCounter];
            if (formsContextCounter != null && (int)formsContextCounter > 1)
            {
                loadedControls.ToList()
                .ForEach(ctrlData =>
                {
                    if (ctrlData.IsLayoutControl)
                        ctrlData.PlaceHolders.ToList().ForEach(plh => placeholders.Add(plh, plh + "_" + formsContextCounter.ToString()));
                });
            }

            foreach (var controlData in loadedControls)
            {
                var isChanged = placeholders.Any(plh => plh.Key == controlData.PlaceHolder && placeholderId == plh.Value);
                if (controlData.PlaceHolder == placeholderId || isChanged)
                    relevantControls.Add(controlData);
                else
                    notRelevantControls.Add(controlData);
            }

            var children = new List<Control>();

            var siblingId = Guid.Empty;
            while (relevantControls.Count > 0)
            {
                var currentControl = relevantControls.FirstOrDefault(c => c.SiblingId == siblingId);
                if (currentControl == null)
                    break;

                relevantControls.Remove(currentControl);
                siblingId = currentControl.Id;

                var controlInstance = manager.LoadControl(currentControl);
                if (currentControl.IsLayoutControl)
                {
                    var layoutControl = (LayoutControl)controlInstance;
                    foreach (var childPlaceholder in layoutControl.Placeholders)
                    {
                        var childControls = this.BuildControls(childPlaceholder.ID, notRelevantControls, manager);

                        if (formsContextCounter != null && (int)formsContextCounter > 1)
                        {
                            childPlaceholder.ID = string.Format(childPlaceholder.ID + "_" + formsContextCounter.ToString());
                        }

                        foreach (var childControl in childControls)
                        {
                            childPlaceholder.Controls.Add(childControl);
                        }
                    }

                    children.Add(layoutControl);
                }
                else
                {
                    var literal = this.GetFieldMarkup(controlInstance, currentControl.Id);
                    children.Add(new LiteralControl(literal));
                }
            }

            return children;
        }
    }
}