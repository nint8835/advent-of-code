module AdventOfCode.Solutions.Y2024.D03

open System.IO
open System.Text.RegularExpressions

let instructionRegex =
    Regex(
        @"((?'instruction'mul)\((?'a'\d+),(?'b'\d+)\)|(?'instruction'do)\(\)|(?'instruction'don't)\(\))"
    )

let (|Do|Dont|Mul|) (matchVal: Match) =
    match matchVal.Groups["instruction"].Value with
    | "do" -> Do
    | "don't" -> Dont
    | _ -> Mul(int matchVal.Groups["a"].Value, int matchVal.Groups["b"].Value)

let inputData = File.ReadAllText "input.txt"

let partA =
    inputData
    |> instructionRegex.Matches
    |> Seq.map (function
        | Do -> 0
        | Dont -> 0
        | Mul(a, b) -> a * b)
    |> Seq.sum

let partB =
    inputData
    |> instructionRegex.Matches
    |> Seq.fold
        (fun (sum, enabled) matchVal ->
            match matchVal with
            | Do -> (sum, true)
            | Dont -> (sum, false)
            | Mul(a, b) when enabled -> (sum + a * b, enabled)
            | _ -> (sum, enabled))
        (0, true)
    |> fst

let solve () : unit =
    partA |> printfn "%A"
    partB |> printfn "%A"
