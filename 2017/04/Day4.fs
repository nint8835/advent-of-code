module AdventOfCode.Solutions.Y2017.D04

open System.IO
open AdventOfCode.Solutions.Utils

let inputData = File.ReadAllLines "input.txt" |> Array.map (String.split " ")

let partA =
    inputData
    |> Array.filter (fun row ->
        Array.length row = Array.length (Array.distinct row))
    |> Array.length

let partB =
    inputData
    |> Array.map (Array.map (Seq.sort >> Seq.map string >> String.concat ""))
    |> Array.filter (fun row ->
        Array.length row = Array.length (Array.distinct row))
    |> Array.length

let solve () =
    partA |> printfn "%d"
    partB |> printfn "%d"
