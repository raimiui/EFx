using System;
using System.Collections.Generic;
using EFx.Model;
using NUnit.Framework;
using EFx.IBll.Extensions;

namespace EFx.Bll.Tests.Extensions
{
    [TestFixture]
    internal class AllInOneTrend
    {
        #region In trend

        [Test]
        [Category("List of 2 member")]
        public void Returns_true_when_list_is_in_Positive_trend_2_members()
        {
            IEnumerable<Quote> quotesInTrend = new List<Quote> { new Quote { Close = 1.1 }, new Quote { Close = 1.2 } };
            Assert.True(quotesInTrend.AllInOneTrend());
        }

        [Test]
        [Category("List of 2 member")]
        public void Returns_false_when_list_is_Negative_in_trend_2_members()
        {
            IEnumerable<Quote> quotesNotInTrend = new List<Quote> { new Quote { Close = 1.1 }, new Quote { Close = 1.0 } };
            Assert.True(quotesNotInTrend.AllInOneTrend());
        }
        
        [Test]
        [Category("List of 3 member")]
        public void Returns_true_when_list_is_in_Positive_trend_3_members()
        {
            IEnumerable<Quote> quotesInTrend = new List<Quote> { new Quote { Close = 1.1 }, new Quote { Close = 1.2 }, new Quote { Close = 12.0 } };
            Assert.True(quotesInTrend.AllInOneTrend());
        }
        
        [Test]
        [Category("List of 3 member")]
        public void Returns_false_when_list_is_Negative_in_trend_3_members()
        {
            IEnumerable<Quote> quotesNotInTrend = new List<Quote> { new Quote { Close = 1.1 }, new Quote { Close = 1.0 }, new Quote { Close = 0.2 } };
            Assert.True(quotesNotInTrend.AllInOneTrend());
        }

        #endregion In trend
        
        #region Not in trend

        [Test]
        [Category("List of 1 member")]
        public void Throws_exception_when_list_is_null()
        {
            IEnumerable<Quote> oneQuote = null;
            Assert.Throws<ArgumentNullException>(() => oneQuote.AllInOneTrend());
        }

        [Test]
        [Category("List of 1 member")]
        
        public void Returns_false_when_only_one_quote_in_list()
        {
            IEnumerable<Quote> oneQuote = new List<Quote> { new Quote { Close = 11D } };
            Assert.False(oneQuote.AllInOneTrend());
        }

        [Test]
        [Category("List of 3 member")]
        public void Returns_false_when_list_is_in_Positive_trend_2_members()
        {
            IEnumerable<Quote> quotesInTrend = new List<Quote> { new Quote { Close = 1.1 }, new Quote { Close = 1.2 }, new Quote { Close = 1.04 } };
            Assert.False(quotesInTrend.AllInOneTrend());
        }
        
        #endregion Not in trend
    }
}
