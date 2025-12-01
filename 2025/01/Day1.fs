module AdventOfCode.Solutions.Y2025.D01

open System
open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (fun line ->
        (if line.[0] = 'L' then -1 else 1)
        * (line |> Seq.skip 1 |> Seq.map string |> String.concat "" |> int))

let solve () =
    inputData
    |> Array.fold
        (fun (curr, zeroCount) offset ->
            let newVal = (curr + offset) %! 100

            newVal, zeroCount + (if newVal = 0 then 1 else 0))
        (50, 0)
    |> snd
    |> printfn "%d"

    inputData
    |> Array.fold
        (fun (curr, zeroCount) offset ->
            let simpleZeros = Math.Abs(offset / 100)
            let newOffset = offset % 100
            let newVal = (curr + newOffset)
            let clampedVal = newVal %! 100
            let isZeroZeroCount = if clampedVal = 0 then 1 else 0

            let isWrapZeroCount =
                if newVal <> clampedVal && clampedVal <> 0 && curr <> 0 then
                    1
                else
                    0

            clampedVal,
            zeroCount + simpleZeros + isZeroZeroCount + isWrapZeroCount)
        (50, 0)
    |> snd
    |> printfn "%d"
