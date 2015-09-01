using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFx.Bll;
using EFx.IBll;
using EFx.IDal;
using EFx.Model;
using Moq;
using NUnit.Framework;

namespace Bll.Tests.QuoteService
{
    [TestFixture]
    public class MergeHistoricalQuotesTests
    {
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
        }

        [SetUp]
        public void Setup()
        {
            var date1 = DateTime.Now;
            var date2 = date1.AddDays(1);
            var date3 = date1.AddDays(2);
        }

        [TearDown]
        public void TearDown()
        {
        }

        private static readonly DateTime Now = DateTime.Now;
        static readonly object[] HistoricalQuotes =
        {
            new [] 
            {
                new List<Quote>{new Quote {CreatedOn = Now, Open = 1, Close = 3}},
                new List<Quote>{new Quote {CreatedOn = Now, Open = 3, Close = 4}}
            }
        };

        //[Test]
        //[TestCaseSource("HistoricalQuotes")]
        //public void MergeHistoricalQuotes_MergesBidAndAskQuotesLists(List<Quote>[] quotes)
        //{
        //    var bidQuotes = quotes[0];
        //    var askQuotes = quotes[1];
        //    var quoteDaoMock = new Mock<IQuoteDao>();
        //    var appSettingsServiceMock = new Mock<IAppSettingsService>();
        //    var quoteService = new EFx.Bll.QuoteService(quoteDaoMock.Object, appSettingsServiceMock.Object);

        //    var mergedHistoricalQuotes = quoteService.MergeHistoricalQuotes(quotes[0], quotes[1]).ToArray();
            
        //    // Assert
        //    Assert.IsTrue(mergedHistoricalQuotes.Count() == 1);
        //    Assert.IsTrue(mergedHistoricalQuotes[0].CreatedOn == quotes[0][0].CreatedOn);
        //    Assert.IsTrue(mergedHistoricalQuotes[0].Bid == bidQuotes[0].Open);
        //    Assert.IsTrue(mergedHistoricalQuotes[0].Ask == askQuotes[0].Open);
        //}
    }
}
