#load "../../utils/Utils.fsx"

open System.IO
open Utils
open System.Text.RegularExpressions

let coordinateRegex = Regex(@"[+=](\d+)")

type Machine =
    { A: int * int
      B: int * int
      Prize: int * int }

let inputData =
    File.ReadAllText "input.txt"
    |> String.split "\n\n"
    |> Array.map (fun machine ->
        let [| a; b; prize |] =
            machine
            |> String.split "\n"
            |> Array.map (fun line ->
                let matches = coordinateRegex.Matches line

                (int matches[0].Groups.[1].Value,
                 int matches[1].Groups.[1].Value))

        { A = a; B = b; Prize = prize })

inputData
|> Array.map (fun machine ->
    [| 0..100 |]
    |> Array.map (fun aPresses ->
        [| 0..100 |] |> Array.map (fun bPresses -> (aPresses, bPresses)))
    |> Array.concat
    |> Array.filter (fun (aPresses, bPresses) ->
        (fst machine.A * aPresses + fst machine.B * bPresses) = fst
            machine.Prize
        && (snd machine.A * aPresses + snd machine.B * bPresses) = snd
            machine.Prize)
    |> Array.map (fun (aPresses, bPresses) -> 3 * aPresses + bPresses)
    |> Array.sort
    |> Array.tryHead)
|> Array.filter Option.isSome
|> Array.map Option.get
|> Array.sum
|> printfn "%A"
