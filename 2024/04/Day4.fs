module AdventOfCode.Solutions.Y2024.D04

open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (Seq.map string >> Seq.toArray)
    |> array2D

let inputWidth, inputHeight = inputData.GetLength 1, inputData.GetLength 0

let checkRange
    (xCoords: int[])
    (yCoords: int[])
    (rangeSelector: int -> int -> (int * int) * (int * int))
    (requiredStr: string)
    : (int * int)[] =
    xCoords
    |> Array.map (fun x ->
        yCoords
        |> Array.filter (fun y ->
            let value =
                inputData
                |> (rangeSelector x y ||> Array2D.range)
                |> String.concat ""

            (value = requiredStr) || (value = (String.reverse requiredStr)))
        |> Array.map (fun y -> (x, y)))
    |> Array.concat

let horizontalOccurrences =
    checkRange
        [| 0 .. inputWidth - 4 |]
        [| 0 .. inputHeight - 1 |]
        (fun x y -> ((x, y), (x + 3, y)))
        "XMAS"

let verticalOccurrences =
    checkRange
        [| 0 .. inputWidth - 1 |]
        [| 0 .. inputHeight - 4 |]
        (fun x y -> ((x, y), (x, y + 3)))
        "XMAS"

let diagonalDownOccurrences =
    checkRange
        [| 0 .. inputWidth - 4 |]
        [| 0 .. inputHeight - 4 |]
        (fun x y -> ((x, y), (x + 3, y + 3)))
        "XMAS"

let diagonalUpOccurrences =
    checkRange
        [| 0 .. inputWidth - 4 |]
        [| 3 .. inputHeight - 1 |]
        (fun x y -> ((x, y), (x + 3, y - 3)))
        "XMAS"

let partA =
    [| horizontalOccurrences
       verticalOccurrences
       diagonalDownOccurrences
       diagonalUpOccurrences |]
    |> Array.sumBy Array.length

let downRightMas =
    checkRange
        [| 0 .. inputWidth - 3 |]
        [| 0 .. inputHeight - 3 |]
        (fun x y -> ((x, y), (x + 2, y + 2)))
        "MAS"
    |> Set.ofArray

let upRightMas =
    checkRange
        [| 0 .. inputWidth - 3 |]
        [| 0 .. inputHeight - 3 |]
        (fun x y -> ((x, y + 2), (x + 2, y)))
        "MAS"
    |> Set.ofArray

let partB = Set.intersect downRightMas upRightMas |> Set.count

let solve () : unit =
    partA |> printfn "%A"
    partB |> printfn "%A"
