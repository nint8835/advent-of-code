module AdventOfCode.Solutions.Y2020.D04

open System.IO
open System.Text.RegularExpressions
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllText "input.txt"
    |> String.split "\n\n"
    |> Array.map (
        String.split "\n"
        >> Array.collect (String.split " ")
        >> Array.map (String.split ":" >> Array.toTuple2)
    )

let requiredFields =
    [| "byr"; "iyr"; "eyr"; "hgt"; "hcl"; "ecl"; "pid" |] |> Set.ofArray

let fieldValidators: Map<string, Regex> =
    Map
        [ ("byr", Regex("^(19[2-9][0-9]|200[0-3])$"))
          ("iyr", Regex("^(201[0-9]|2020)$"))
          ("eyr", Regex("^(202[0-9]|2030)$"))
          ("hgt", Regex("^(1[5-8][0-9]cm|19[0-3]cm|59in|7[0-6]in|6[0-9]in)$"))
          ("hcl", Regex("^#[0-9a-f]{6}$"))
          ("ecl", Regex("^(amb|blu|brn|gry|grn|hzl|oth)$"))
          ("pid", Regex("^[0-9]{9}$")) ]

let solve () =
    inputData
    |> Array.filter (fun passport ->
        passport |> Array.map fst |> Set.ofArray |> Set.isSubset requiredFields)
    |> Array.length
    |> printfn "%d"

    inputData
    |> Array.filter (fun passport ->
        passport
        |> Array.filter (fun (field, value) ->
            match Map.tryFind field fieldValidators with
            | Some regex -> regex.IsMatch value
            | None -> true)
        |> Array.map fst
        |> Set.ofArray
        |> Set.isSubset requiredFields)
    |> Array.length
    |> printfn "%d"
