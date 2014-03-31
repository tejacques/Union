using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Functional.Union;

namespace RefUnionTests
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

        public int Match()
        {
            switch(this.Tag)
            {
                case Union<int,double>.UnionTypes.Type1:
                    return this.union.ValueOr(0);
                case Union<int,double>.UnionTypes.Type2:
                    return (int)this.union.ValueOr(0.0d);
            }

            return 0;
        }
    }

    public class Tree : RefUnion<Tree, Tree.Leaf, Tree.Node>
    {
        public class Leaf
        {
            public static readonly Tree Tree = Tree.Create((Leaf)null);
        }
        public class Node
        {
            public int Value;
            public Tree Left;
            public Tree Right;

            public Node(int value, Tree left, Tree right)
            {
                Value = value;
                Left = left;
                Right = right;
            }
        }

        public static int Sum(Tree tree)
        {
            return tree.Match(
                (Leaf l) => 0,
                (Node n) => n.Value + Sum(n.Left) + Sum(n.Right));
        }

        public int SumTree()
        {
            return this.Match(
                (Leaf l) => 0,
                (Node n) => n.Value + n.Left.SumTree() + n.Right.SumTree());
        }

        public int SumTreeDirect()
        {
            switch (this.Tag)
            {
                case Union<Leaf, Node>.UnionTypes.Type1:
                    return 0;
                case Union<Leaf, Node>.UnionTypes.Type2:
                    var n = this.ValueOr((Node)null);
                    return n.Value + n.Left.SumTreeDirect() + n.Right.SumTreeDirect();
            }

            return 0;
        }
    }

    [TestFixture]
    public class RefUnionTests
    {
        [Test]
        public void TestCreateUnion()
        {
            UnionIntDouble u = UnionIntDouble.Create(1);

            var s = u.Match(
                (int i) => i,
                (double d) => d,
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

        [Test]
        public void TestTree()
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

            var resultSumTree = Tree.Sum(t);
            Assert.AreEqual(10, resultSumTree);
        }
    }
}
