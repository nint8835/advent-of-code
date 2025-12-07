module AdventOfCode.Solutions.Y2025.D07

open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (Seq.toArray >> Array.map string)
    |> array2D

let gridWidth = Array2D.length2 inputData

let startPosition =
    inputData |> Array2D.findIndices ((=) "S") |> Array.map fst |> Set.ofArray

let splitterPositions = inputData |> Array2D.findIndices ((=) "^")

let splitterRows =
    splitterPositions
    |> Array.groupBy snd
    |> Array.map (snd >> Array.map fst >> Set.ofArray)

let splitBeams
    ((currentBeamCols: Set<int>), (splitCount: int), (timelineCounts: int64[]))
    (splitterCols: Set<int>)
    : Set<int> * int * int64[] =
    let hitSplitters = Set.intersect currentBeamCols splitterCols

    let newBeamCols =
        hitSplitters
        |> Set.fold
            (fun acc col ->
                let acc = Set.remove col acc
                let leftCol = col - 1
                let rightCol = col + 1

                let acc = if leftCol >= 0 then Set.add leftCol acc else acc

                let acc =
                    if rightCol < gridWidth then Set.add rightCol acc else acc

                // TODO: Don't use mutable array
                timelineCounts[leftCol] <-
                    timelineCounts[leftCol] + timelineCounts[col]

                timelineCounts[rightCol] <-
                    timelineCounts[rightCol] + timelineCounts[col]

                timelineCounts[col] <- 0

                acc)
            currentBeamCols

    (newBeamCols, splitCount + Set.count hitSplitters, timelineCounts)


let solve () =
    let (_, splitCount, timelineCounts) =
        splitterRows
        |> Array.fold
            splitBeams
            (startPosition,
             0,
             (Array.zeroCreate gridWidth
              |> Array.updateAt (startPosition |> Set.minElement) 1L))

    splitCount |> printfn "%d"
    timelineCounts |> Array.sum |> printfn "%d"
