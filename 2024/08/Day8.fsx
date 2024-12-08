#load "../../utils/Utils.fsx"

open System.IO
open Utils

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (Seq.toArray >> Array.map string)
    |> array2D

let inputWidth, inputHeight = inputData.GetLength 1, inputData.GetLength 0

let antennas =
    inputData
    |> Array2D.findIndices (fun x -> x <> ".")
    |> Array.groupBy (fun (x, y) -> inputData.[y, x])
    |> Map.ofArray

let partA =
    antennas
    |> Map.values
    |> Seq.toArray
    |> Array.map (fun (coords: (int * int)[]) ->
        coords
        |> Array.permutations 2
        |> Array.map Array.toTuple2
        |> Array.map (fun (a, b) ->
            let diff = Tuple.subtract2 b a
            let potentialAntiNode1 = Tuple.add2 b diff
            let potentialAntiNode2 = Tuple.subtract2 a diff

            [| potentialAntiNode1; potentialAntiNode2 |])
        |> Array.concat
        |> Set.ofArray)
    |> Set.unionMany
    |> Set.filter (fun (x, y) ->
        x >= 0 && x < inputWidth && y >= 0 && y < inputHeight)
    |> Set.count

let partB =
    antennas
    |> Map.values
    |> Seq.toArray
    |> Array.map (fun (coords: (int * int)[]) ->
        coords
        |> Array.permutations 2
        |> Array.map Array.toTuple2
        |> Array.map (fun (a, b) ->
            let diff = Tuple.subtract2 b a

            seq {
                for step in 1..inputHeight do
                    let offset = (fst diff * step, snd diff * step)
                    yield Tuple.add2 a offset
            }
            |> Seq.toArray)
        |> Array.concat
        |> Set.ofArray)
    |> Set.unionMany
    |> Set.filter (fun (x, y) ->
        x >= 0 && x < inputWidth && y >= 0 && y < inputHeight)
    |> Set.count

printfn "%A" partA
printfn "%A" partB
