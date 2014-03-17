using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Functional.Union;

namespace UnionTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestCreateUnion()
        {
            Union<int, double> u = 1;

            var s = u.Match(
                Match1: i => i,
                Match2: d => d,
                Else: () => 0);

            Assert.AreEqual(1, s);
        }

        [Test]
        public void TestMatchExhaustive()
        {
            Union<int, double> u = 1;

            // Actions
            Assert.Throws(typeof(MatchNotExhaustiveException), () =>
            {
                u.Match();
            });

            Assert.Throws(typeof(MatchNotExhaustiveException), () =>
            {
                u.Match(
                    Match1: i => { });
            });

            Assert.Throws(typeof(MatchNotExhaustiveException), () =>
            {
                u.Match(
                    Match2: d => { });
            });

            Assert.DoesNotThrow(() =>
            {
                u.Match(
                    Match1: i => { },
                    Match2: d => { });
            });

            Assert.DoesNotThrow(() =>
            {
                u.Match(
                    Else: () => { });
            });

            // Funcs
            Assert.Throws(typeof(MatchNotExhaustiveException), () =>
            {
                var x = u.Match<int>();
            });

            Assert.Throws(typeof(MatchNotExhaustiveException), () =>
            {
                var x = u.Match(
                    Match1: i => i);
            });

            Assert.Throws(typeof(MatchNotExhaustiveException), () =>
            {
                var x = u.Match(
                    Match2: d => (int)d);
            });

            Assert.DoesNotThrow(() =>
            {
                u.Match(
                    Match1: i => i,
                    Match2: d => d);
            });

            Assert.DoesNotThrow(() =>
            {
                u.Match(
                    Else: () => 0);
            });
        }
    }
}
