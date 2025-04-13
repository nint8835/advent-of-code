module AdventOfCode.Solutions.Y2024.D10

open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (Seq.map (string >> int))
    |> array2D

let inputWidth, inputHeight = inputData.GetLength 1, inputData.GetLength 0

let rec findPeaks ((x, y): int * int) : Set<int * int> * int =
    let currentValue = inputData.[y, x]

    if currentValue = 9 then
        Set.singleton (x, y), 1
    else
        [| (0, -1); (0, 1); (-1, 0); (1, 0) |]
        |> Array.map (fun (dx, dy) -> (x + dx, y + dy))
        |> Array.filter (fun (nx, ny) ->
            nx >= 0 && nx < inputWidth && ny >= 0 && ny < inputHeight)
        |> Array.map (fun (nx, ny) -> ((nx, ny), inputData.[ny, nx]))
        |> Array.filter (fun (_, v) -> (v - currentValue) = 1)
        |> Array.map fst
        |> Array.map findPeaks
        |> Array.fold
            (fun (setAcc, countAcc) (set, count) ->
                Set.union setAcc set, countAcc + count)
            (Set.empty, 0)

let searchResult =
    inputData |> Array2D.findIndices ((=) 0) |> Array.map findPeaks

let solve () : unit =
    searchResult
    |> Array.map fst
    |> Array.map Set.count
    |> Array.sum
    |> printfn "%A"

    searchResult |> Array.map snd |> Array.sum |> printfn "%A"
