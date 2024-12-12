#load "../../utils/Utils.fsx"

open System.IO
open Utils

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (Seq.map string >> Seq.toArray)
    |> array2D

let inputWidth, inputHeight = inputData.GetLength 1, inputData.GetLength 0

type Plot =
    { Perimeter: int
      Area: int
      Coordinates: Set<int * int> }

[<TailCall>]
let rec discoverPlotSize
    (targetPoints: (int * int)[])
    (seenPoints: Set<int * int>)
    (currentPlot: Plot)
    : Set<int * int> * Plot =
    if targetPoints |> Array.length = 0 then
        seenPoints, currentPlot
    else
        let target = targetPoints |> Array.head
        let remainingPoints = targetPoints |> Array.skip 1

        if seenPoints |> Set.contains target then
            discoverPlotSize remainingPoints seenPoints currentPlot
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
                { Perimeter = currentPlot.Perimeter + 4 - samePlants.Length
                  Area = currentPlot.Area + 1
                  Coordinates = currentPlot.Coordinates |> Set.add target }

let countCorners (plot: Plot) : int =
    let outerCorners =
        plot.Coordinates
        |> Set.toArray
        |> Array.map (fun (x, y) ->
            [| [| (x - 1, y); (x, y - 1) |]
               [| (x, y - 1); (x + 1, y) |]
               [| (x - 1, y); (x, y + 1) |]
               [| (x, y + 1); (x + 1, y) |] |]
            |> Array.filter (fun coords ->
                coords
                |> Array.forall (fun coord ->
                    not (plot.Coordinates |> Set.contains coord)))
            |> Array.length)
        |> Array.sum

    let innerCorners =
        plot.Coordinates
        |> Set.toArray
        |> Array.map (fun (x, y) ->
            [| ([| (x - 1, y); (x, y - 1) |], (x - 1, y - 1))
               ([| (x, y - 1); (x + 1, y) |], (x + 1, y - 1))
               ([| (x - 1, y); (x, y + 1) |], (x - 1, y + 1))
               ([| (x, y + 1); (x + 1, y) |], (x + 1, y + 1)) |]
            |> Array.filter (fun (coords, diag) ->
                coords
                |> Array.forall (fun coord ->
                    (plot.Coordinates |> Set.contains coord))
                && not (plot.Coordinates |> Set.contains diag))
            |> Array.length)
        |> Array.sum

    innerCorners + outerCorners

let plots =
    [| 0 .. inputHeight - 1 |]
    |> Array.map (fun y ->
        [| 0 .. inputWidth - 1 |] |> Array.map (fun x -> (x, y)))
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
                        { Perimeter = 0
                          Area = 0
                          Coordinates = Set.empty }

                newSeenPoints, plots |> Array.append [| plotSize |])
        (Set.empty, [||])
    |> snd

plots |> Array.sumBy (fun plot -> plot.Area * plot.Perimeter) |> printfn "%A"

plots |> Array.sumBy (fun plot -> plot.Area * countCorners plot) |> printfn "%A"
