#load "../../utils/Utils.fsx"

open System.IO
open Utils
open System.Text.RegularExpressions

let coordinateRegex = Regex(@"[+=](\d+)")

type Machine =
    { A: int64 * int64
      B: int64 * int64
      Prize: int64 * int64 }

let solveSystem
    (a: int64 * int64)
    (b: int64 * int64)
    (prize: int64 * int64)
    : int64 * int64 =
    let x1, x2 = fst a, fst b
    let y1, y2 = snd a, snd b
    let pX, pY = fst prize, snd prize

    let det = x1 * y2 - x2 * y1
    let detX = pX * y2 - x2 * pY
    let detY = x1 * pY - pX * y1

    detX / det, detY / det


let inputData =
    File.ReadAllText "input.txt"
    |> String.split "\n\n"
    |> Array.map (fun machine ->
        let [| a; b; prize |] =
            machine
            |> String.split "\n"
            |> Array.map (fun line ->
                let matches = coordinateRegex.Matches line

                (int64 matches[0].Groups.[1].Value,
                 int64 matches[1].Groups.[1].Value))

        { A = a; B = b; Prize = prize })

let findTokens (prizeCoordinateMutator: int64 -> int64) : int =
    inputData
    |> Array.map (fun machine ->
        solveSystem
            machine.A
            machine.B
            (prizeCoordinateMutator (fst machine.Prize),
             prizeCoordinateMutator (snd machine.Prize)),
        machine)
    |> Array.filter (fun ((aPresses, bPresses), machine) ->
        aPresses >= 0L
        && bPresses >= 0L
        && (aPresses * fst machine.A + bPresses * fst machine.B) = fst
            machine.Prize
        && (aPresses * snd machine.A + bPresses * snd machine.B) = snd
            machine.Prize)
    |> Array.sumBy (fun ((aPresses, bPresses), _) ->
        int (3L * aPresses + bPresses))

findTokens id |> printfn "%d"
findTokens (fun v -> v + 10000000000000L) |> printfn "%d"
