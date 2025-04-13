module AdventOfCode.Solutions.Y2022.D02

open System.IO
open AdventOfCode.Solutions.Utils

/// Euclidean remainder, the proper modulo operation
/// Taken from https://stackoverflow.com/a/35848799
let inline (%!) a b = (a % b + b) % b

let getCharCode (char: string) : int = (("ABCXYZ".IndexOf char) %! 3) + 1


let inputData =
    File.ReadAllText "input.txt"
    |> String.split "\n"
    |> Array.map (fun line -> line |> String.split " " |> Array.map getCharCode)
    |> Array.map (fun line -> (line[0], line[1]))

let partA ((m, n): int * int) : int = (3 * ((1 + n - m) %! 3)) + n

let partB ((m, n): int * int) : int = (3 * (n - 1)) + ((m + n) %! 3) + 1

let solve () =
    inputData |> Array.map partA |> Array.sum |> printfn "Part A: %d"
    inputData |> Array.map partB |> Array.sum |> printfn "Part B: %d"
