#load "../../utils/Utils.fsx"

open System.IO
open Utils

let inputText = File.ReadAllText "input.txt" |> Seq.map string

let fileSizes =
    inputText
    |> Seq.mapi (fun i d -> (i, d))
    |> Seq.filter (fun (i, _) -> i % 2 = 0)
    |> Seq.map (fun (i, d) -> (i / 2, int d))
    |> Map.ofSeq

fileSizes |> printfn "%A"

let inputData =
    inputText
    |> Seq.mapi (fun i d ->
        if i % 2 = 0 then
            [| 1 .. int d |] |> Array.map (fun _ -> Some(i / 2))
        else
            [| 1 .. int d |] |> Array.map (fun _ -> None))
    |> Seq.concat
    |> Seq.toArray

[<TailCall>]
let rec moveBlocksPtA (input: int option[]) : int option[] =
    if input |> Array.forall Option.isSome then
        input
    else
        let emptyIndex = input |> Array.findIndex Option.isNone

        let newArray =
            input
            |> Array.updateAt emptyIndex (input |> Array.last)
            |> Array.removeAt ((input |> Array.length) - 1)

        newArray |> moveBlocksPtA

let partA =
    inputData
    |> moveBlocksPtA
    |> Array.map Option.get
    |> Array.mapi (fun i v -> uint64 i * uint64 v)
    |> Array.sum

partA |> printfn "%d"
