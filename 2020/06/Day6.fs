module AdventOfCode.Solutions.Y2020.D06

open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllText "input.txt"
    |> String.split "\n\n"
    |> Array.map (String.split "\n" >> Array.map Set.ofSeq)

let solve () =
    inputData
    |> Array.map (Set.unionMany >> Set.count)
    |> Array.sum
    |> printfn "%d"

    inputData
    |> Array.map (Set.intersectMany >> Set.count)
    |> Array.sum
    |> printfn "%d"
