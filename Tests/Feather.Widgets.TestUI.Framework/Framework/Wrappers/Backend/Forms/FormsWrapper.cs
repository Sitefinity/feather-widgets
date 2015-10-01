using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.Common.UnitTesting;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;

namespace Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Forms
{
    /// <summary>
    /// This is an entry point for FormsWrapper.
    /// </summary>
    public class FormsWrapper : BaseWrapper
    {
        /// <summary>
        /// Verify number of responses in backend
        /// </summary>
        public void VerifyNumberOfResponses(int expectedResponsesCount)
        {
            var actualResponsesCount = BAT.Wrappers().Backend().Forms().FormsResponseScreen().GetResponsesCountInGrid();
            Assert.AreEqual(expectedResponsesCount, actualResponsesCount, "The expected responses count differs from the actual response count.");
        }

        /// <summary>
        /// Verify response author name in backend
        /// </summary>
        public void VerifyResponseAuthorUsername(string expectedAuthorName)
        {
            var actualAuthorUsername = BAT.Wrappers().Backend().Forms().FormsResponseScreen().GetResponseAuthorUsername();
            Assert.AreEqual(expectedAuthorName, actualAuthorUsername, "Actual form response author differs from the expected one");
        }

        /// <summary>
        /// Verify response submit date in backend
        /// </summary>
        public void VerifyResponseSubmitDate()
        {
            var actualSubmitDate = BAT.Wrappers().Backend().Forms().FormsResponseScreen().GetResponseSubmitDate();
            Assert.IsTrue(actualSubmitDate.Day == DateTime.Now.Day && actualSubmitDate.Month == DateTime.Now.Month, "Actual submit date differs from the expected one");
        }

        /// <summary>
        /// Verify response submit date in backend
        /// </summary>
        public void VerifyResponseTextboxAnswer(string textBoxContent)
        {
            var actualTextboxAnswer = BAT.Wrappers().Backend().Forms().FormsResponseScreen().GetResponseTextboxAnswer();
            Assert.AreEqual(textBoxContent, actualTextboxAnswer, "Actual form response differs from the expected one");
        }
    }
}
