module AdventOfCode.Solutions.Y2024.D11

open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllText "input.txt"
    |> String.split " "
    |> Array.map uint64
    |> Array.countBy id
    |> Array.map (fun (k, v) -> (k, uint64 v))

let blink (stone: uint64) (count: uint64) : (uint64 * uint64)[] =
    let strStone = string stone

    if stone = 0UL then
        [| (1UL, count) |]
    else if ((strStone |> String.length) % 2) = 0 then
        [| (uint64 strStone[0 .. strStone.Length / 2 - 1], count)
           (uint64 strStone[strStone.Length / 2 .. strStone.Length - 1], count) |]
    else
        [| (stone * 2024UL, count) |]

let performBlinks (count: int) : uint64 =
    [| 0 .. count - 1 |]
    |> Array.fold
        (fun nums _ ->
            nums
            |> Array.map (fun (k, v) -> blink k v)
            |> Array.concat
            |> Array.groupBy fst
            |> Array.map (
                (fun (k, countArrs) -> (k, countArrs |> Array.sumBy snd))
            ))
        inputData
    |> Array.sumBy snd

let solve () : unit =
    performBlinks 25 |> printfn "%d"
    performBlinks 75 |> printfn "%d"
