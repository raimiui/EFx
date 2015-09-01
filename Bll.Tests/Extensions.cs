using System;
using EFx.Model;
using Moq;
using NUnit.Framework;
using EFx.IBll.Extensions;

namespace EFx.Bll.Tests
{
   [TestFixture]
    public class Extensionss3
    {
        [TestFixtureSetUp]
        public void BeforeTests()
        {
        }

        [TestFixtureTearDown]
        public void AfterTests()
        {
        }

       [TestCase(1,2)]
       [TestCase(null,6), Description("Labas ka tu nori?")]
        public void MyTest(int a, int b)
        {
           Assert.True(a < b);
        }
    }
}
namespace kfove
{
    [TestFixture]
    public class Extensionss3
    {
        [Test]
        public void MyTest(
            [Values(1)] int x
            )
        {
        }
    }
}
