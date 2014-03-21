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
    using Tree = Union<Leaf, Node>;
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
            BenchmarkSumTree();
            BenchmarkSumTreeCached();
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

        [Test]
        public void BenchmarkSumTree()
        {
            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                Tree t = new Node
                (
                    value: 0,
                    left: new Node
                    (
                        value: 1,
                        left: new Node
                        (
                            value: 2,
                            left: Node.Leaf,
                            right: Node.Leaf
                        ),
                        right: new Node
                        (
                            value: 3,
                            left: Node.Leaf,
                            right: Node.Leaf
                        )
                    ),
                    right: new Node
                    (
                        value: 4,
                        left: Node.Leaf,
                        right: Node.Leaf
                    )
                );

                var resultSumTree = UnionTests.SumTree(t);
            }
        }

        [Test]
        public void BenchmarkSumTreeCached()
        {
            Tree t = new Node
            (
                value: 0,
                left: new Node
                (
                    value: 1,
                    left: new Node
                    (
                        value: 2,
                        left: Node.Leaf,
                        right: Node.Leaf
                    ),
                    right: new Node
                    (
                        value: 3,
                        left: Node.Leaf,
                        right: Node.Leaf
                    )
                ),
                right: new Node
                (
                    value: 4,
                    left: Node.Leaf,
                    right: Node.Leaf
                )
            );

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var resultSumTree = UnionTests.SumTree(t);
            }
        }
    }
}
