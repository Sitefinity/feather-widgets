using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.UI;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace FeatherWidgets.TestUtilities.CommonOperations.Templates
{
    public class TemplateOperations
    {
        /// <summary>
        /// Adds a control to a page template
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="control">The Control</param>
        /// <param name="placeHolder">The placeholder</param>
        /// <param name="caption">The caption</param>
        public void AddControlToTemplate(Guid templateId, Control control, string placeHolder, string caption)
        {
            var pageManager = PageManager.GetManager();
            using (new ElevatedModeRegion(pageManager))
            {
                var template = pageManager.GetTemplates().Where(t => t.Id == templateId).SingleOrDefault();

                if (template != null)
                {
                    var master = pageManager.TemplatesLifecycle.Edit(template);
                    var temp = pageManager.TemplatesLifecycle.CheckOut(master);

                    if (temp != null)
                    {
                        if (string.IsNullOrEmpty(control.ID))
                        {
                            control.ID = this.GenerateUniqueControlIdForTemplate(temp);
                        }

                        var templateControl = pageManager.CreateControl<TemplateDraftControl>(control, placeHolder);
                        templateControl.Caption = caption;
                        templateControl.SiblingId = this.GetLastControlInPlaceHolderInTemplateId(temp, placeHolder);

                        pageManager.SetControlDefaultPermissions(templateControl);

                        temp.Controls.Add(templateControl);

                        master = pageManager.TemplatesLifecycle.CheckIn(temp);
                        master.ApprovalWorkflowState.Value = "Published";
                        pageManager.TemplatesLifecycle.Publish(master);

                        pageManager.SaveChanges();
                    }
                }
            }
        }

        public Guid GetLastControlInPlaceHolderInTemplateId(TemplateDraft template, string placeHolder)
        {
            var id = Guid.Empty;
            TemplateDraftControl control;

            var controls = new List<TemplateDraftControl>(template.Controls.Where(c => c.PlaceHolder == placeHolder));

            while (controls.Count > 0)
            {
                control = controls.Where(c => c.SiblingId == id).SingleOrDefault();
                if (control != null)
                {
                    id = control.Id;

                    controls.Remove(control);
                }
            }

            return id;
        }

        public string GenerateUniqueControlIdForTemplate(TemplateDraft template)
        {
            int controlsCount = 0;

            if (template != null)
            {
                controlsCount = template.Controls.Count;
            }

            return string.Format(CultureInfo.InvariantCulture, "T" + controlsCount.ToString(CultureInfo.InvariantCulture).PadLeft(3, '0'));
        }

        public string SfPath
        {
            get
            {
                return System.Web.Hosting.HostingEnvironment.MapPath("~/");
            }
        }

        public Guid GetTemplateIdByTitle(string templateTitle)
        {
            var pageManager = PageManager.GetManager();
            var template = pageManager.GetTemplates().Where(t => t.Title.Contains(templateTitle)).FirstOrDefault();

            if (template != null)
            {
                return template.Id;
            }
            else
            {
                throw new ArgumentException("Template was not found");
            }
        }

        public void WaitForTemplatesCountToIncrease(int primaryCount, int increment)
        {
            PageManager pageManager = PageManager.GetManager();

            for (int i = 50; i > 0; --i)
            {
                if (pageManager.GetTemplates().Count() == primaryCount + increment)
                    break;

                Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }
        }
    }
}
