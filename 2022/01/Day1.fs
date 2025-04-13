module AdventOfCode.Solutions.Y2022.D01

open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllText "input.txt"
    |> String.split "\n\n"
    |> Array.map (fun elf -> elf |> String.split "\n" |> Array.map int)

let maxNElves (n: int) (elves: int[][]) : int =
    elves
    |> Array.map Array.sum
    |> Array.sortDescending
    |> Array.take n
    |> Array.sum

let solve () =
    inputData |> maxNElves 1 |> printfn "Part A: %d"
    inputData |> maxNElves 3 |> printfn "Part B: %d"
