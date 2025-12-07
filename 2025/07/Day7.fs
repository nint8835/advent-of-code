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

let handleSplit
    (beams: Set<int>, timelines: int64[])
    (col: int)
    : Set<int> * int64[] =
    let beams = Set.remove col beams
    let leftCol = col - 1
    let rightCol = col + 1

    let beams = if leftCol >= 0 then Set.add leftCol beams else beams

    let beams =
        if rightCol < gridWidth then
            Set.add rightCol beams
        else
            beams

    let timelines =
        if leftCol >= 0 then
            timelines
            |> Array.updateAt leftCol (timelines[leftCol] + timelines[col])
        else
            timelines

    let timelines =
        if rightCol < gridWidth then
            timelines
            |> Array.updateAt rightCol (timelines[rightCol] + timelines[col])
        else
            timelines

    let timelines = timelines |> Array.updateAt col 0L

    beams, timelines

let splitBeams
    (currentBeamCols: Set<int>, splitCount: int, timelineCounts: int64[])
    (splitterCols: Set<int>)
    : Set<int> * int * int64[] =
    let hitSplitters = Set.intersect currentBeamCols splitterCols

    let newBeamCols, newBeamTimelines =
        hitSplitters |> Set.fold handleSplit (currentBeamCols, timelineCounts)

    (newBeamCols, splitCount + Set.count hitSplitters, newBeamTimelines)


let solve () =
    let _, splitCount, timelineCounts =
        splitterRows
        |> Array.fold
            splitBeams
            (startPosition,
             0,
             (Array.zeroCreate gridWidth
              |> Array.updateAt (startPosition |> Set.minElement) 1L))

    splitCount |> printfn "%d"
    timelineCounts |> Array.sum |> printfn "%d"
