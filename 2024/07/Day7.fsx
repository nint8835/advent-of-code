#load "../../utils/Utils.fsx"

open System.IO
open Utils

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
    let operatorPermutations =
        Array.permutationsWithRepetition (values.Length - 1) operators

    operatorPermutations
    |> Array.exists (fun operatorArr ->
        Array.zip (Array.skip 1 values) operatorArr
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

partA |> printfn "%d"
partB |> printfn "%d"
