module AdventOfCode.Solutions.Y2020.D03

open System.IO

type GridContents =
    | Tree
    | Open

let getGridContents =
    function
    | '#' -> Tree
    | '.' -> Open
    | _ -> failwith "Invalid grid content"

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (Seq.map getGridContents >> Seq.toArray)
    |> array2D

[<TailCall>]
let rec walkSlope
    (x: int)
    (y: int)
    (treeCount: int)
    (right: int)
    (down: int)

    : int =
    if y >= inputData.GetLength 0 then
        treeCount
    else
        let currentPosition = inputData[y, x % inputData.GetLength 1]

        let newTreeCount =
            match currentPosition with
            | Tree -> treeCount + 1
            | Open -> treeCount

        walkSlope (x + right) (y + down) newTreeCount right down

let solve () =
    walkSlope 0 0 0 3 1 |> printfn "%d"

    [| (1, 1); (3, 1); (5, 1); (7, 1); (1, 2) |]
    |> Array.map (fun offset -> offset ||> walkSlope 0 0 0)
    |> Array.map int64
    |> Array.reduce (*)
    |> printfn "%d"
