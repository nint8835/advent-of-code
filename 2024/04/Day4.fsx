#load "../../utils/Utils.fsx"

open System.IO
open Utils

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (Seq.map string >> Seq.toArray)
    |> array2D

let inputWidth, inputHeight = inputData.GetLength 1, inputData.GetLength 0

let horizontalOccurrences =
    [| 0 .. inputHeight - 1 |]
    |> Array.map (fun y ->
        [| 0 .. inputWidth - 4 |]
        |> Array.map (fun x ->
            inputData |> Array2D.range (x, y) (x + 3, y) |> String.concat "")
        |> Array.filter (fun str -> str = "XMAS" || str = "SAMX"))

let verticalOccurrences =
    [| 0 .. inputWidth - 1 |]
    |> Array.map (fun x ->
        [| 0 .. inputHeight - 4 |]
        |> Array.map (fun y ->
            inputData |> Array2D.range (x, y) (x, y + 3) |> String.concat "")
        |> Array.filter (fun str -> str = "XMAS" || str = "SAMX"))

let diagonalDownOccurrences =
    [| 0 .. inputWidth - 4 |]
    |> Array.map (fun x ->
        [| 0 .. inputHeight - 4 |]
        |> Array.map (fun y ->
            inputData
            |> Array2D.range (x, y) (x + 3, y + 3)
            |> String.concat "")
        |> Array.filter (fun str -> str = "XMAS" || str = "SAMX"))

let diagonalUpOccurrences =
    [| 0 .. inputWidth - 4 |]
    |> Array.map (fun x ->
        [| 3 .. inputHeight - 1 |]
        |> Array.map (fun y ->
            inputData
            |> Array2D.range (x, y) (x + 3, y - 3)
            |> String.concat "")
        |> Array.filter (fun str -> str = "XMAS" || str = "SAMX"))

let partA =
    Array.concat
        [| horizontalOccurrences
           verticalOccurrences
           diagonalDownOccurrences
           diagonalUpOccurrences |]
    |> Array.map Array.length
    |> Array.sum

let partB =
    [| 0 .. inputWidth - 3 |]
    |> Array.map (fun x ->
        [| 0 .. inputHeight - 3 |]
        |> Array.map (fun y ->
            let downRight =
                inputData
                |> Array2D.range (x, y) (x + 2, y + 2)
                |> String.concat ""

            let upRight =
                inputData
                |> Array2D.range (x, y + 2) (x + 2, y)
                |> String.concat ""

            (downRight = "MAS" || downRight = "SAM")
            && (upRight = "MAS" || upRight = "SAM")))
    |> Array.concat
    |> Array.filter id
    |> Array.length

partA |> printfn "%A"
partB |> printfn "%A"
