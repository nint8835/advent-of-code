module AdventOfCode.Solutions.Y2024.D05

open System.IO
open AdventOfCode.Solutions.Utils

let ruleLines, updateLines =
    File.ReadAllText "input.txt" |> String.split "\n\n" |> Array.toTuple2

let rules =
    ruleLines
    |> String.split "\n"
    |> Array.map (String.split "|" >> Array.map int >> Array.toTuple2)
    |> Array.map (fun (a, b) -> [| ((a, b), -1); ((b, a), 1) |])
    |> Array.concat
    |> Map.ofArray

let updates =
    updateLines
    |> String.split "\n"
    |> Array.map (String.split "," >> Array.map int)

let originallySorted, originallyUnsorted =
    updates
    |> Array.map (fun update ->
        (update,
         update
         |> Array.sortWith (fun a b ->
             rules |> Map.tryFind (a, b) |> Option.defaultValue 0)))
    |> Array.partition (fun (original, sorted) -> original = sorted)

let solve () : unit =
    originallySorted
    |> Array.map fst
    |> Array.map (fun update -> update[update.Length / 2])
    |> Array.sum
    |> printfn "%A"

    originallyUnsorted
    |> Array.map snd
    |> Array.map (fun update -> update[update.Length / 2])
    |> Array.sum
    |> printfn "%A"
