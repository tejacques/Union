using Functional.Union;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnionTests
{
    [TestFixture]
    public class BenchmarkUnion
    {
        Union<int, double> union;
        int loops = 1000000;

        [Test]
        public void _Jit()
        {
            int tmp = loops;
            loops = 1;
            BenchmarkCreate();
            BenchmarkValeOr_Present();
            BenchmarkValeOr_NotPresent();
            BenchmarkValeOrFn_Present();
            BenchmarkValeOrFn_NotPresent();
            BenchmarkMatch();
            BenchmarkMatchResult();
            loops = tmp;
        }

        [Test]
        public void BenchmarkLoop()
        {
            for(int i = 0; i < loops; i++)
            {
                int j = i;
            }
        }

        [Test]
        public void BenchmarkCreate()
        {
            for (int i = 0; i < loops; i++)
            {
                union = i;
            }
        }

        [Test]
        public void BenchmarkValeOr_Present()
        {
            Union<int, double> u = 1;

            for (int i = 0; i < loops; i++)
            {
                var s = u.ValueOr(2);
            }
        }

        [Test]
        public void BenchmarkValeOr_NotPresent()
        {
            Union<int, double> u = 1;

            for (int i = 0; i < loops; i++)
            {
                var s = u.ValueOr(2.0);
            }
        }

        [Test]
        public void BenchmarkValeOrFn_Present()
        {
            Union<int, double> u = 1;

            for (int i = 0; i < loops; i++)
            {
                var s = u.ValueOr(() => 2);
            }
        }

        [Test]
        public void BenchmarkValeOrFn_NotPresent()
        {
            Union<int, double> u = 1;

            for (int i = 0; i < loops; i++)
            {
                var s = u.ValueOr(() => 2.0);
            }
        }

        [Test]
        public void BenchmarkMatch()
        {
            Union<int, double> u = 1;

            for (int i = 0; i < loops; i++)
            {
                u.Match(Else: () => { });
            }
        }

        [Test]
        public void BenchmarkMatchResult()
        {
            Union<int, double> u = 1;

            for (int i = 0; i < loops; i++)
            {
                var s = u.Match(
                    Match1: x => x,
                    Else: () => 1);
            }
        }
    }
}
