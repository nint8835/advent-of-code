module AdventOfCode.Solutions.Y2023.D02

open System.Text.RegularExpressions
open System

let inputRegex =
    Regex(@"^Game (\d+): ([^;\n]+(?:;|$))+$", RegexOptions.Compiled)

let inputData =
    System.IO.File.ReadAllText("input.txt").Split "\n"
    |> Array.map (fun line ->
        let groups =
            inputRegex.Match line
            |> _.Groups
            |> Seq.skip 1
            |> Seq.toArray
            |> Array.map (fun group ->
                group.Captures |> Seq.toArray |> Array.map _.Value)

        let game = int groups[0].[0]

        let maxCounts =
            groups[1]
            |> Array.map (fun round ->
                round.Trim().Replace(";", "").Split(", ")
                |> Array.map (fun entry ->
                    let parts = entry.Split(" ")
                    int parts[0], parts[1])
                |> Array.fold
                    (fun acc (count, colour) ->
                        acc
                        |> Map.change colour (function
                            | Some currentCount -> Some(currentCount + count)
                            | None -> Some(count)))
                    (Map []))
            |> Array.fold
                (fun acc subMap ->
                    subMap
                    |> Map.fold
                        (fun wipMap colour count ->
                            wipMap
                            |> Map.change colour (function
                                | Some currentCount ->
                                    Some(Math.Max(count, currentCount))
                                | None -> Some(count)))
                        acc)

                (Map [])

        game, maxCounts)

let solve () =
    let partA =
        inputData
        |> Array.filter (fun (_, maxCounts) ->
            maxCounts["red"] <= 12
            && maxCounts["green"] <= 13
            && maxCounts["blue"] <= 14)
        |> Array.map fst
        |> Array.sum

    let partB =
        inputData
        |> Array.map (fun (_, maxCounts) ->
            maxCounts |> Map.values |> Seq.fold (*) 1)
        |> Array.sum

    printfn "%A" partA
    printfn "%A" partB
