module AdventOfCode.Solutions.Y2022.D06

open System.IO

let inputData = File.ReadAllText "input.txt"

let findMarker (size: int) (input: string) : int =
    input
    |> Seq.windowed size
    |> Seq.map (fun window -> (window |> Set.ofArray |> Set.count))
    |> Seq.findIndex ((=) size)
    |> (+) size

let solve () =
    inputData |> findMarker 4 |> printfn "%A"
    inputData |> findMarker 14 |> printfn "%A"
