module AdventOfCode.Solutions.Y2022.D03

open System.IO
open AdventOfCode.Solutions.Utils

let inputData = File.ReadAllText "input.txt" |> String.split "\n"


let getPriority (item: char) : int =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf item + 1


let calculatePriority
    (grouper: string[] -> string[][])
    (input: string[])
    : int =
    input
    |> grouper
    |> Array.map (fun group ->
        group
        |> Array.map Set.ofSeq
        |> Array.reduce Set.intersect
        |> Set.toArray
        |> Array.map getPriority
        |> Array.sum)
    |> Array.sum

let partAGroups (input: string[]) : string[][] =
    input
    |> Array.map (fun line ->
        [| line[.. line.Length / 2 - 1]; line[line.Length / 2 ..] |])

let partBGroups (input: string[]) : string[][] = input |> Array.chunkBySize 3

let solve () =
    inputData |> calculatePriority partAGroups |> printfn "%A"
    inputData |> calculatePriority partBGroups |> printfn "%A"
