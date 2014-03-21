namespace FSharpUnionExamples

type IntDoubleUnion =
    | Int of int
    | Double of double

type UnionMatch() = 
    member this.Value = Int(0)

    member this.Match(I: int -> int, D: double -> int) =
        match this.Value with
        | Int(i) -> I(i)
        | Double(d) -> D(d)

    member this.Match() = 
        match this.Value with
        | Int(i) -> i
        | Double(d) -> int(d)

    member this.Match(i: int) =
        this.Match(Int(i))

    member this.Match(d: double) =
        this.Match(Double(d))

    member this.Match(u: IntDoubleUnion) =
        match u with
        | Int(i) -> i
        | Double(d) -> int(d)

    new(i: int) as this = UnionMatch() then this.Value = Int(i)