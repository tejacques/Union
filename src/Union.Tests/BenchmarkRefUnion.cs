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
    public class BenchmarkRefUnion
    {
        public class UnionIntDouble : RefUnion<UnionIntDouble, int, double>
        {
            public override void Match(
                Action<int> Int = null,
                Action<double> Double = null,
                Action Else = null)
            {
                base.Match(Int, Double, Else);
            }
            public override TResult Match<TResult>(
                Func<int, TResult> Int = null,
                Func<double, TResult> Double = null,
                Func<TResult> Else = null)
            {
                return base.Match<TResult>(Int, Double, Else);
            }
        }

        UnionIntDouble union;
        int loops = 1000000;

        [Test]
        public void _Jit()
        {
            int tmp = loops;
            loops = 1;
            BenchmarkCreate();
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
                union = UnionIntDouble.Create(i);
            }
        }

        [Test]
        public void BenchmarkValeOr_Present()
        {
            var u = UnionIntDouble.Create(1);

            for (int i = 0; i < loops; i++)
            {
                var s = u.ValueOr(2);
            }
        }

        [Test]
        public void BenchmarkValeOr_NotPresent()
        {
            var u = UnionIntDouble.Create(1);

            for (int i = 0; i < loops; i++)
            {
                var s = u.ValueOr(2.0);
            }
        }

        [Test]
        public void BenchmarkValeOrFn_Present()
        {
            var u = UnionIntDouble.Create(1);

            for (int i = 0; i < loops; i++)
            {
                var s = u.ValueOr(() => 2);
            }
        }

        [Test]
        public void BenchmarkValeOrFn_NotPresent()
        {
            var u = UnionIntDouble.Create(1);

            for (int i = 0; i < loops; i++)
            {
                var s = u.ValueOr(() => 2.0);
            }
        }

        [Test]
        public void BenchmarkMatch()
        {
            var u = UnionIntDouble.Create(1);

            for (int i = 0; i < loops; i++)
            {
                u.Match(
                    Int: x => { },
                    Else: () => { });
            }
        }

        [Test]
        public void BenchmarkMatchResult()
        {
            var u = UnionIntDouble.Create(1);

            for (int i = 0; i < loops; i++)
            {
                var s = u.Match(
                    Int: x => x,
                    Else: () => i);
            }
        }
    }
}
