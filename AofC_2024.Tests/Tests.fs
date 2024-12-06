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



[<Fact>]
let ``D02`` () =
    let input = """
7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9
"""
    Assert.Equal(2, D02.part1 input)
    Assert.Equal(4, D02.part2 input)




[<Fact>]
let ``D03`` () =
    let input = """
xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
"""
    Assert.Equal(161, D03.part1 input)

    let input = """
xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))
"""
    Assert.Equal(48, D03.part2 input)


[<Fact>]
let ``D04`` () =
    let input = """
MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX
"""
    Assert.Equal(18, D04.part1 input)
    Assert.Equal(9, D04.part2 input)


