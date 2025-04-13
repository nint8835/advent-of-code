module AdventOfCode.Solutions.Y2024.D08

open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (Seq.toArray >> Array.map string)
    |> array2D

let inputWidth, inputHeight = inputData.GetLength 1, inputData.GetLength 0

let antennas =
    inputData
    |> Array2D.findIndices (fun x -> x <> ".")
    |> Array.groupBy (fun (x, y) -> inputData[y, x])
    |> Array.map snd


let findAntipoles (stepStart: int) (stepEnd: int) : int =
    antennas
    |> Array.map (fun coords ->
        coords
        |> Array.permutations 2
        |> Array.map Array.toTuple2
        |> Array.map (fun (a, b) ->
            let run, rise = Tuple.subtract2 b a

            [| stepStart..stepEnd |]
            |> Array.map (fun step -> Tuple.add2 a (run * step, rise * step)))
        |> Array.concat
        |> Set.ofArray)
    |> Set.unionMany
    |> Set.filter (fun (x, y) ->
        x >= 0 && x < inputWidth && y >= 0 && y < inputHeight)
    |> Set.count

let solve () : unit =
    findAntipoles 2 2 |> printfn "%A"
    findAntipoles 1 inputHeight |> printfn "%A"
