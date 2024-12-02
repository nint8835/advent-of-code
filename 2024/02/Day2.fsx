#load "../../utils/Utils.fsx"

open System.IO
open Utils

let inputData =
    File.ReadAllText "input.txt"
    |> String.split "\n"
    |> Array.map (String.split " " >> Array.map int)

let safeReports =
    inputData
    |> Array.filter (fun report ->
        let sortedAscending = Array.sort report
        let sortedDescending = Array.sortDescending report

        report = sortedAscending || report = sortedDescending)
    |> Array.filter (fun report ->
        report
        |> Array.windowed 2
        |> Array.map (fun pair -> pair.[0] - pair.[1] |> abs)
        |> Array.forall (fun diff -> diff >= 1 && diff <= 3))

safeReports |> _.Length |> printfn "%A"
