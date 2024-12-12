#load "../../utils/Utils.fsx"

open System.IO
open Utils

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (Seq.map string >> Seq.toArray)
    |> array2D

let inputWidth, inputHeight = inputData.GetLength 1, inputData.GetLength 0

type PlotSize = { Perimeter: int; Area: int }

[<TailCall>]
let rec discoverPlotSize
    (targetPoints: (int * int)[])
    (seenPoints: Set<int * int>)
    (currentSize: PlotSize)
    : Set<int * int> * PlotSize =
    if targetPoints |> Array.length = 0 then
        seenPoints, currentSize
    else
        let target = targetPoints |> Array.head
        let remainingPoints = targetPoints |> Array.skip 1

        if seenPoints |> Set.contains target then
            discoverPlotSize remainingPoints seenPoints currentSize
        else
            let (x, y) = target
            let targetValue = inputData.[y, x]

            let samePlants, differentPlants =
                [| (x - 1, y); (x + 1, y); (x, y - 1); (x, y + 1) |]
                |> Array.filter (fun (x, y) ->
                    x >= 0 && x < inputWidth && y >= 0 && y < inputHeight)
                |> Array.partition (fun (x, y) ->
                    inputData.[y, x] = targetValue)

            discoverPlotSize
                (remainingPoints |> Array.append samePlants)
                (seenPoints |> Set.add target)
                { Perimeter = currentSize.Perimeter + 4 - samePlants.Length
                  Area = currentSize.Area + 1 }

[| 0 .. inputHeight - 1 |]
|> Array.map (fun y -> [| 0 .. inputWidth - 1 |] |> Array.map (fun x -> (x, y)))
|> Array.concat
|> Array.fold
    (fun (seenPoints, plots) target ->
        if seenPoints |> Set.contains target then
            seenPoints, plots
        else
            let (newSeenPoints, plotSize) =
                discoverPlotSize
                    [| target |]
                    seenPoints
                    { Perimeter = 0; Area = 0 }

            newSeenPoints, plots |> Array.append [| plotSize |])
    (Set.empty, [||])
|> snd
|> Array.sumBy (fun plot -> plot.Area * plot.Perimeter)
|> printfn "%A"
