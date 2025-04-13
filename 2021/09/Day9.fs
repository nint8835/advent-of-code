// aoc-tools:stars 1
module AdventOfCode.Solutions.Y2021.D09

let inputData =
    (System.IO.File.ReadAllText "input.txt").Split "\n"
    |> Array.map (fun line ->
        line |> Seq.toArray |> Array.map string |> Array.map int)
    |> array2D

let width = (inputData.GetLength 1) - 1
let height = (inputData.GetLength 0) - 1

let calculateLowPointsSum (grid: int[,]) : int =
    let mutable lowPointsSum = 0

    for x = 0 to width do
        for y = 0 to height do
            let pointsToCheck =
                Array.concat
                    [| if x <> 0 then [| (y, x - 1) |] else [||]
                       if x < width then [| (y, x + 1) |] else [||]
                       if y <> 0 then [| (y - 1, x) |] else [||]
                       if y < height then [| (y + 1, x) |] else [||] |]

            let lowerPoints =
                pointsToCheck
                |> Array.map (fun (checkY, checkX) ->
                    grid.[checkY, checkX] <= grid.[y, x])
                |> Array.filter id

            if lowerPoints.Length = 0 then
                printfn $"%d{x} %d{y}"
                lowPointsSum <- lowPointsSum + grid.[y, x] + 1

    lowPointsSum

let solve () =
    let partA = inputData |> calculateLowPointsSum

    printfn $"%d{partA}"
