module AdventOfCode.Solutions.Y2018.D01

open System.IO

let inputData = File.ReadAllLines "input.txt" |> Array.map int

[<TailCall>]
let rec findFirstRepeat (seen: Set<int>) (frequency: int) (index: int) : int =
    if seen.Contains frequency then
        frequency
    else
        findFirstRepeat
            (seen.Add frequency)
            (frequency + inputData[index % inputData.Length])
            (index + 1)

let solve () =
    inputData |> Array.sum |> printfn "%d"
    findFirstRepeat Set.empty 0 0 |> printfn "%d"
