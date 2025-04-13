module AdventOfCode.Solutions.Y2024.D01

open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllText "input.txt"
    |> String.split "\n"
    |> Array.map (String.split "   " >> Array.map int >> Array.toTuple2)

let leftHalf, rightHalf = inputData |> Array.unzip

let rightHalfCounts = rightHalf |> Array.countBy id |> Map.ofArray

let partA =
    Array.zip (Array.sort leftHalf) (Array.sort rightHalf)
    |> Array.sumBy (fun pair -> pair ||> (-) |> abs)

let partB =
    leftHalf
    |> Array.sumBy (fun a ->
        rightHalfCounts |> Map.tryFind a |> Option.defaultValue 0 |> (*) a)

let solve () : unit =
    partA |> printfn "%A"
    partB |> printfn "%A"
