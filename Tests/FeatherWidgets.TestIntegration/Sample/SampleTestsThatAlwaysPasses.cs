using System;
using MbUnit.Framework;

namespace FeatherWidgets.TestIntegration.Sample
{
    /// <summary>
    /// This is a sample class with a test that always passes.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly"), TestFixture]
    [Description("This is a sample class with a test that always passes.")]
    public class SampleTestsThatAlwaysPasses
    {
        /// <summary>
        /// Samples the test that always passes.
        /// </summary>
        [Test]
        [Category(TestCategories.Samples)]
        [Author("idimitrov")]
        public void SampleTestThatAlwaysPasses()
        {
            var expected = new Guid("28AABFCA-6FFF-49FD-96C1-B7C1023DAE7A");
            var actual = new Guid("28AABFCA-6FFF-49FD-96C1-B7C1023DAE7A");
            Assert.AreEqual(expected, actual);
        }
    }
}
