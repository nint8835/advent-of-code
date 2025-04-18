module AdventOfCode.Solutions.Y2024.D07

open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (String.split ": " >> Array.toTuple2)
    |> Array.map (fun (target, values) ->
        uint64 target, values |> String.split " " |> Array.map uint64)

let concatOperator (a: uint64) (b: uint64) : uint64 =
    a.ToString() + b.ToString() |> uint64

let inputIsValid
    (operators: (uint64 -> uint64 -> uint64)[])
    ((target, values): (uint64 * uint64[]))
    : bool =
    operators
    |> Array.permutationsWithRepetition (values.Length - 1)
    |> Array.exists (fun operatorArr ->
        Array.zip (values |> Array.skip 1) operatorArr
        |> Array.fold
            (fun acc (value, operator) -> operator acc value)
            values[0]
        |> (=) target)

let partA =
    inputData
    |> Array.filter (inputIsValid [| (+); (*) |])
    |> Array.map fst
    |> Array.sum

let partB =
    inputData
    |> Array.filter (inputIsValid [| (+); (*); concatOperator |])
    |> Array.map fst
    |> Array.sum

let solve () : unit =
    partA |> printfn "%d"
    partB |> printfn "%d"
