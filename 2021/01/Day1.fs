module AdventOfCode.Solutions.Y2021.D01

open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllText "input.txt" |> String.split "\n" |> Array.map int

let getIncreasingWindows windowSize (array: int[]) =
    array
    |> Array.windowed (windowSize + 1)
    |> Array.map (fun window ->
        window |> Array.windowed windowSize |> Array.map Array.sum)
    |> Array.filter (fun window -> window.[1] > window.[0])
    |> Array.length

let solve () =
    inputData |> getIncreasingWindows 1 |> printfn "Part A: %d"
    inputData |> getIncreasingWindows 3 |> printfn "Part B: %d"
