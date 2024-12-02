module D02

open Tools
open System.Text.RegularExpressions

let parseRow row = [| row |]

let part1 input =
    let rows = Parsing.parseRows input (fun r -> Regex.Matches(r, @"\d+") |> Seq.map (fun v -> int v.Value) |> Seq.toArray)

    let getDiffs array = 
        array |> Array.pairwise |> Array.map (fun (a, b) -> b - a)

    let isUnsafeDiff value = 
        let v = abs value 
        if v < 1 || v > 3 then true else false

    let isSafeArray array =
        let diffs = array |> getDiffs
        let unsafeDiffs = diffs |> Array.filter isUnsafeDiff 
        if unsafeDiffs |> Array.length > 0 then
            false
        else
            let signs = diffs |> Array.map sign
            if signs |> Array.distinct |> Array.length = 1 then
                true
            else
                false

    let result = rows |> Array.filter isSafeArray |> Array.length
    result
    
let part2 input =
    let rows = Parsing.parseRows input parseRow
    let result = 0
    result
