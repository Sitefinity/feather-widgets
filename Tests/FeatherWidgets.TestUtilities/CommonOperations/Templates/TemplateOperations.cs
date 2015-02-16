using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.UI;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Workflow;

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

        /// <summary>
        /// Returns the template Id by given title 
        /// </summary>
        /// <param name="templateTitle">the template title</param>
        /// <returns></returns>
        public Guid GetTemplateIdByTitle(string templateTitle)
        {
            var pageManager = PageManager.GetManager();
            var template = pageManager.GetTemplates().Where(t => t.Title == templateTitle).FirstOrDefault();

            if (template != null)
            {
                return template.Id;
            }
            else
            {
                throw new ArgumentException("Template was not found");
            }
        }

        /// <summary>
        /// Generates Id for template based on the controls count.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <returns>The generated Id.</returns>
        public string GenerateUniqueControlIdForTemplate(TemplateDraft template)
        {
            int controlsCount = 0;

            if (template != null)
            {
                controlsCount = template.Controls.Count;
            }

            return string.Format(CultureInfo.InvariantCulture, "T" + controlsCount.ToString(CultureInfo.InvariantCulture).PadLeft(3, '0'));
        }

        /// <summary>
        /// Gets the Sitefinity web app project path.
        /// </summary>
        public string SfPath
        {
            get
            {
                return System.Web.Hosting.HostingEnvironment.MapPath("~/");
            }
        }

        /// <summary>
        /// Duplicates existing page template.
        /// </summary>
        /// <param name="templateTitle">The template title.</param>
        /// <param name="newTemplateName">The new template title.</param>
        /// <returns>The Id of the new template.</returns>
        public Guid DuplicatePageTemplate(string templateTitle, string newTemplateName)
        {
            Guid templateId = Guid.Empty;
            var fluent = App.WorkWith();
            Guid parentTemplateId = this.GetTemplateIdByTitle(templateTitle);
            var parentTemplate = fluent.Page().PageManager.GetTemplate(parentTemplateId);
            templateId = fluent.PageTemplate()
                               .CreateNew()
                               .Do(t =>
                               {
                                   t.Title = newTemplateName;
                                   t.Name = new Lstring(Regex.Replace(newTemplateName, @"[^\w\-\!\$\'\(\)\=\@\d_]+", string.Empty).ToLower());
                                   t.Description = newTemplateName + " descr";
                                   t.ParentTemplate = parentTemplate;
                                   t.ShowInNavigation = true;
                                   t.Framework = PageTemplateFramework.Hybrid;
                                   t.Category = SiteInitializer.CustomTemplatesCategoryId;
                                   t.Visible = true;
                               })
                               .SaveAndContinue()
                               .Get()
                               .Id;
            var pageManager = PageManager.GetManager();
            var template = pageManager.GetTemplates().Where(t => t.Id == templateId).SingleOrDefault();
            var master = pageManager.TemplatesLifecycle.Edit(template);
            pageManager.TemplatesLifecycle.Publish(master);
            pageManager.SaveChanges();

            return template.Id;
        }

        /// <summary>
        /// Gets the page templates count.
        /// </summary>
        /// <returns>Page templates count.</returns>
        public int GetTemplatesCount
        {
            get
            {
                PageManager pageManager = PageManager.GetManager();
                return pageManager.GetTemplates().Count();
            }
        }

        private Guid GetLastControlInPlaceHolderInTemplateId(TemplateDraft template, string placeHolder)
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
    }
}
