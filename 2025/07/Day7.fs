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

let timelinesToSet (timelines: int64[]) : Set<int> =
    timelines
    |> Array.indexed
    |> Array.filter (snd >> ((<>) 0L))
    |> Array.map fst
    |> Set.ofArray

let handleSplit (timelines: int64[]) (col: int) : int64[] =
    [| col - 1; col + 1 |]
    |> Array.filter (fun c -> c >= 0 && c < gridWidth)
    |> Array.fold
        (fun timelines newCol ->
            timelines
            |> Array.updateAt newCol (timelines[newCol] + timelines[col]))
        timelines
    |> Array.updateAt col 0L

let splitBeams
    (splitCount: int, timelines: int64[])
    (splitterCols: Set<int>)
    : int * int64[] =
    let hitSplitters = timelines |> timelinesToSet |> Set.intersect splitterCols

    (splitCount + Set.count hitSplitters,
     hitSplitters |> Set.fold handleSplit timelines)

let solve () =
    let splitCount, timelineCounts =
        splitterRows
        |> Array.fold
            splitBeams
            (0,
             (Array.zeroCreate gridWidth
              |> Array.updateAt (startPosition |> Set.minElement) 1L))

    splitCount |> printfn "%d"
    timelineCounts |> Array.sum |> printfn "%d"
