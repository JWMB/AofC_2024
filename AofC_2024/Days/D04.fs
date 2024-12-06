module D04

open Tools

let parseRow row = row |> Seq.toArray

let part1 input =
    let rows = Parsing.parseRows input (fun s -> s)
    let matchWith = "XMAS" |> Seq.toArray

    let singleRow = rows |> String.concat "" |> Seq.toArray

    let width = rows[0] |> Seq.length;
    let height = rows |> Seq.length;
    
    let xyToIndex x y = y * width + x
    let indexToXY index = (index % width, index / width)
    
    let makeMove position scalar length =
        position + scalar * length;

    let getXYMoved startXY move step =
        makeMove (fst startXY) (fst move) step, makeMove (snd startXY) (snd move) step

    let getAt startXY move step =
        let xy = getXYMoved startXY move step
        if (fst xy) < 0 || (fst xy) >= width || (snd xy) < 0 || (snd xy) >= height then '0' else singleRow[(xyToIndex (fst xy) (snd xy))]

    let getIsCorrectChar startIndex move i shouldBeChar = 
        let c = getAt (indexToXY startIndex) move i
        c = shouldBeChar

    let checkFrom index move =
        let anyMismatch = matchWith |> Array.tail |> Seq.mapi (fun i v -> getIsCorrectChar index move (i + 1) v) |> Seq.filter (fun v -> v = false) |> Seq.length > 0
        anyMismatch = false

    let moves = [|-1..1|] |> Array.map (fun x -> [|-1..1|] |> Array.map (fun y -> (x, y))) |> Array.reduce Array.append |> Array.filter (fun p -> if (fst p) = 0 && snd p = 0 then false else true)
    let startIndices = singleRow |> Array.mapi (fun i c -> (c, i)) |> Array.filter (fun t -> if fst t = 'X' then true else false) |> Array.map (fun t -> snd t)

    let checks = startIndices |> Array.map (fun index -> moves |> Array.map (fun move -> (index, move))) |> Array.reduce Array.append

    let found = checks |> Array.map (fun (index, move) -> checkFrom index move) |> Array.filter (fun v -> v)

    let result = found |> Array.length
    result
    
let part2 input =
    let rows = Parsing.parseRows input parseRow
    let result = 0
    result
