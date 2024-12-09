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

let moveBlocksPtB (input: int option[]) (target: int) : int option[] =
    let availableGaps =
        input
        |> Array.indexed
        |> Array.filter (fun (i, v) -> v |> Option.isNone)
        |> Array.filter (fun (i, _) ->
            i < (input |> Array.findIndex (fun v -> v = Some target)))
        |> Array.map fst
        |> Array.map (fun i ->
            (i,
             input
             |> Array.skip i
             |> Array.takeWhile Option.isNone
             |> Array.length))

    let targetSize = fileSizes |> Map.find target

    let targetGap =
        availableGaps |> Array.tryFind (fun (_, gap) -> gap >= targetSize)

    match targetGap with
    | None -> input
    | Some(i, _) ->
        let targetRemovedArr =
            input
            |> Array.mapi (fun j v ->
                match v with
                | Some t when t = target -> None
                | _ -> v)

        [| i .. (i + targetSize - 1) |]
        |> Array.fold
            (fun arr j -> arr |> Array.updateAt j (Some target))
            targetRemovedArr


let partA =
    inputData
    |> moveBlocksPtA
    |> Array.map Option.get
    |> Array.mapi (fun i v -> uint64 i * uint64 v)
    |> Array.sum

let partB =
    fileSizes
    |> Map.keys
    |> Seq.toArray
    |> Array.sortDescending
    |> Array.fold moveBlocksPtB inputData
    |> Array.indexed
    |> Array.filter (fun (_, v) -> v |> Option.isSome)
    |> Array.map (fun (i, v) -> (uint64 i, uint64 (v |> Option.get)))
    |> Array.map (fun (i, v) -> i * v)
    |> Array.sum

partA |> printfn "%d"
partB |> printfn "%d"
