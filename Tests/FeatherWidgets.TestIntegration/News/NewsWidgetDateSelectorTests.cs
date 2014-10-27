using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using News.Mvc.Controllers;
using News.Mvc.Models;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Workflow;

namespace FeatherWidgets.TestIntegration.News
{
    /// <summary>
    /// This is a class with News tests.
    /// </summary>
    [TestFixture]
    [Description("This is a class with News tests for date selector.")]
    public class NewsWidgetDateSelectorTests
    {
        /// <summary>
        /// Set up method
        /// </summary>
        [SetUp]
        public void Setup()
        {
            for (int i = 0; i < this.newsTitles.Length; i++)
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().CreateNewsItem(this.newsTitles[i]);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.News().DeleteAllNews();
        }

        /// <summary>
        /// News widget - test date selector - any time
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("FeatherTeam")]
        [Description("Verifies that the date selector - any time resolves the correct news.")]
        public void NewsWidget_VerifyDateSelectorAnyTimeOption()
        {
            var newsManager = NewsManager.GetManager();
            int newsCount = 3;

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.Model.SelectionMode = NewsSelectionMode.FilteredItems;
            newsController.Model.SerializedAdditionalFilters = "{\"QueryItems\":[]}";

            mvcProxy.Settings = new ControllerSettings(newsController);

            DateTime publicationDate = DateTime.UtcNow.AddYears(-2);

            NewsItem modified = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "Boat").FirstOrDefault();
            newsManager.Lifecycle.PublishWithSpecificDate(modified, publicationDate);
            newsManager.SaveChanges();

            newsController.Index(null);

            Assert.AreEqual(newsCount, newsController.Model.Items.Count, "The count of the news item is not as expected");

            Assert.IsTrue(newsController.Model.Items[0].Title.Value.Equals(this.newsTitles[1]), "The news with this title was not found!");
            Assert.IsTrue(newsController.Model.Items[1].Title.Value.Equals(this.newsTitles[0]), "The news with this title was not found!");
            Assert.IsTrue(newsController.Model.Items[2].Title.Value.Equals(this.newsTitles[2]), "The news with this title was not found!");
        }

        /// <summary>
        /// News widget - test date selector -last 1 day
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("FeatherTeam")]
        [Description("Verifies that the date selector -last 1 day resolves the correct news.")]
        public void NewsWidget_VerifyDateSelectorLastOneDayOption()
        {
            var newsManager = NewsManager.GetManager();
            int newsCount = 2;

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.Model.SelectionMode = NewsSelectionMode.FilteredItems;
            newsController.Model.SerializedAdditionalFilters = @"{
				""QueryItems"": [
					{
						""IsGroup"":true,
						""Ordinal"":0,
						""Join"":""AND"",
						""ItemPath"":""_0"",
						""Value"":null,
						""Condition"":null,
						""Name"":""PublicationDate""
					},
					{
						""IsGroup"":false,
						""Ordinal"":0,
						""Join"":""AND"",
						""ItemPath"":""_0_0"",
						""Value"":""DateTime.UtcNow.AddDays(-1.0)"",
						""Condition"": {
							""FieldName"":""PublicationDate"",
							""FieldType"":""System.DateTime"",
							""Operator"":"">""
						},
						""Name"":""PublicationDate.DateTime.UtcNow.AddDays(-1.0)""
					}
				]
			}";

            mvcProxy.Settings = new ControllerSettings(newsController);

            DateTime publicationDate = DateTime.UtcNow.AddDays(-10);

            NewsItem modified = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "Boat").FirstOrDefault();
            newsManager.Lifecycle.PublishWithSpecificDate(modified, publicationDate);
            newsManager.SaveChanges();

            newsController.Index(null);

            var newsItemsCount = newsController.Model.Items.Count;

            Assert.AreEqual(newsCount, newsItemsCount, "The count of the news item is not as expected");

            for (int i = 0; i < newsItemsCount; i++)
            {
                Assert.IsFalse(newsController.Model.Items[i].Title.Value.Equals(this.newsTitles[2]), "The news with this title was found!");
            }

            Assert.IsTrue(newsController.Model.Items[0].Title.Value.Equals(this.newsTitles[1]), "The news with this title was not found!");
            Assert.IsTrue(newsController.Model.Items[1].Title.Value.Equals(this.newsTitles[0]), "The news with this title was not found!");
        }

        /// <summary>
        /// News widget - test date selector -last 1 week
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("FeatherTeam")]
        [Description("Verifies that the date selector -last 1 week resolves the correct news.")]
        public void NewsWidget_VerifyDateSelectorLastOneWeekOption()
        {
            var newsManager = NewsManager.GetManager();
            int newsCount = 2;

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.Model.SelectionMode = NewsSelectionMode.FilteredItems;
            newsController.Model.SerializedAdditionalFilters = @"{
				""QueryItems"": [
					{
						""IsGroup"":true,
						""Ordinal"":0,
						""Join"":""AND"",
						""ItemPath"":""_0"",
						""Value"":null,
						""Condition"":null,
						""Name"":""PublicationDate""
					},
					{
						""IsGroup"":false,
						""Ordinal"":0,
						""Join"":""AND"",
						""ItemPath"":""_0_0"",
						""Value"":""DateTime.UtcNow.AddDays(-7.0)"",
						""Condition"": {
							""FieldName"":""PublicationDate"",
							""FieldType"":""System.DateTime"",
							""Operator"":"">""
						},
						""Name"":""PublicationDate.DateTime.UtcNow.AddDays(-7.0)""
					}
				]
			}";

            mvcProxy.Settings = new ControllerSettings(newsController);

            DateTime publicationDate = DateTime.UtcNow.AddDays(-10);

            NewsItem modified = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "Boat").FirstOrDefault();
            newsManager.Lifecycle.PublishWithSpecificDate(modified, publicationDate);
            newsManager.SaveChanges();

            newsController.Index(null);

            var newsItemsCount = newsController.Model.Items.Count;

            Assert.AreEqual(newsCount, newsItemsCount, "The count of the news item is not as expected");

            for (int i = 0; i < newsItemsCount; i++)
            {
                Assert.IsFalse(newsController.Model.Items[i].Title.Value.Equals(this.newsTitles[2]), "The news with this title was found!");
            }

            Assert.IsTrue(newsController.Model.Items[0].Title.Value.Equals(this.newsTitles[1]), "The news with this title was not found!");
            Assert.IsTrue(newsController.Model.Items[1].Title.Value.Equals(this.newsTitles[0]), "The news with this title was not found!");
        }

        /// <summary>
        /// News widget - test date selector -last 1 month
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("FeatherTeam")]
        [Description("Verifies that the date selector -last 1 month resolves the correct news.")]
        public void NewsWidget_VerifyDateSelectorLastOneMonthOption()
        {
            var newsManager = NewsManager.GetManager();
            int newsCount = 2;

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.Model.SelectionMode = NewsSelectionMode.FilteredItems;
            newsController.Model.SerializedAdditionalFilters = @"{
				""QueryItems"": [
					{
						""IsGroup"":true,
						""Ordinal"":0,
						""Join"":""AND"",
						""ItemPath"":""_0"",
						""Value"":null,
						""Condition"":null,
						""Name"":""PublicationDate""
					},
					{
						""IsGroup"":false,
						""Ordinal"":0,
						""Join"":""AND"",
						""ItemPath"":""_0_0"",
						""Value"":""DateTime.UtcNow.AddMonths(-1)"",
						""Condition"": {
							""FieldName"":""PublicationDate"",
							""FieldType"":""System.DateTime"",
							""Operator"":"">""
						},
						""Name"":""PublicationDate.DateTime.UtcNow.AddMonths(-1)""
					}
				]
			}";

            mvcProxy.Settings = new ControllerSettings(newsController);

            DateTime publicationDate = DateTime.UtcNow.AddMonths(-2);

            NewsItem modified = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "Boat").FirstOrDefault();
            newsManager.Lifecycle.PublishWithSpecificDate(modified, publicationDate);
            newsManager.SaveChanges();

            newsController.Index(null);

            var newsItemsCount = newsController.Model.Items.Count;

            Assert.AreEqual(newsCount, newsItemsCount, "The count of the news item is not as expected");

            for (int i = 0; i < newsItemsCount; i++)
            {
                Assert.IsFalse(newsController.Model.Items[i].Title.Value.Equals(this.newsTitles[2]), "The news with this title was found!");
            }

            Assert.IsTrue(newsController.Model.Items[0].Title.Value.Equals(this.newsTitles[1]), "The news with this title was not found!");
            Assert.IsTrue(newsController.Model.Items[1].Title.Value.Equals(this.newsTitles[0]), "The news with this title was not found!");
        }

        /// <summary>
        /// News widget - test date selector -last 1 year
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("FeatherTeam")]
        [Description("Verifies that the date selector -last 1 year resolves the correct news.")]
        public void NewsWidget_VerifyDateSelectorLastOneYearOption()
        {
            var newsManager = NewsManager.GetManager();
            int newsCount = 2;

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.Model.SelectionMode = NewsSelectionMode.FilteredItems;
            newsController.Model.SerializedAdditionalFilters = @"{
				""QueryItems"": [
					{
						""IsGroup"":true,
						""Ordinal"":0,
						""Join"":""AND"",
						""ItemPath"":""_0"",
						""Value"":null,
						""Condition"":null,
						""Name"":""PublicationDate""
					},
					{
						""IsGroup"":false,
						""Ordinal"":0,
						""Join"":""AND"",
						""ItemPath"":""_0_0"",
						""Value"":""DateTime.UtcNow.AddYears(-1)"",
						""Condition"": {
							""FieldName"":""PublicationDate"",
							""FieldType"":""System.DateTime"",
							""Operator"":"">""
						},
						""Name"":""PublicationDate.DateTime.UtcNow.AddYears(-1)""
					}
				]
			}";

            mvcProxy.Settings = new ControllerSettings(newsController);

            DateTime publicationDate = DateTime.UtcNow.AddYears(-2);

            NewsItem modified = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "Boat").FirstOrDefault();
            newsManager.Lifecycle.PublishWithSpecificDate(modified, publicationDate);
            newsManager.SaveChanges();

            newsController.Index(null);

            var newsItemsCount = newsController.Model.Items.Count;

            Assert.AreEqual(newsCount, newsItemsCount, "The count of the news item is not as expected");

            for (int i = 0; i < newsItemsCount; i++)
            {
                Assert.IsFalse(newsController.Model.Items[i].Title.Value.Equals(this.newsTitles[2]), "The news with this title was found!");
            }

            Assert.IsTrue(newsController.Model.Items[0].Title.Value.Equals(this.newsTitles[1]), "The news with this title was not found!");
            Assert.IsTrue(newsController.Model.Items[1].Title.Value.Equals(this.newsTitles[0]), "The news with this title was not found!");
        }
        
        /// <summary>
        /// News widget - test date selector - custom range
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author("FeatherTeam")]
        [Description("Verifies that the date selector - custom range resolves the correct news.")]
        public void NewsWidget_VerifyDateSelectorCustomRangeOption()
        {
            var newsManager = NewsManager.GetManager();
            int newsCount = 2;
            DateTime publicationDateOld = new DateTime(2014, 10, 14, 12, 00, 00);
            DateTime publicationDateNew = new DateTime(2014, 10, 23, 12, 00, 00);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.Model.SelectionMode = NewsSelectionMode.FilteredItems;
            newsController.Model.SerializedAdditionalFilters = @"{
            ""QueryItems"":[
	            {
		            ""IsGroup"":true,
		            ""Ordinal"":0,
		            ""Join"":""AND"",
		            ""ItemPath"":""_0"",
		            ""Value"":null,
		            ""Condition"":null,
		            ""Name"":""PublicationDate""
	            },
	            {
		            ""IsGroup"":false,
		            ""Ordinal"":0,
		            ""Join"":""AND"",
		            ""ItemPath"":""_0_0"",
		            ""Value"":""Wed, 22 Oct 2014 21:00:00 GMT"",
		            ""Condition"":
			            {
				            ""FieldName"":""PublicationDate"",
				            ""FieldType"":""System.DateTime"",
				            ""Operator"":"">""
			            },
		            ""Name"":""PublicationDate.Wed, 22 Oct 2014 21:00:00 GMT""
	            },
	            {
		            ""IsGroup"":false,
		            ""Ordinal"":1,
		            ""Join"":""AND"",
		            ""ItemPath"":""_0_1"",
		            ""Value"":""Fri, 24 Oct 2014 21:00:00 GMT"",
		            ""Condition"":
			            {
				            ""FieldName"":""PublicationDate"",
				            ""FieldType"":""System.DateTime"",
				            ""Operator"":""<""
			            },
		            ""Name"":""PublicationDate.Fri, 24 Oct 2014 21:00:00 GMT""
	            }";

            mvcProxy.Settings = new ControllerSettings(newsController);

            NewsItem modifiedBoat = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "Boat").FirstOrDefault();
            newsManager.Lifecycle.PublishWithSpecificDate(modifiedBoat, publicationDateOld);

            NewsItem modifiedCat = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "Cat").FirstOrDefault();
            newsManager.Lifecycle.PublishWithSpecificDate(modifiedCat, publicationDateNew);

            NewsItem modifiedAngel = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "Angel").FirstOrDefault();
            newsManager.Lifecycle.PublishWithSpecificDate(modifiedAngel, publicationDateNew);
            newsManager.SaveChanges();

            newsController.Index(null);

            var newsItemsCount = newsController.Model.Items.Count;

            Assert.AreEqual(newsCount, newsItemsCount, "The count of the news item is not as expected");

            for (int i = 0; i < newsItemsCount; i++)
            {
                Assert.IsFalse(newsController.Model.Items[i].Title.Value.Equals(this.newsTitles[2]), "The news with this title was found!");
            }

            Assert.IsTrue(newsController.Model.Items[0].Title.Value.Equals(this.newsTitles[0]), "The news with this title was not found!");
            Assert.IsTrue(newsController.Model.Items[1].Title.Value.Equals(this.newsTitles[1]), "The news with this title was not found!");
        }

        #region Fields and constants

        private string[] newsTitles = { "Cat", "Angel", "Boat" };

        #endregion
    }
}
