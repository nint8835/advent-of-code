#load "../../utils/Utils.fsx"

open System.IO
open Utils

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

inputData |> maxNElves 1 |> printfn "Part A: %d"
inputData |> maxNElves 3 |> printfn "Part B: %d"
