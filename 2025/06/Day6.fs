module AdventOfCode.Solutions.Y2025.D06

open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (String.split " " >> Array.filter (fun s -> s <> ""))

let numberColumns =
    inputData
    |> Array.take ((inputData |> Array.length) - 1)
    |> Array.map (Array.map int64)
    |> Array.transpose

let operators =
    inputData
    |> Array.last
    |> Array.map (function
        | "+" -> (+)
        | "*" -> (*)
        | _ -> failwith "Invalid operator")

let partBNumberColumns =
    File.ReadAllLines "input.txt"
    |> Array.map (Seq.toArray >> Array.map string)
    |> Array.transpose
    |> Array.map (fun arr ->
        arr |> Array.take (arr.Length - 1) |> Array.filter (fun s -> s <> " "))
    |> Array.splitBy (Array.isEmpty)
    |> Array.map (Array.map (String.concat "" >> int64))

let evaluateNumbers (numberArray: int64[][]) : int64 =
    numberArray
    |> Array.zip operators
    |> Array.sumBy (fun (op, numbers) -> numbers |> Array.reduce op)

let solve () =
    evaluateNumbers numberColumns |> printfn "%d"
    evaluateNumbers partBNumberColumns |> printfn "%d"
