module TreeUnion

type Tree =
    | Leaf
    | Node of int * Tree * Tree

let rec sumTree tree =
    match tree with
    | Leaf -> 0
    | Node(value, left, right) ->
        value + sumTree(left) + sumTree(right)

let makeTree() =
    Node(0, Node(1, Node(2, Leaf, Leaf), Node(3, Leaf, Leaf)), Node(4, Leaf, Leaf))

let myTree = makeTree()

let resultSumTree() =
    sumTree myTree