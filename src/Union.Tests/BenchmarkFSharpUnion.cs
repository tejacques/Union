using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FSharpUnionExamples;
using Microsoft.FSharp.Core;

namespace Union.Tests
{
    [TestFixture]
    public class BenchmarkFSharpUnion
    {

        [Test]
        public void _Jit()
        {
            var tmp = BenchmarkSettings.Loops;
            BenchmarkSettings.Loops = 1;
            BenchmarkUnionFSharp();
            BenchmarkUnionFSharpNatural();
            BenchmarkUnionFSharpCached();
            BenchmarkUnionFSharpWithLambda();
            BenchmarkUnionFSharpWithLambdaCached();
            BenchmarkSettings.Loops = tmp;
        }

        [Test]
        public void BenchmarkUnionFSharp()
        {
            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var u = new UnionMatch(i);
                var res = u.Match();
            }
        }

        [Test]
        public void BenchmarkUnionFSharpNatural()
        {
            var u = new UnionMatch();

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var res = u.Match(i);
            }
        }

        [Test]
        public void BenchmarkUnionFSharpCached()
        {
            var u = new UnionMatch();

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var res = u.Match();
            }
        }

        [Test]
        public void BenchmarkUnionFSharpWithLambda()
        {
            var I = FuncConvert.ToFSharpFunc((int i) => i);
            var D = FuncConvert.ToFSharpFunc((double d) => (int)d);

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var u = new UnionMatch(i);
                var res = u.Match(
                    I: I,
                    D: D);
            }
        }

        [Test]
        public void BenchmarkUnionFSharpWithLambdaCached()
        {
            var u = new UnionMatch();

            var I = FuncConvert.ToFSharpFunc((int i) => i);
            var D = FuncConvert.ToFSharpFunc((double d) => (int)d);

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var res = u.Match(
                    I: I,
                    D: D);
            }
        }
    }
}
