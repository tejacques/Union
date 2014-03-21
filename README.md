Union
=====

What is it?
-----------

A Discriminated Union library for C#

Why was it made?
----------------

Discriminated unions are an incredibly useful concept used as a fundamental building block in functional languages. They allow for clear typesafe access to differing forms of input to a function. What you get is an elegant style of writing null-safe typed checked functions. What you lose out on is performance. In the range of 2x-3x with F# and 5-30x with this library for the overhead, depending on what you are doing.

Examples
--------

### Tree Traversal ###

#### CSharp Raw ####

``` csharp
public class Node
{
    public int Value;
    public Node Left;
    public Node Right;
    
    public Node(int value, Node left, Node right)
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

public class Program
{
    public static void Main()
    {
        Node n = new Node(
            0,
            new Node(
                1,
                new Node(
                    2,
                    null,
                    null
                ),
                new Node(
                    3,
                    null,
                    null
                )
            ),
            new Node(
                4,
                null,
                null
            )
        );
        
        var resultSumTree = n.SumTree(); // = 10
    }
}
```

#### CSharp Polymorphism ####

``` csharp
public interface Tree
{
    int SumTree();
}

public class Leaf : Tree
{
    public static readonly Leaf Tree = new Leaf();
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

public class Program
{
    public static void Main()
    {
        Tree t = new Node(
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
```

#### FSharp ####
``` fsharp
type Tree =
    | Leaf
    | Node of int * Tree * Tree

let rec sumTree tree =
    match tree with
    | Leaf -> 0
    | Node(value, left, right) ->
        value + sumTree(left) + sumTree(right)

let myTree = Node(
    0,
    Node(
        1,
        Node(
            2,
            Leaf,
            Leaf
        ),
        Node(
            3,
            Leaf,
            Leaf
        )
    ),
    Node(
        4,
        Leaf,
        Leaf
    )
)

let resultSumTree = sumTree myTree // = 10
```

#### CSharp with Unions ####
``` csharp
using Tree = Union<Leaf, Node>;
public class Leaf
{
    public static readonly Tree Tree = (Leaf)null;
}
public class Node
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
}

public class Program
{
    public static int SumTree(Tree tree)
    {
        return tree.Match(
            (Leaf l) => 0,
            (Node n) => n.Value + SumTree(n.Left) + SumTree(n.Right));
    }

    public static void Main()
    {
        Tree t = new Node(
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

        var resultSumTree = SumTree(t); // = 10
    }
}
```

#### CSharp with RefUnions ####
``` csharp
public class Tree : RefUnion<Tree, Leaf, Node>
{
    public int SumTree()
    {
        return this.Match(
            (Leaf l) => 0,
            (Node n) => n.Value + n.Left.SumTree() + n.Right.SumTree());
    }
}
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
        this.Value = value;
        this.Left = left;
        this.Right = right;
    }
}

public class Program
{
    public static void Main()
    {
        Tree t = Tree.Create(new Node(
            0,
            Tree.Create(new Node(
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
            )),
            Tree.Create(new Node(
                4,
                Leaf.Tree,
                Leaf.Tree
            ))
        ));

        var resultSumTree = t.SumTree(); // = 10
    }
}
```
