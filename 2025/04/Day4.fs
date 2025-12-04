module AdventOfCode.Solutions.Y2025.D04

open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (
        Seq.map (function
            | '.' -> 0
            | '@' -> 1
            | _ -> failwith "Invalid input")
        >> Seq.toArray
    )
    |> array2D

let locateRemovablePaper (input: int[,]) : (int * int)[] =
    input
    |> Array2D.indexed
    |> Array.filter (fun (_, _, v) -> v = 1)
    |> Array.filter (fun (x, y, _) ->
        let neighbourSum =
            Array2D.neighbourValuesWithDiagonals x y input |> Array.sum

        neighbourSum < 4)
    |> Array.map (fun (x, y, _) -> (x, y))

[<TailCall>]
let rec removeAllReachablePaper (totalRemoved: int) (input: int[,]) =
    let removablePaper = locateRemovablePaper input

    if Array.isEmpty removablePaper then
        totalRemoved
    else
        removablePaper
        |> Array.fold (fun acc (x, y) -> Array2D.updateAt x y 0 acc) input
        |> removeAllReachablePaper (totalRemoved + Array.length removablePaper)

let solve () =
    inputData |> locateRemovablePaper |> Array.length |> printfn "%d"
    inputData |> removeAllReachablePaper 0 |> printfn "%d"
