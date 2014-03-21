using Functional.Union;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Union.Tests;

namespace UnionTests
{
    [TestFixture]
    public class BenchmarkUnion
    {
        Union<int, double> union;

        [Test]
        public void _Jit()
        {
            int tmp = BenchmarkSettings.Loops;
            BenchmarkSettings.Loops = 1;
            BenchmarkCreate();
            BenchmarkValeOr_Present();
            BenchmarkValeOr_NotPresent();
            BenchmarkValeOrFn_Present();
            BenchmarkValeOrFn_NotPresent();
            BenchmarkMatch();
            BenchmarkMatchCached();
            BenchmarkMatchResult();
            BenchmarkMatchResultCached();
            BenchmarkSettings.Loops = tmp;
        }

        [Test]
        public void BenchmarkCreate()
        {
            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                union = i;
            }
        }

        [Test]
        public void BenchmarkValeOr_Present()
        {
            Union<int, double> u = 1;

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var s = u.ValueOr(2);
            }
        }

        [Test]
        public void BenchmarkValeOr_NotPresent()
        {
            Union<int, double> u = 1;

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var s = u.ValueOr(2.0);
            }
        }

        [Test]
        public void BenchmarkValeOrFn_Present()
        {
            Union<int, double> u = 1;

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var s = u.ValueOr(() => 2);
            }
        }

        [Test]
        public void BenchmarkValeOrFn_NotPresent()
        {
            Union<int, double> u = 1;

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var s = u.ValueOr(() => 2.0);
            }
        }

        [Test]
        public void BenchmarkMatch()
        {
            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                Union<int, double> u = i;
                u.Match(Else: () => { });
            }
        }

        [Test]
        public void BenchmarkMatchCached()
        {
            Union<int, double> u = 1;

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                u.Match(Else: () => { });
            }
        }

        [Test]
        public void BenchmarkMatchResult()
        {
            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                Union<int, double> u = i;
                var s = u.Match(
                    Match1: x => x,
                    Else: () => 1);
            }
        }

        [Test]
        public void BenchmarkMatchResultCached()
        {
            Union<int, double> u = 1;

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var s = u.Match(
                    Match1: x => x,
                    Else: () => 1);
            }
        }
    }
}
