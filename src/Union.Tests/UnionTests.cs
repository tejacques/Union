using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Functional.Union;

namespace UnionTests
{
    using Tree = Union<Leaf, Node>;
    public struct Leaf
    {
        
    }
    public class Node
    {
        public static readonly Tree Leaf = default(Leaf);
        public int Value;
        public Tree Left;
        public Tree Right;

        public Node(int value, Tree left, Tree right)
        {
            this.Value = value;
            this.Left = left;
            this.Right = right;
        }
    }

    [TestFixture]
    public class UnionTests
    {
        [Test]
        public void TestCreateUnion()
        {
            Union<int, double> u = 1;

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

        public static int SumTree(Tree tree)
        {
            return tree.Match(
                (Leaf l) => 0,
                (Node n) => n.Value + SumTree(n.Left) + SumTree(n.Right));
        }

        [Test]
        public void TestTree()
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

            var resultSumTree = SumTree(t);
            Assert.AreEqual(10, resultSumTree);
        }
    }
}
