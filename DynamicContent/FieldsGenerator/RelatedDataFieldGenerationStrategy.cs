using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace DynamicContent.FieldsGenerator
{
    /// <summary>
    /// This class represents field generation strategy for related data dynamic fields.
    /// </summary>
    public class RelatedDataFieldGenerationStrategy : FieldGenerationStrategy
    {
        /// <inheritdoc/>
        public override bool GetFieldCondition(DynamicModuleField field)
        {
            var condition = base.GetFieldCondition(field)
                && field.FieldType == FieldType.RelatedData;

            return condition;
        }

        /// <inheritdoc/>
        public override string GetFieldMarkup(DynamicModuleField field)
        {
            var markup = string.Format(this.BuildRelatedDataFieldTemplate(field.FrontendWidgetTypeName, field.FrontendWidgetLabel, field.FieldNamespace, field.RelatedDataType, field.RelatedDataProvider, field.Name, field.CanSelectMultipleItems, false, false));

            return markup;
        }

        public string BuildRelatedDataFieldTemplate(string frontendWidgetTypeName, string frontendWidgetLabel, string parentTypeName, string childTypeName, string childTypeProviderName, string fieldName, bool isMasterView, bool trim = true, bool registerAssembly = true)
        {
            var result = String.Empty;
            var childType = TypeResolutionService.ResolveType(childTypeName, false);
            var frontendWidget = TypeResolutionService.ResolveType(frontendWidgetTypeName, false);

            var template = String.Empty;
            if (RelatedDataFieldGenerationStrategy.InlineControlValue.Equals(frontendWidgetTypeName))
            {
                var identifierField = this.GetRelatedTypeIdentifierField(childTypeName);
                template = this.GetInlineFieldTemplate(isMasterView, childType);
                result = String.Format(template, fieldName, identifierField, frontendWidgetLabel);
            }
            //else if (childType != null && frontendWidget != null)
            //{
            //    template = FieldTemplateBuilder.GetFieldTemplate(childType, frontendWidget);

            //    result = String.Format(template, frontendWidget.Name, fieldName, parentTypeName, frontendWidgetLabel, RelationDirection.Child, childTypeProviderName);

            //    if (registerAssembly)
            //    {
            //        var registeredAssembly = String.Format(FieldTemplateBuilder.relatedResourceTemplate, frontendWidget.Assembly.GetName().Name, frontendWidget.Namespace);
            //        if (typeof(MediaContentView).IsAssignableFrom(frontendWidget))
            //        {
            //            //Telerik.Sitefinity.Modules.Libraries.Web.UI namespace is required for MasterDefinition and DetailDefinition
            //            registeredAssembly = String.Concat(registeredAssembly, String.Format(FieldTemplateBuilder.relatedResourceTemplate, "Telerik.Sitefinity", "Telerik.Sitefinity.Modules.Libraries.Web.UI"));
            //        }
            //        else if (typeof(ContentView).IsAssignableFrom(frontendWidget))
            //        {
            //            //Telerik.Sitefinity.Web.UI.ContentUI namespace is required for ContentViewMasterDefinition and ContentViewDetailDefinition
            //            registeredAssembly = String.Concat(registeredAssembly, String.Format(FieldTemplateBuilder.relatedResourceTemplate, "Telerik.Sitefinity", "Telerik.Sitefinity.Web.UI.ContentUI"));
            //        }

            //        var relatedResourceComment = String.Format("<%--{0}--%>", Res.Get<ModuleEditorResources>().RelatedResourceComment);
            //        result = String.Concat(relatedResourceComment, registeredAssembly, result);
            //    }
            //}
            //else if (childType != null && frontendWidgetTypeName.StartsWith("~/"))
            //{
            //    template = FieldTemplateBuilder.relatedControlByVirtualPathTemplate;
            //    result = String.Format(template, frontendWidgetTypeName, fieldName, parentTypeName, RelationDirection.Child, frontendWidgetLabel);
            //}

            return result;
        }

        private string GetInlineFieldTemplate(bool isMasterView, Type childType)
        {
            var template = String.Empty;

            if (typeof(Image).IsAssignableFrom(childType))
            {
                if (isMasterView)
                {
                    template = RelatedDataFieldGenerationStrategy.inlineImageListItem;
                }
                else
                {
                    template = RelatedDataFieldGenerationStrategy.inlineSingleImageItem;
                }
            }
            else if (typeof(Video).IsAssignableFrom(childType))
            {
                if (isMasterView)
                {
                    template = RelatedDataFieldGenerationStrategy.inlineVideoListItem;
                }
                else
                {
                    template = RelatedDataFieldGenerationStrategy.inlineSingleVideoItem;
                }
            }
            else if (typeof(Document).IsAssignableFrom(childType))
            {
                if (isMasterView)
                {
                    template = RelatedDataFieldGenerationStrategy.inlineDocumentListItem;
                }
                else
                {
                    template = RelatedDataFieldGenerationStrategy.inlineSingleDocumentItem;
                }
            }
            else if (typeof(PageNode).IsAssignableFrom(childType))
            {
                if (isMasterView)
                {
                    template = RelatedDataFieldGenerationStrategy.inlinePageList;
                }
                else
                {
                    template = RelatedDataFieldGenerationStrategy.inlineSinglePage;
                }
            }
            else
            {
                if (isMasterView)
                {
                    template = RelatedDataFieldGenerationStrategy.inlineListItem;
                }
                else
                {
                    template = RelatedDataFieldGenerationStrategy.inlineSingleItem;
                }
            }
            return template;
        }

        private string GetRelatedTypeIdentifierField(string relatedTypeName)
        {
            if (typeof(PageNode).FullName.Equals(relatedTypeName) ||
                "Telerik.Sitefinity.Events.Model.Event".Equals(relatedTypeName) ||
                "Telerik.Sitefinity.Blogs.Model.BlogPost".Equals(relatedTypeName) ||
                "Telerik.Sitefinity.News.Model.NewsItem".Equals(relatedTypeName) ||
                "Telerik.Sitefinity.Libraries.Model.Image".Equals(relatedTypeName) ||
                "Telerik.Sitefinity.Libraries.Model.Video".Equals(relatedTypeName) ||
                "Telerik.Sitefinity.Libraries.Model.Document".Equals(relatedTypeName) ||
                TypeResolutionService.ResolveType("Telerik.Sitefinity.Ecommerce.Catalog.Model.Product", false).IsAssignableFrom(TypeResolutionService.ResolveType(relatedTypeName, false)))
            {
                return "Title";
            }
            else
            {
                return "Title";
                //var mbManager = ModuleBuilderManager.GetManager();

                //DynamicModuleType dynamicType = null;
                ////Try to get the dynamic module type.
                ////It is possible the type to be unregistered.
                //var dynamicModuleType = TypeResolutionService.ResolveType(relatedTypeName, false);

                //if (dynamicModuleType != null)
                //{
                //    dynamicType = mbManager.GetDynamicModuleType(dynamicModuleType);
                //}
                //else
                //{
                //    //This is only in the case when we import a module and relate unregistered type.
                //    var lastIndex = relatedTypeName.LastIndexOf(".");
                //    var typeName = relatedTypeName.Substring(lastIndex + 1);
                //    var typeNamespace = relatedTypeName.Substring(0, lastIndex);

                //    var moduleTypes = mbManager.GetDynamicModuleTypes().Where(t => t.TypeNamespace == typeNamespace && t.TypeName == typeName);
                //    dynamicType = moduleTypes.FirstOrDefault();

                //    if (dynamicType == null)
                //    {
                //        throw new Exception(String.Format("Selected related data type {0} doesn't exists", relatedTypeName));
                //    }
                //}

                //return dynamicType.MainShortTextFieldName;
            }
        }

        internal const string InlineControlValue = "inline";

        private const string inlineSingleItem =
@"
            <div class=""sfSnglRelatedItmWrp"">
                <h2 class=""sfrelatedItmTitle"">{2}</h2>
                <div class=""sfrelatedItmWrp"">
                    <a href='<%# Telerik.Sitefinity.RelatedData.RelatedDataExtensions.GetDefaultUrl(Eval(""{0}"")) %>'><%# Eval(""{0}.{1}"") %></a>
                </div>
            </div>";

        private const string inlineListItem = @"@Html.Sitefinity().RelatedDataInlineListField(((object) Model.Item).GetRelatedItems(""{0}"").ToList<IDataItem>(), ""{1}"", ""{2}"")";

        private const string inlineSinglePage =
@"
            <div class=""sfSnglRelatedItmWrp"">
                <h2 class=""sfrelatedItmTitle"">{2}</h2>
                <div class=""sfrelatedItmWrp"">
                    <a <%# (Eval(""{0}"") != null) && ((Telerik.Sitefinity.Pages.Model.PageNode)Eval(""{0}"")).OpenNewWindow ? ""target=\""_blank\"" "" : """" %>href='<%# Telerik.Sitefinity.RelatedData.RelatedDataExtensions.GetDefaultUrl(Eval(""{0}"")) %>'><%# Eval(""{0}.{1}"") %></a>
                </div>
            </div>";

        private const string inlinePageList =
@"
            <div class=""sfMultiRelatedItmsWrp"">
                <h2 class=""sfrelatedItmTitle"">{2}</h2>
                <ul class=""sfrelatedList sflist"">
                    <asp:Repeater runat=""server"" DataSource='<%# Eval(""{0}"") %>'>
                        <ItemTemplate>
                            <li class=""sfrelatedListItem sflistitem""><a <%# ((Telerik.Sitefinity.Pages.Model.PageNode)Container.DataItem).OpenNewWindow ? ""target=\""_blank\"" "" : """" %>href='<%# Telerik.Sitefinity.RelatedData.RelatedDataExtensions.GetDefaultUrl(Container.DataItem) %>'><%# Eval(""{1}"") %></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>";

        private const string inlineSingleImageItem =
@"
            <div class=""sfSnglRelatedItmWrp"">
                <h2 class=""sfrelatedItmTitle"">{2}</h2>
                <div class=""sfrelatedItmWrp"">
                    <img src='<%# Eval(""{0}.MediaUrl"")%>' alt='<%# Eval(""{0}.AlternativeText"")%>' title='<%# Eval(""{0}.{1}"") %>' />
                </div>
            </div>";

        private const string inlineImageListItem =
@"
            <div class=""sfMultiRelatedItmsWrp"">
                <h2 class=""sfrelatedItmTitle"">{2}</h2>
                <ul class=""sfrelatedList sflist"">
                    <asp:Repeater runat=""server"" DataSource='<%# Eval(""{0}"") %>'>
                        <ItemTemplate>
                            <li class=""sfrelatedListItem sflistitem"">
                                <a href='<%# Eval(""MediaUrl"") %>'>
                                    <img src='<%# Eval(""ThumbnailUrl"")%>' alt='<%# Eval(""AlternativeText"")%>' title='<%# Eval(""{1}"") %>' />
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>";

        private const string inlineSingleVideoItem =
@"
            <div class=""sfSnglRelatedItmWrp"">
                <h2 class=""sfrelatedItmTitle"">{2}</h2>
                <div class=""sfrelatedItmWrp"">
                    <video controls>
                        <source  src='<%# Eval(""{0}.MediaUrl"")%>'>
                    </video>
                </div>
            </div>";

        private const string inlineVideoListItem =
@"
            <div class=""sfMultiRelatedItmsWrp"">
                <h2 class=""sfrelatedItmTitle"">{2}</h2>
                <ul class=""sfrelatedList sflist"">
                    <asp:Repeater runat=""server"" DataSource='<%# Eval(""{0}"") %>'>
                        <ItemTemplate>
                            <li class=""sfrelatedListItem sflistitem"">
                                <video controls>
                                    <source  src='<%# Eval(""MediaUrl"")%>'>
                                </video>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>";

        private const string inlineSingleDocumentItem =
@"
            <div class=""sfSnglRelatedItmWrp"">
                <h2 class=""sfrelatedItmTitle"">{2}</h2>
                <div class=""sfrelatedItmWrp"">
                    <a href='<%# Eval(""{0}.MediaUrl"")%>'>
                        <%# Eval(""{0}.{1}"") %>
                    </a>
                </div>
            </div>";

        private const string inlineDocumentListItem =
@"
            <div class=""sfMultiRelatedItmsWrp"">
                <h2 class=""sfrelatedItmTitle"">{2}</h2>
                <ul class=""sfrelatedList sflist"">
                    <asp:Repeater runat=""server"" DataSource='<%# Eval(""{0}"") %>'>
                        <ItemTemplate>
                            <li class=""sfrelatedListItem sflistitem"">
                               <a href='<%# Eval(""MediaUrl"")%>'>
                                  <%# Eval(""{1}"") %>
                               </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>";
        //private const string fieldMarkupTempalte = @"@Html.Sitefinity().TaxonomyField((object)Model.Item.{2}, new Guid(""{0}""), ""{1}"", ""{2}"")";
    }
}
