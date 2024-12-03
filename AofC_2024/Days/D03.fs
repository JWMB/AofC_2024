module D03

open Tools
open System.Text.RegularExpressions

type CallResult =
    | Value of int
    | Enabled of bool

let parseRow row = [| row |]
type Call = { Operation: string; Arguments: int array } with
    member this.execute =
        match this.Operation with
        | "mul" -> Value(this.Arguments |> Array.reduce (fun p c -> p * c))
        | "do" -> Enabled(true)
        | "don't" -> Enabled(false)
        | _ -> raise (System.ArgumentException("Unknown operation!"))

let parseMatch (m: Match) =
    let args = if m.Groups["args"] <> null
                    then m.Groups["args"].Captures |> Seq.map (fun c -> c.Value.TrimEnd(','))
                    else [| |]
    let argl = if m.Groups["argl"].Success then [|  m.Groups["argl"].Value |] else [||]
    let args = args |> Seq.append argl |> Seq.map int |> Seq.toArray
    { Operation = m.Groups["op"].Value; Arguments = args }

let part1 input =
    let expressions = Regex.Matches(input, @"(?<op>mul)\((?<args>(\d{1,3}),){1,}(?<argl>\d{1,3})\)")

    let calls = expressions |> Seq.map parseMatch |> Seq.toArray
    let results = calls
                    |> Array.map (fun o -> o.execute)
                    |> Array.choose (function | Value v -> Some(v) | _ -> None)
    let result = results |> Array.sum
    result
    
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
