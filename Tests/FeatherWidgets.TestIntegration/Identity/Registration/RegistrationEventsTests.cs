using System;
using System.Threading;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Registration;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Security.Events;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUtilities.Utilities;

namespace FeatherWidgets.TestIntegration.Identity.Registration
{
    /// <summary>
    /// This is a test class with tests related to registration functionality of Registration widget.
    /// </summary>
    [TestFixture]
    [Description("This is a test class with tests related to registration functionality of Registration widget.")]
    public class RegistrationEventsTests
    {
        [SetUp]
        public void SetUp()
        {
            this.userRegisteredEventHandler = new SitefinityEventHandler<UserRegistered>((e) => this.eventRaised = true);
            this.isUserRegisteredEventRaised = new Func<bool>(() => this.eventRaised);
            EventHub.Subscribe<UserRegistered>(this.userRegisteredEventHandler);
            this.eventRaised = false;
        }

        [TearDown]
        public void TearDown()
        {
            EventHub.Unsubscribe<UserRegistered>(this.userRegisteredEventHandler);
            ServerOperations.Users().DeleteUserAndProfile(this.validModel.UserName);
            this.eventRaised = false;
        }

        /// <summary>
        /// Verifies that a user registered event is raised after successful registration.
        /// </summary>
        [Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.SitefinityTeam3)]
        [Description("Verifies that a user registered event is raised after successful registration.")]
        public void RegisterUser_OnSuccessfulRegistration_RaisesUserRegisteredEvent()
        {
            // act
            this.controller.Index(this.validModel);

            // assert
            WaitUtils.WaitOperationUntil(this.isUserRegisteredEventRaised, RegistrationEventsTests.EventRaisedTotalTimeout, null, RegistrationEventsTests.EventRaisedPollTimeout);
            Assert.IsTrue(this.eventRaised);
        }

        /// <summary>
        /// Verifies that a user registered event is not raised after failed registration.
        /// </summary>
        [Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.SitefinityTeam3)]
        [Description("Verifies that a user registered event is not raised after failed registration.")]
        public void RegisterUser_OnFailedRegistration_DoesNotRaiseUserRegisteredEvent()
        {
            // act
            this.controller.Index(this.invalidModel);

            // assert
            Thread.Sleep(RegistrationEventsTests.EventRaisedTotalTimeout);
            Assert.IsFalse(this.eventRaised);
        }

        private const int EventRaisedTotalTimeout = 6000;
        private const int EventRaisedPollTimeout = 1000;
        private bool eventRaised;
        private SitefinityEventHandler<UserRegistered> userRegisteredEventHandler;
        private RegistrationController controller = new RegistrationController();
        private Func<bool> isUserRegisteredEventRaised;
        private RegistrationViewModel validModel = new RegistrationViewModel()
        {
            UserName = "Dummy2",
            Email = "dummy2@dummy.com",
            Password = "admin@2",
            ReTypePassword = "admin@2"
        };

        private RegistrationViewModel invalidModel = new RegistrationViewModel()
        {
            UserName = "Dummy",
            Email = "invalidmail",
            Password = "a",
            ReTypePassword = "admi"
        };
    }
}