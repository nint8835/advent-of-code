module AdventOfCode.Solutions.Y2025.D05

open System
open System.IO
open AdventOfCode.Solutions.Utils

let rangeLines, ingredientLines =
    File.ReadAllText "input.txt"
    |> String.split "\n\n"
    |> Array.map (String.split "\n")
    |> Array.toTuple2

let freshIngredients =
    rangeLines
    |> Array.map (String.split "-" >> Array.map int64 >> Array.toTuple2)

let availableIngredients = ingredientLines |> Array.map int64

let solve () =
    availableIngredients
    |> Array.filter (fun v ->
        (freshIngredients
         |> Array.exists (fun (min, max) -> min <= v && max >= v)))
    |> Array.length
    |> printfn "%d"

    freshIngredients
    |> Array.sortBy fst
    |> Array.fold
        (fun acc (min, max) ->
            match acc with
            | [] -> [ (min, max) ]
            | (lastMin, lastMax) :: tail when min <= lastMax + 1L ->
                (lastMin, Math.Max(lastMax, max)) :: tail
            | _ -> (min, max) :: acc)
        []
    |> List.sumBy (fun (min, max) -> max - min + 1L)
    |> printfn "%d"
