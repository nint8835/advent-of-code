open System.IO
open System.Text.RegularExpressions

let partARegex = Regex(@"mul\((\d+),(\d+)\)")
let partBRegex = Regex(@"(mul\((\d+),(\d+)\)|do\(\)|don't\(\))")

let inputData = File.ReadAllText "input.txt"

let partA =
    inputData
    |> partARegex.Matches
    |> Seq.map (fun mulMatch ->
        int mulMatch.Groups[1].Value * int mulMatch.Groups[2].Value)
    |> Seq.sum

let partB =
    inputData
    |> partBRegex.Matches
    |> Seq.fold
        (fun (sum, enabled) matchVal ->
            match matchVal.Groups.[1].Value with
            | "do()" -> (sum, true)
            | "don't()" -> (sum, false)
            | _ when enabled ->
                (sum
                 + int matchVal.Groups[2].Value * int matchVal.Groups[3].Value,
                 enabled)
            | _ -> (sum, enabled))
        (0, true)
    |> fst

partA |> printfn "%A"
partB |> printfn "%A"
