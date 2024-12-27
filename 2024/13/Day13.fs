module AdventOfCode.Solutions.Y2024.D13

open System.IO
open System.Text.RegularExpressions
open AdventOfCode.Solutions.Utils

let coordinateRegex = Regex(@"[+=](\d+)")

type Machine =
    { A: float * float
      B: float * float
      Prize: float * float }

let solveSystem
    ((aX, aY): float * float)
    ((bX, bY): float * float)
    ((pX, pY): float * float)
    : float * float =
    ((pX * bY - pY * bX) / (aX * bY - aY * bX),
     (aX * pY - aY * pX) / (aX * bY - aY * bX))

let isInteger (v: float) : bool = abs (v - floor v) < 1e-10


let inputData =
    File.ReadAllText "input.txt"
    |> String.split "\n\n"
    |> Array.map (fun machine ->
        let [| a; b; prize |] =
            machine
            |> String.split "\n"
            |> Array.map (fun line ->
                let matches = coordinateRegex.Matches line

                (float matches[0].Groups.[1].Value,
                 float matches[1].Groups.[1].Value))

        { A = a; B = b; Prize = prize })

let findTokens (prizeCoordinateMutator: float -> float) : float =
    inputData
    |> Array.map (fun machine ->
        solveSystem
            machine.A
            machine.B
            (prizeCoordinateMutator (fst machine.Prize),
             prizeCoordinateMutator (snd machine.Prize)),
        machine)
    |> Array.filter (fun ((aPresses, bPresses), _) ->
        aPresses >= 0.0
        && bPresses >= 0.0
        && isInteger aPresses
        && isInteger bPresses)
    |> Array.sumBy (fun ((aPresses, bPresses), _) ->
        (3.0 * aPresses + bPresses))

let solve () : unit =
    findTokens id |> uint64 |> printfn "%d"
    findTokens (fun v -> v + 10000000000000.0) |> uint64 |> printfn "%d"
