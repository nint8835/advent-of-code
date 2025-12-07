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

    let (newBeamCols, newBeamTimelines) =
        hitSplitters
        |> Set.fold
            (fun (wipSet, wipTimelines) col ->
                let wipSet = Set.remove col wipSet
                let leftCol = col - 1
                let rightCol = col + 1

                let wipSet =
                    if leftCol >= 0 then Set.add leftCol wipSet else wipSet

                let wipSet =
                    if rightCol < gridWidth then
                        Set.add rightCol wipSet
                    else
                        wipSet

                let wipTimelines =
                    if leftCol >= 0 then
                        wipTimelines
                        |> Array.updateAt
                            leftCol
                            (wipTimelines[leftCol] + wipTimelines[col])
                    else
                        wipTimelines

                let wipTimelines =
                    if rightCol < gridWidth then
                        wipTimelines
                        |> Array.updateAt
                            rightCol
                            (wipTimelines[rightCol] + wipTimelines[col])
                    else
                        wipTimelines

                let wipTimelines = wipTimelines |> Array.updateAt col 0L

                (wipSet, wipTimelines))
            (currentBeamCols, timelineCounts)

    (newBeamCols, splitCount + Set.count hitSplitters, newBeamTimelines)


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
