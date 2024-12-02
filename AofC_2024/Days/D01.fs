module D01

open Tools
open System.Text.RegularExpressions

let rotate (arrOfArrs: 'T array array) =
//let rotate arrOfArrs =
    let numValsPerRow = arrOfArrs
                        |> Array.map (fun r -> r |> Seq.length)
                        |> Array.min
    let rotated = [|0..numValsPerRow-1|] 
                |> Array.map (fun index -> arrOfArrs |> Array.map (fun r -> r[index]))
    rotated

let parseToRows input =
    let rowToValues row = Regex.Matches(row, @"\d+") |> Seq.map (fun o -> int o.Value) |> Seq.toArray
    Parsing.parseRows input rowToValues 

let part1 input =
    let rows = parseToRows input

    let rotated = rotate rows

    let sorted = rotated |> Array.map (fun a -> Array.sort a)

    let unrotated = rotate sorted

    let diffs = unrotated |> Array.map (fun row -> abs (row[0] - row[1]))

    let result = diffs |> Array.sum
    result

let part2 input =
    let rows = parseToRows input

    let rotated = rotate rows

    let countByItem arr = arr |> Array.groupBy (fun v -> v) |> Array.map (fun (a, b) -> (a, Array.length b)) |> Map

    let counts = rotated |> Array.map countByItem


    let calcItemScore key leftCount rightMap =
            let found = rightMap |> Map.tryFind key
            match found with
            | None -> 0
            | Some rightCount -> leftCount * rightCount * key

    let result = counts[0] 
                |> Map.map (fun key value -> calcItemScore key value counts[1] )
                |> Map.values |> Seq.sum
    result