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
    public struct Leaf { }
    public class Node
    {
        public int Value;
        public Tree Left;
        public Tree Right;

        public Node()
        {
            Left = new Leaf();
            Right = new Leaf();
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
            {
                Value = 0,
                Left = new Node
                {
                    Value = 1,
                    Left = new Node
                    {
                        Value = 2
                    },
                    Right = new Node
                    {
                        Value = 3
                    }
                },
                Right = new Node
                {
                    Value = 4
                }
            };

            var resultSumTree = SumTree(t);
            Assert.AreEqual(10, resultSumTree);
        }
    }
}
