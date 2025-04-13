module AdventOfCode.Solutions.Y2022.D04

open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllText "input.txt"
    |> String.split "\n"
    |> Array.map (fun line ->
        line
        |> String.split ","
        |> Array.map (fun elf -> elf |> String.split "-" |> Array.map int))

let tryBothOrders (func: int[] -> int[] -> bool) (pair: int[][]) : bool =
    func pair[0] pair[1] || func pair[1] pair[0]

let partA (a: int[]) (b: int[]) : bool = a[0] <= b[0] && a[1] >= b[1]

let partB (a: int[]) (b: int[]) : bool = a[0] <= b[0] && b[0] <= a[1]

let solve () =
    inputData
    |> Array.filter (tryBothOrders partA)
    |> Array.length
    |> printfn "%A"

    inputData
    |> Array.filter (tryBothOrders partB)
    |> Array.length
    |> printfn "%A"
