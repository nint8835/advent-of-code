#load "../../utils/Utils.fsx"

open System.IO
open Utils

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

partA |> printfn "%A"
partB |> printfn "%A"
