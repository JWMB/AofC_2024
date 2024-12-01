module D01

open Tools
open System.Text.RegularExpressions

let part1 input =
    let rowToValues row = Regex.Matches(row, @"\d+") |> Seq.map (fun o -> int o.Value) |> Seq.toArray
    let rows = Parsing.parseRows input rowToValues 

    let numValsPerRow = rows
                        |> Array.map (fun r -> r.Length)
                        |> Array.min

    // rotate
    let arrays = [|0..numValsPerRow-1|] 
                  |> Array.map (fun index -> rows |> Array.map (fun r -> r[index]))

    let sorted = arrays |> Array.map (fun a -> Array.sort a)

    // rotate back
    let orgFormat = [|0..rows.Length-1|]
                     |> Array.map (fun row -> [|0..sorted.Length-1|] |> Array.map (fun index -> sorted[index][row]))

    let diffs = orgFormat |> Array.map (fun row -> abs (row[0] - row[1]))

    let result = diffs |> Array.sum
    result
