module AdventOfCode.Solutions.Y2020.D02

open System.IO
open System.Text.RegularExpressions

let lineRegex = Regex("^(\d+)-(\d+) (\w): (\w+)$")

type PasswordEntry =
    { Min: int
      Max: int
      Letter: char
      Password: string }

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map lineRegex.Match
    |> Array.map (fun m ->
        { Min = int m.Groups.[1].Value
          Max = int m.Groups.[2].Value
          Letter = m.Groups.[3].Value.[0]
          Password = m.Groups.[4].Value })

let solve () =
    inputData
    |> Array.filter (fun entry ->
        let letterCount =
            entry.Password
            |> Seq.filter (fun c -> c = entry.Letter)
            |> Seq.length

        letterCount >= entry.Min && letterCount <= entry.Max)
    |> Array.length
    |> printfn "%d"

    inputData
    |> Array.filter (fun entry ->
        let firstPositionMatch = entry.Password.[entry.Min - 1] = entry.Letter
        let secondPositionMatch = entry.Password.[entry.Max - 1] = entry.Letter

        firstPositionMatch <> secondPositionMatch)
    |> Array.length
    |> printfn "%d"
