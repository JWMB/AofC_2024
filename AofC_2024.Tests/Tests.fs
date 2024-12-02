module Tests

open System
open Xunit

[<Fact>]
let ``template`` () =
    let input = """
"""
    Assert.Equal(0, Template.part1 input)
    Assert.Equal(0, Template.part2 input)


[<Fact>]
let ``D01`` () =
    let input = """
3   4
4   3
2   5
1   3
3   9
3   3
"""
    Assert.Equal(11, D01.part1 input)
    Assert.Equal(31, D01.part2 input)

