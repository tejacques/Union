using Functional.Union;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Union.Tests;
using RefUnionTests;

namespace RefUnionTests
{
    [TestFixture]
    public class BenchmarkRefUnion
    {
        UnionIntDouble union;

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
            BenchmarkMatchResult();
            BenchmarkMatchResultNoLambda();
            BenchmarkMatchResultNoLambdaCached();
            BenchmarkSumTree();
            BenchmarkSumTreeCached();
            BenchmarkSettings.Loops = tmp;
        }

        [Test]
        public void BenchmarkCreate()
        {
            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                union = UnionIntDouble.Create(i);
            }
        }

        [Test]
        public void BenchmarkValeOr_Present()
        {
            var u = UnionIntDouble.Create(1);

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var s = u.ValueOr(2);
            }
        }

        [Test]
        public void BenchmarkValeOr_NotPresent()
        {
            var u = UnionIntDouble.Create(1);

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var s = u.ValueOr(2.0);
            }
        }

        [Test]
        public void BenchmarkValeOrFn_Present()
        {
            var u = UnionIntDouble.Create(1);

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var s = u.ValueOr(() => 2);
            }
        }

        [Test]
        public void BenchmarkValeOrFn_NotPresent()
        {
            var u = UnionIntDouble.Create(1);

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
                var u = UnionIntDouble.Create(1);
                u.Match(
                    Int: x => { },
                    Else: () => { });
            }
        }

        [Test]
        public void BenchmarkMatchCached()
        {
            var u = UnionIntDouble.Create(1);

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                u.Match(
                    Int: x => { },
                    Else: () => { });
            }
        }

        [Test]
        public void BenchmarkMatchResult()
        {
            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var u = UnionIntDouble.Create(i);
                var s = u.Match(
                    Int: x => x,
                    Else: () => i);
            }
        }

        [Test]
        public void BenchmarkMatchResultCached()
        {
            var u = UnionIntDouble.Create(1);

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var s = u.Match(
                    Int: x => x,
                    Else: () => i);
            }
        }

        [Test]
        public void BenchmarkMatchResultNoLambda()
        {
            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var u = UnionIntDouble.Create(1);
                var s = u.Match();
            }
        }

        [Test]
        public void BenchmarkMatchResultNoLambdaCached()
        {
            var u = UnionIntDouble.Create(1);

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var s = u.Match();
            }
        }

        [Test]
        public void BenchmarkSumTree()
        {
            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                Tree t = Tree.Create(new Tree.Node
                (
                    value: 0,
                    left: Tree.Create(new Tree.Node
                    (
                        value: 1,
                        left: Tree.Create(new Tree.Node
                        (
                            value: 2,
                            left: Tree.Leaf.Tree,
                            right: Tree.Leaf.Tree
                        )),
                        right: Tree.Create(new Tree.Node
                        (
                            value: 3,
                            left: Tree.Leaf.Tree,
                            right: Tree.Leaf.Tree
                        ))
                    )),
                    right: Tree.Create(new Tree.Node
                    (
                        value: 4,
                        left: Tree.Leaf.Tree,
                        right: Tree.Leaf.Tree
                    ))
                ));

                var resultSumTree = t.SumTree();
            }
        }

        [Test]
        public void BenchmarkSumTreeCached()
        {
            Tree t = Tree.Create(new Tree.Node
            (
                value: 0,
                left: Tree.Create(new Tree.Node
                (
                    value: 1,
                    left: Tree.Create(new Tree.Node
                    (
                        value: 2,
                        left: Tree.Leaf.Tree,
                        right: Tree.Leaf.Tree
                    )),
                    right: Tree.Create(new Tree.Node
                    (
                        value: 3,
                        left: Tree.Leaf.Tree,
                        right: Tree.Leaf.Tree
                    ))
                )),
                right: Tree.Create(new Tree.Node
                (
                    value: 4,
                    left: Tree.Leaf.Tree,
                    right: Tree.Leaf.Tree
                ))
            ));

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var resultSumTree = t.SumTree();
            }
        }
    }
}
