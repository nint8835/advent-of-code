open System

let inputData =
    (System.IO.File.ReadAllText "Day7/input.txt")
        .Split ","
    |> Array.map int

/// Mode of calculating movement costs.
type CostMode =
    /// Costs are linear.
    | Linear
    /// Costs are represented as the sum from k=1 to n of k.
    | Parabolic

type PositionCost = { cost: int; position: int }

/// Calculate the cost to get from endPosition to startPosition with a given cost mode.
let getFuelCost (startPosition: int) (endPosition: int) (costMode: CostMode) : int =
    let difference = Math.Abs(endPosition - startPosition)

    match costMode with
    | CostMode.Linear -> difference
    | CostMode.Parabolic -> (difference * (difference + 1)) / 2

/// Calculate the total cost to get a list of positions to a target position.
let calculateTotalCost (costMode: CostMode) (startingPositions: int []) (targetPosition: int) : PositionCost =
    startingPositions
    |> Array.map
        (fun startPosition ->
            { cost = getFuelCost startPosition targetPosition costMode
              position = targetPosition })
    |> Array.fold
        (fun acc position ->
            { acc with
                  cost = acc.cost + position.cost })
        { position = targetPosition; cost = 0 }

/// Find the cheapest position for a given cost mode.
let calculateCheapestPosition (costMode: CostMode) (startingPositions: int []) : PositionCost =
    startingPositions
    |> Array.map (calculateTotalCost costMode startingPositions)
    |> Array.minBy (fun position -> position.cost)

let partA =
    inputData
    |> calculateCheapestPosition CostMode.Linear

let partB =
    inputData
    |> calculateCheapestPosition CostMode.Parabolic

printfn $"%A{partA}"
printfn $"%A{partB}"
