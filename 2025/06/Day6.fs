module AdventOfCode.Solutions.Y2025.D06

open System
open System.IO
open AdventOfCode.Solutions.Utils

let inputData = File.ReadAllLines "input.txt"

let operators =
    inputData
    |> Array.map (
        String.split " " >> Array.filter (String.IsNullOrEmpty >> not)
    )
    |> Array.last
    |> Array.map (function
        | "+" -> (+)
        | "*" -> (*)
        | _ -> failwith "Invalid operator")

let partANumbers =
    inputData
    |> Array.map (
        String.split " " >> Array.filter (String.IsNullOrEmpty >> not)
    )
    |> (fun arr -> arr[..^1])
    |> Array.map (Array.map int64)
    |> Array.transpose

let partBNumbers =
    inputData
    |> Array.map (Seq.toArray >> Array.map string)
    |> Array.transpose
    |> Array.map (fun arr ->
        arr[..^1] |> Array.filter (String.IsNullOrWhiteSpace >> not))
    |> Array.splitBy (Array.isEmpty)
    |> Array.map (Array.map (String.concat "" >> int64))

let evaluateNumbers (numberArray: int64[][]) : int64 =
    numberArray
    |> Array.zip operators
    |> Array.sumBy (fun (op, numbers) -> numbers |> Array.reduce op)

let solve () =
    partANumbers |> evaluateNumbers |> printfn "%d"
    partBNumbers |> evaluateNumbers |> printfn "%d"
