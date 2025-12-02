module AdventOfCode.Solutions.Y2025.D02

open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllText "input.txt"
    |> String.split ","
    |> Array.map (String.split "-" >> Array.map int64 >> Array.toTuple2)

let ids =
    inputData |> Array.map (fun (fst, snd) -> seq { fst..snd } |> Seq.toArray)

let idIsInvalidPt1 (targetId: int64) : bool =
    let idString = string targetId
    let len = idString.Length

    if len % 2 <> 0 then
        false
    else
        let halfLen = len / 2
        let firstHalf = idString[0 .. halfLen - 1]
        let secondHalf = idString[halfLen .. len - 1]
        firstHalf = secondHalf

let idIsInvalidPt2 (targetId: int64) : bool =
    let idString = string targetId
    let len = idString.Length

    seq { 1 .. (len / 2) }
    |> Seq.filter (fun subLen -> len % subLen = 0)
    |> Seq.map (fun subLen ->
        let repeats = len / subLen
        let substring = idString[0 .. subLen - 1]
        String.replicate repeats substring)
    |> Seq.exists ((=) idString)


let solve () =
    ids
    |> Array.map (Array.filter idIsInvalidPt1 >> Array.sum)
    |> Array.sum
    |> printfn "%d"

    ids
    |> Array.map (Array.filter idIsInvalidPt2 >> Array.sum)
    |> Array.sum
    |> printfn "%d"
