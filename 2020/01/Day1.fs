module AdventOfCode.Solutions.Y2020.D01

open System.IO
open AdventOfCode.Solutions.Utils

let inputData = File.ReadAllLines "input.txt" |> Array.map int

let solve () =
    inputData
    |> Array.permutations 2
    |> Array.map Array.toTuple2
    |> Array.find (fun (a, b) -> a + b = 2020)
    ||> (*)
    |> printfn "%d"

    inputData
    |> Array.permutations 3
    |> Array.map Array.toTuple3
    |> Array.find (fun (a, b, c) -> a + b + c = 2020)
    |> fun (a, b, c) -> a * b * c
    |> printfn "%d"
    
    1 %! 2
