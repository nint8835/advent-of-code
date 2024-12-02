#load "../../utils/Utils.fsx"

open System.IO
open Utils

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (String.split " " >> Array.map int)

let reportIsSafe (report: int[]) : bool =
    let sortedAscending = Array.sort report
    let sortedDescending = Array.sortDescending report

    (report = sortedAscending || report = sortedDescending)
    && report
       |> Array.windowed 2
       |> Array.map (fun pair -> pair[0] - pair[1] |> abs)
       |> Array.forall (fun diff -> diff >= 1 && diff <= 3)

let partA = inputData |> Array.filter reportIsSafe

let partB =
    inputData
    |> Array.filter (fun report ->
        [| 0 .. report.Length - 1 |]
        |> Array.map (fun i -> report |> Array.removeAt i)
        |> Array.exists reportIsSafe)

partA |> _.Length |> printfn "%A"
partB |> _.Length |> printfn "%A"
