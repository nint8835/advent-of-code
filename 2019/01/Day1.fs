module AdventOfCode.Solutions.Y2019.D01

open System.IO

let inputData = File.ReadAllLines "input.txt" |> Array.map int

[<TailCall>]
let rec calculateTotalFuel (accumulatedFuel: int) (mass: int) : int =
    let fuel = mass / 3 - 2

    if fuel <= 0 then
        accumulatedFuel
    else
        calculateTotalFuel (accumulatedFuel + fuel) fuel


let solve () =
    inputData
    |> Array.map (fun mass -> mass / 3 - 2)
    |> Array.sum
    |> printfn "%d"

    inputData |> Array.map (calculateTotalFuel 0) |> Array.sum |> printfn "%d"
