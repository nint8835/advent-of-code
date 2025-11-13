module AdventOfCode.Solutions.Y2018.D02

open System.IO
open AdventOfCode.Solutions.Utils

let inputData = File.ReadAllLines "input.txt"

let findIdCount (duplicates: int) : int =
    inputData
    |> Array.filter (Seq.countBy id >> Seq.map snd >> Seq.contains duplicates)
    |> Array.length

let partB =
    inputData
    |> Array.permutations 2
    |> Array.map Array.toTuple2
    |> Array.find (fun (a, b) ->
        Seq.zip a b |> Seq.filter (fun (x, y) -> x <> y) |> Seq.length = 1)
    |> fun (a, b) ->
        Seq.zip a b
        |> Seq.filter (fun (x, y) -> x = y)
        |> Seq.map fst
        |> Seq.map string
        |> String.concat ""

let solve () =
    (findIdCount 2) * (findIdCount 3) |> printfn "%d"
    partB |> printfn "%s"
