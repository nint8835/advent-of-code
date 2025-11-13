module AdventOfCode.Solutions.Y2017.D02

open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (String.split "\t" >> Array.map int)

let partA =
    inputData
    |> Array.map (fun row -> Array.max row - Array.min row)
    |> Array.sum

let partB =
    inputData
    |> Array.map (
        Array.permutations 2
        >> Array.map Array.toTuple2
        >> Array.find (fun (a, b) -> a % b = 0)
        >> fun (a, b) -> a / b
    )
    |> Array.sum

let solve () =
    partA |> printfn "%d"
    partB |> printfn "%d"
