#load "../../utils/Utils.fsx"

open System.IO
open Utils

let inputData =
    File.ReadAllText "input.txt"
    |> String.split "\n"
    |> Array.map (fun line ->
        line |> String.split " " |> Array.filter ((<>) "") |> Array.map int)

let leftHalf = inputData |> Array.map (fun line -> line |> Array.head)

let rightHalf = inputData |> Array.map (fun line -> line |> Array.last)

let rightHalfCounts = rightHalf |> Array.countBy id

let partA =
    Array.zip (Array.sort leftHalf) (Array.sort rightHalf)
    |> Array.map (fun (a, b) -> abs (a - b))
    |> Array.sum

let partB =
    leftHalf
    |> Array.map (fun a ->
        rightHalfCounts
        |> Array.tryFind (fun (b, _) -> a = b)
        |> (function
        | Some(_, count) -> count
        | None -> 0)
        |> (*) a)
    |> Array.sum

partA |> printfn "%A"
partB |> printfn "%A"
