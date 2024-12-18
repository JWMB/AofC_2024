# AofC_2024

Learning F# by doing [AdventOfCode 2024](https://adventofcode.com/2024)

Previous:
* [2021](https://github.com/JWMB/AofC_2021)
* [2022](https://github.com/JWMB/AofC_2022)
* [2023](https://github.com/JWMB/AofC_2023)


##Autogenerated##
## [Day 1 : Historian Hysteria](https://adventofcode.com/2024/day/1)
[Source](/AofC_2024/Days/D01.fs) | [Input](/AofC_2024/Days/D01.txt)  
### part1
```FSharp
let part1 input =
    let rows = parseToRows input

    let rotated = rotate rows

    let sorted = rotated |> Array.map (fun a -> Array.sort a)

    let unrotated = rotate sorted

    let diffs = unrotated |> Array.map (fun row -> abs (row[0] - row[1]))

    let result = diffs |> Array.sum
    result
```

Result (in `9`ms): `2756096`
### part2
```FSharp
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
```

Result (in `10`ms): `23117829`
## [Day 2 : Red-Nosed Reports](https://adventofcode.com/2024/day/2)
[Source](/AofC_2024/Days/D02.fs) | [Input](/AofC_2024/Days/D02.txt)  
### part1
```FSharp
let part1 input =
    let rows = Parsing.parseRows input parseRow
    let result = rows |> Array.filter isSafeArray |> Array.length
    result
```

Result (in `8`ms): `639`
### part2
```FSharp
let part2 input =
    let rows = Parsing.parseRows input parseRow

    let getWithoutIndex array index =
        array
        |> Array.mapi (fun i el -> (i <> index, el)) 
        |> Array.filter fst |> Array.map snd

    let tryFindSafeVersion row =
        if isSafeArray row then
            true
        else
            let mutated = [|0..row.Length-1|] |> Array.map (fun index -> getWithoutIndex row index)
            let found = mutated |> Array.tryFind isSafeArray
            if found.IsNone then false else true

    let result = rows |> Array.filter tryFindSafeVersion |> Array.length
    result
```

Result (in `9`ms): `674`
## [Day 3 : Mull It Over](https://adventofcode.com/2024/day/3)
[Source](/AofC_2024/Days/D03.fs) | [Input](/AofC_2024/Days/D03.txt)  
### part1
```FSharp
let part1 input =
    let expressions = Regex.Matches(input, @"(?<op>mul)\((?<args>(\d{1,3}),){1,}(?<argl>\d{1,3})\)")

    let calls = expressions |> Seq.map parseMatch |> Seq.toArray
    let results = calls
                    |> Array.map (fun o -> o.execute)
                    |> Array.choose (function | Value v -> Some(v) | _ -> None)
    let result = results |> Array.sum
    result
```

Result (in `10`ms): `175700056`
### part2
```FSharp
let part2 input =
    let expressions = Regex.Matches(input, @"(?<op>mul|do|don't)\((?<args>(\d{1,3}),)*(?<argl>\d{1,3})?\)")
    let calls = expressions |> Seq.map parseMatch |> Seq.toArray

    let aggregate =
        ( {| Enabled = true; Value = 0 |}, calls)
            ||> Array.fold (fun acc call -> 
                let result = call.execute
                match result with
                | Enabled e -> {| Enabled = e; Value = acc.Value |}
                | Value v -> {| Enabled = acc.Enabled; Value = acc.Value + if acc.Enabled then v else 0 |}
            )
    let result = aggregate.Value
    result
```

Result (in `4`ms): `71668682`
