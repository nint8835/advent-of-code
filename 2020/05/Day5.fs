module AdventOfCode.Solutions.Y2020.D05

open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (fun line ->
        line
        |> Seq.fold
            (fun (rowValue, colValue) char ->
                match char with
                | 'F' -> (rowValue <<< 1, colValue)
                | 'B' -> ((rowValue <<< 1) ||| 1, colValue)
                | 'L' -> (rowValue, colValue <<< 1)
                | 'R' -> (rowValue, (colValue <<< 1) ||| 1)
                | _ -> failwith "Invalid character")
            (0, 0)
        |> fun (row, col) -> row * 8 + col)

let solve () =
    inputData |> Array.max |> printfn "%d"

    let idSet = inputData |> Set.ofArray

    [| 0 .. Set.maxElement idSet |]
    |> Array.windowed 3
    |> Array.map Array.toTuple3
    |> Array.find (fun (prev, curr, next) ->
        Set.contains prev idSet
        && not (Set.contains curr idSet)
        && Set.contains next idSet)
    |> fun (_, missing, _) -> missing |> printfn "%d"
