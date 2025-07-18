module AdventOfCode.Solutions.Y2017.D01

open System.IO

let inputData =
    File.ReadAllText "input.txt" |> Seq.map (string >> int) |> Array.ofSeq

let partA =
    inputData
    |> (fun arr -> Array.append arr [| arr.[0] |])
    |> Array.pairwise
    |> Array.filter (fun (a, b) -> a = b)
    |> Array.map fst
    |> Array.sum

let partB =
    inputData
    |> Array.mapi (fun i x ->
        x, inputData[(inputData.Length / 2 + i) % inputData.Length])
    |> Array.filter (fun (a, b) -> a = b)
    |> Array.map fst
    |> Array.sum

let solve () =
    partA |> printfn "Part A: %d"
    partB |> printfn "Part B: %d"
