module AdventOfCode.Solutions.Y2017.D05

open System.IO

[<TailCall>]
let rec jump
    (offsets: int array)
    (position: int)
    (steps: int)
    (calculateNewOffset: int -> int)
    : int =
    if position < 0 || position >= offsets.Length then
        steps
    else
        let positionOffset = offsets[position]

        jump
            (offsets
             |> Array.updateAt position (calculateNewOffset positionOffset))
            (position + positionOffset)
            (steps + 1)
            calculateNewOffset

let inputData = File.ReadAllLines "input.txt" |> Array.map int

let solve () =
    jump inputData 0 0 (fun offset -> offset + 1) |> printfn "%d"

    jump inputData 0 0 (fun offset ->
        if offset >= 3 then offset - 1 else offset + 1)
    |> printfn "%d"
