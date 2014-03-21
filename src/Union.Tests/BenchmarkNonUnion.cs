using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Union.Tests
{
    public interface Tree
    {
        int SumTree();
    }
    public class Leaf : Tree
    {
        public static readonly Leaf Tree = (Leaf)null;
        public int SumTree()
        {
            return 0;
        }
    }

    public class Node : Tree
    {
        public int Value;
        public Tree Left;
        public Tree Right;

        public Node(int value, Tree left, Tree right)
        {
            this.Value = value;
            this.Left = left;
            this.Right = right;
        }

        public int SumTree()
        {
            var result = this.Value;
            if (null != this.Left)
            {
                result += this.Left.SumTree();
            }
            if (null != this.Right)
            {
                result += this.Right.SumTree();
            }

            return result;
        }
    }

    public class Node2
    {
        public int Value;
        public Node2 Left;
        public Node2 Right;

        public Node2(int value, Node2 left, Node2 right)
        {
            this.Value = value;
            this.Left = left;
            this.Right = right;
        }

        public int SumTree()
        {
            var result = this.Value;
            if (null != this.Left)
            {
                result += this.Left.SumTree();
            }
            if (null != this.Right)
            {
                result += this.Right.SumTree();
            }

            return result;
        }
    }

    [TestFixture]
    public class BenchmarkNonUnion
    {
        [Test]
        public void _Jit()
        {
            var tmp = BenchmarkSettings.Loops;
            BenchmarkSettings.Loops = 1;
            BenchmarkSumTree();
            BenchmarkSumTreeCached();
            BenchmarkSumTreeNode();
            BenchmarkSumTreeNodeCached();
            BenchmarkSettings.Loops = tmp;
        }

        [Test]
        public void BenchmarkSumTree()
        {
            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                Tree n = new Node(
                    0,
                    new Node(
                        1,
                        new Node(
                            2,
                            Leaf.Tree,
                            Leaf.Tree
                        ),
                        new Node(
                            3,
                            Leaf.Tree,
                            Leaf.Tree
                        )
                    ),
                    new Node(
                        4,
                        Leaf.Tree,
                        Leaf.Tree
                    )
                );

                var resultSumTree = n.SumTree(); // = 10
            }
        }

        [Test]
        public void BenchmarkSumTreeCached()
        {
            Tree n = new Node(
                0,
                new Node(
                    1,
                    new Node(
                        2,
                        Leaf.Tree,
                        Leaf.Tree
                    ),
                    new Node(
                        3,
                        Leaf.Tree,
                        Leaf.Tree
                    )
                ),
                new Node(
                    4,
                    Leaf.Tree,
                    Leaf.Tree
                )
            );

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var resultSumTree = n.SumTree(); // = 10
            }
        }

        [Test]
        public void BenchmarkSumTreeNode()
        {
            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                Node2 n = new Node2
                (
                    0,
                    new Node2(
                        1,
                        new Node2(
                            2,
                            null,
                            null
                        ),
                        new Node2(
                            3,
                            null,
                            null
                        )
                    ),
                    new Node2(
                        4,
                        null,
                        null
                    )
                );

                var resultSumTree = n.SumTree(); // = 10
            }
        }

        [Test]
        public void BenchmarkSumTreeNodeCached()
        {
            Node2 n = new Node2
            (
                0,
                new Node2(
                    1,
                    new Node2(
                        2,
                        null,
                        null
                    ),
                    new Node2(
                        3,
                        null,
                        null
                    )
                ),
                new Node2(
                    4,
                    null,
                    null
                )
            );

            for (int i = 0; i < BenchmarkSettings.Loops; i++)
            {
                var resultSumTree = n.SumTree(); // = 10
            }
        }
    }
}
