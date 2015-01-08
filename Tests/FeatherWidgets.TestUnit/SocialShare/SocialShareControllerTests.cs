using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses.SocialShare;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.SocialShare.Mvc.Models;

namespace FeatherWidgets.TestUnit.SocialShare
{
    /// <summary>
    /// Tests methods for the SocialShareController
    /// </summary>
    [TestClass]
    public class SocialShareControllerTests
    {
        /// <summary>
        /// The create social share_ call the index action_ ensures the default view is returned.
        /// </summary>
        [TestMethod]
        [Owner("NPetrova")]
        [Description("Checks whether the Index action returns the default view.")]
        public void CreateSocialShare_CallTheIndexAction_EnsuresTheDefaultViewIsReturned()
        {
            // Arrange
            using (var controller = new DummySocialShareController())
            {
                // Act
                var view = controller.Index() as ViewResult;

                // Assert
                Assert.AreEqual("SocialShare", view.ViewName, "The view name is not correct.");
            }
        }

        /// <summary>
        /// The create social share_ call the index action_ ensures the changed view is returned.
        /// </summary>
        [TestMethod]
        [Owner("NPetrova")]
        [Description("Checks whether the Index action returns the changed view.")]
        public void CreateSocialShare_CallTheIndexAction_EnsuresTheChangedViewIsReturned()
        {
            // Arrange
            using (var controller = new DummySocialShareController())
            {
                controller.TemplateName = "SocialShareIconsWithText";

                // Act
                var view = controller.Index() as ViewResult;

                // Assert
                Assert.AreEqual("SocialShareIconsWithText", view.ViewName, "The view name is not correct.");
            }
        }

        /// <summary>
        /// Checks the properly set of social share button from controller to model.
        /// </summary>
        [TestMethod]
        [Owner("Manev")]
        [Description("Checks the properly set of social share button from controller to model.")]
        public void CheckProperlySet_OfSocialShareButton_FromControllerToModel()
        {
            var socialShareOptions = new List<SocialShareGroupMap>
                {
                    new SocialShareGroupMap(new List<SocialShareMap>
                        {
                            new SocialShareMap("Facebook", true),
                            new SocialShareMap("Twitter", true),
                            new SocialShareMap("GooglePlusOne", true),
                            new SocialShareMap("LinkedIn", true),
                            new SocialShareMap("SocialShareMode", false)
                        }),
                };

            // Arrange
            using (var controller = new DummySocialShareController(socialShareOptions))
            {
                // Act
                controller.Index();

                // Assert
                Assert.IsTrue(controller.Model.SocialButtons.Count == 4);
                
                foreach (SocialShareMap socialShareMap in socialShareOptions.SelectMany(s => s.Groups))
                {
                    var button = controller.Model.SocialButtons.FirstOrDefault(b => b.ButtonName == socialShareMap.Key);

                    if (!socialShareMap.IsChecked)
                    {
                        Assert.IsNull(button);
                    }
                    else
                    {
                        Assert.IsNotNull(button);
                    }
                }
            }
        }

        /// <summary>
        /// Checks the properly set_ of social share button_ passed from client.
        /// </summary>
        [TestMethod]
        [Owner("Manev")]
        [Description("Checks the properly set of social share buttons passed from client.")]
        public void CheckProperlySet_OfSocialShareButton_PassedFromClient()
        {
            string selectedSocialButtons = "[{\"__type\":\"Telerik.Sitefinity.Frontend.SocialShare.Mvc.Models.SocialShareGroupMap, Telerik.Sitefinity.Frontend.SocialShare\",\"Groups\":[{\"Key\":\"Facebook\",\"Label\":\"Facebook\",\"IsChecked\":true,\"$$hashKey\":\"object:15\"},{\"Key\":\"Twitter\",\"Label\":\"Twitter\",\"IsChecked\":false,\"$$hashKey\":\"object:16\"},{\"Key\":\"GooglePlusOne\",\"Label\":\"Google +\",\"IsChecked\":true,\"$$hashKey\":\"object:17\"},{\"Key\":\"StumbleUpon\",\"Label\":\"Stumble Upon\",\"IsChecked\":true,\"$$hashKey\":\"object:18\"},{\"Key\":\"GoogleBookmarks\",\"Label\":\"Google bookmarks\",\"IsChecked\":true,\"$$hashKey\":\"object:19\"}],\"$$hashKey\":\"object:9\"}}]";

            var socialShareOptions = new List<SocialShareGroupMap>
                {
                    new SocialShareGroupMap(new List<SocialShareMap>
                        {
                            new SocialShareMap("Facebook", true),
                            new SocialShareMap("GooglePlusOne", true),
                            new SocialShareMap("StumbleUpon", true),
                            new SocialShareMap("GoogleBookmarks", true)
                        }),
                };

            // Arrange
            using (var controller = new DummySocialShareController())
            {
                controller.SerializedSocialShareSectionMap = selectedSocialButtons;

                // Act
                controller.Index();

                // Assert
                Assert.IsTrue(controller.Model.SocialButtons.Count == 4);

                foreach (SocialShareMap socialShareMap in socialShareOptions.SelectMany(s => s.Groups))
                {
                    var button = controller.Model.SocialButtons.FirstOrDefault(b => b.ButtonName == socialShareMap.Key);

                    Assert.IsNotNull(button);
                }
            }
        }
    }
}
