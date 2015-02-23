using System;
using System.Linq;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.News.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.News.Model;

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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.News)]
        [Author(FeatherTeams.Team2)]
        [Description("Verifies that the date selector - any time resolves the correct news.")]
        public void NewsWidget_VerifyDateSelectorAnyTimeOption()
        {
            var newsManager = NewsManager.GetManager();
            int newsCount = 3;

            var newsController = new NewsController();
            newsController.Model.SelectionMode = SelectionMode.FilteredItems;
            newsController.Model.SerializedAdditionalFilters = "{\"QueryItems\":[]}";

            DateTime publicationDate = DateTime.UtcNow.AddYears(-2);

            NewsItem modified = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "Boat").FirstOrDefault();
            newsManager.Lifecycle.PublishWithSpecificDate(modified, publicationDate);
            newsManager.SaveChanges();

            var items = newsController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.AreEqual(newsCount, items.Length, "The count of the news item is not as expected");

            Assert.IsTrue(items[0].Fields.Title.Equals(this.newsTitles[1]), "The news with this title was not found!");
            Assert.IsTrue(items[1].Fields.Title.Equals(this.newsTitles[0]), "The news with this title was not found!");
            Assert.IsTrue(items[2].Fields.Title.Equals(this.newsTitles[2]), "The news with this title was not found!");
        }

        /// <summary>
        /// News widget - test date selector -last 1 day
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.News)]
        [Author(FeatherTeams.Team2)]
        [Description("Verifies that the date selector -last 1 day resolves the correct news.")]
        public void NewsWidget_VerifyDateSelectorLastOneDayOption()
        {
            var newsManager = NewsManager.GetManager();
            int newsCount = 2;

            var newsController = new NewsController();
            newsController.Model.SelectionMode = SelectionMode.FilteredItems;
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

            DateTime publicationDate = DateTime.UtcNow.AddDays(-10);

            NewsItem modified = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "Boat").FirstOrDefault();
            newsManager.Lifecycle.PublishWithSpecificDate(modified, publicationDate);
            newsManager.SaveChanges();

            var items = newsController.Model.CreateListViewModel(null, 1).Items.ToArray();
            var newsItemsCount = items.Length;

            Assert.AreEqual(newsCount, newsItemsCount, "The count of the news item is not as expected");

            for (int i = 0; i < newsItemsCount; i++)
            {
                Assert.IsFalse(items[i].Fields.Title.Equals(this.newsTitles[2]), "The news with this title was found!");
            }

            Assert.IsTrue(items[0].Fields.Title.Equals(this.newsTitles[1]), "The news with this title was not found!");
            Assert.IsTrue(items[1].Fields.Title.Equals(this.newsTitles[0]), "The news with this title was not found!");
        }

        /// <summary>
        /// News widget - test date selector -last 1 week
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.News)]
        [Author(FeatherTeams.Team2)]
        [Description("Verifies that the date selector -last 1 week resolves the correct news.")]
        public void NewsWidget_VerifyDateSelectorLastOneWeekOption()
        {
            var newsManager = NewsManager.GetManager();
            int newsCount = 2;

            var newsController = new NewsController();
            newsController.Model.SelectionMode = SelectionMode.FilteredItems;
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

            DateTime publicationDate = DateTime.UtcNow.AddDays(-10);

            NewsItem modified = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "Boat").FirstOrDefault();
            newsManager.Lifecycle.PublishWithSpecificDate(modified, publicationDate);
            newsManager.SaveChanges();

            var items = newsController.Model.CreateListViewModel(null, 1).Items.ToArray();
            var newsItemsCount = items.Length;

            Assert.AreEqual(newsCount, newsItemsCount, "The count of the news item is not as expected");

            for (int i = 0; i < newsItemsCount; i++)
            {
                Assert.IsFalse(items[i].Fields.Title.Equals(this.newsTitles[2]), "The news with this title was found!");
            }

            Assert.IsTrue(items[0].Fields.Title.Equals(this.newsTitles[1]), "The news with this title was not found!");
            Assert.IsTrue(items[1].Fields.Title.Equals(this.newsTitles[0]), "The news with this title was not found!");
        }

        /// <summary>
        /// News widget - test date selector -last 1 month
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.News)]
        [Author(FeatherTeams.Team2)]
        [Description("Verifies that the date selector -last 1 month resolves the correct news.")]
        public void NewsWidget_VerifyDateSelectorLastOneMonthOption()
        {
            var newsManager = NewsManager.GetManager();
            int newsCount = 2;

            var newsController = new NewsController();
            newsController.Model.SelectionMode = SelectionMode.FilteredItems;
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

            DateTime publicationDate = DateTime.UtcNow.AddMonths(-2);

            NewsItem modified = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "Boat").FirstOrDefault();
            newsManager.Lifecycle.PublishWithSpecificDate(modified, publicationDate);
            newsManager.SaveChanges();

            var items = newsController.Model.CreateListViewModel(null, 1).Items.ToArray();
            var newsItemsCount = items.Length;

            Assert.AreEqual(newsCount, newsItemsCount, "The count of the news item is not as expected");

            for (int i = 0; i < newsItemsCount; i++)
            {
                Assert.IsFalse(items[i].Fields.Title.Equals(this.newsTitles[2]), "The news with this title was found!");
            }

            Assert.IsTrue(items[0].Fields.Title.Equals(this.newsTitles[1]), "The news with this title was not found!");
            Assert.IsTrue(items[1].Fields.Title.Equals(this.newsTitles[0]), "The news with this title was not found!");
        }

        /// <summary>
        /// News widget - test date selector -last 1 year
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.News)]
        [Author(FeatherTeams.Team2)]
        [Description("Verifies that the date selector -last 1 year resolves the correct news.")]
        public void NewsWidget_VerifyDateSelectorLastOneYearOption()
        {
            var newsManager = NewsManager.GetManager();
            int newsCount = 2;

            var newsController = new NewsController();
            newsController.Model.SelectionMode = SelectionMode.FilteredItems;
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

            DateTime publicationDate = DateTime.UtcNow.AddYears(-2);

            NewsItem modified = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "Boat").FirstOrDefault();
            newsManager.Lifecycle.PublishWithSpecificDate(modified, publicationDate);
            newsManager.SaveChanges();

            var items = newsController.Model.CreateListViewModel(null, 1).Items.ToArray();
            var newsItemsCount = items.Length;

            Assert.AreEqual(newsCount, newsItemsCount, "The count of the news item is not as expected");

            for (int i = 0; i < newsItemsCount; i++)
            {
                Assert.IsFalse(items[i].Fields.Title.Equals(this.newsTitles[2]), "The news with this title was found!");
            }

            Assert.IsTrue(items[0].Fields.Title.Equals(this.newsTitles[1]), "The news with this title was not found!");
            Assert.IsTrue(items[1].Fields.Title.Equals(this.newsTitles[0]), "The news with this title was not found!");
        }
        
        /// <summary>
        /// News widget - test date selector - custom range
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.News)]
        [Author(FeatherTeams.Team2)]
        [Description("Verifies that the date selector - custom range resolves the correct news.")]
        public void NewsWidget_VerifyDateSelectorCustomRangeOption()
        {
            var newsManager = NewsManager.GetManager();
            int newsCount = 2;
            DateTime publicationDateOld = new DateTime(2014, 10, 14, 12, 00, 00);
            DateTime publicationDateNew = new DateTime(2014, 10, 23, 12, 00, 00);

            var newsController = new NewsController();
            newsController.Model.SelectionMode = SelectionMode.FilteredItems;
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

            NewsItem modifiedBoat = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "Boat").FirstOrDefault();
            newsManager.Lifecycle.PublishWithSpecificDate(modifiedBoat, publicationDateOld);

            NewsItem modifiedCat = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "Cat").FirstOrDefault();
            newsManager.Lifecycle.PublishWithSpecificDate(modifiedCat, publicationDateNew);

            NewsItem modifiedAngel = newsManager.GetNewsItems().Where<NewsItem>(ni => ni.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master && ni.Title == "Angel").FirstOrDefault();
            newsManager.Lifecycle.PublishWithSpecificDate(modifiedAngel, publicationDateNew);
            newsManager.SaveChanges();

            var items = newsController.Model.CreateListViewModel(null, 1).Items.ToArray();
            var newsItemsCount = items.Length;

            Assert.AreEqual(newsCount, newsItemsCount, "The count of the news item is not as expected");

            for (int i = 0; i < newsItemsCount; i++)
            {
                Assert.IsFalse(items[i].Fields.Title.Equals(this.newsTitles[2]), "The news with this title was found!");
            }

            Assert.IsTrue(items[0].Fields.Title.Equals(this.newsTitles[0]), "The news with this title was not found!");
            Assert.IsTrue(items[1].Fields.Title.Equals(this.newsTitles[1]), "The news with this title was not found!");
        }

        /// <summary>
        /// News widget - test date selector - custom range not existing
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author(FeatherTeams.Team2)]
        [Description("Verifies that the date selector - not existing custom range resolves the correct news.")]
        public void NewsWidget_VerifyDateSelectorCustomRangeOptionNotExisting()
        {
            int newsCount = 0;

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NewsController).FullName;
            var newsController = new NewsController();
            newsController.Model.SelectionMode = SelectionMode.FilteredItems;
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

            var newsItemsCount = newsController.Model.CreateListViewModel(null, 1).Items.Count();
            Assert.AreEqual(newsCount, newsItemsCount, "The count of the news item is not as expected");
        }

        #region Fields and constants

        private string[] newsTitles = { "Cat", "Angel", "Boat" };

        #endregion
    }
}
