#load "../../utils/Utils.fsx"

open System.IO
open Utils

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

inputData |> calculatePriority partAGroups |> printfn "%A"
inputData |> calculatePriority partBGroups |> printfn "%A"
