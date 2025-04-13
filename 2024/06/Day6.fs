module AdventOfCode.Solutions.Y2024.D06

open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (Seq.map string >> Seq.toArray)
    |> array2D

let gridWidth, gridHeight = inputData.GetLength 1, inputData.GetLength 0

let obstacles = inputData |> Array2D.findIndices ((=) "#") |> Set.ofArray

let initialGuardPosition =
    inputData |> Array2D.findIndices ((=) "^") |> Array.head

type GuardDirection =
    | Up
    | Down
    | Left
    | Right

let rotateGuard (direction: GuardDirection) : GuardDirection =
    match direction with
    | Up -> Right
    | Right -> Down
    | Down -> Left
    | Left -> Up

let getMoveOffset (direction: GuardDirection) : int * int =
    match direction with
    | Up -> (0, -1)
    | Down -> (0, 1)
    | Left -> (-1, 0)
    | Right -> (1, 0)

type SearchResult =
    { SeenPositions: Set<(int * int) * GuardDirection>
      IsLoop: bool }

[<TailCall>]
let rec performSearch
    (obstacles: Set<(int * int)>)
    (position: int * int)
    (direction: GuardDirection)
    (seenPositions: Set<(int * int) * GuardDirection>)
    : SearchResult =
    let offset = getMoveOffset direction
    let newPosition = Tuple.add2 position offset
    let newX, newY = newPosition

    if obstacles |> Set.contains newPosition then
        performSearch obstacles position (rotateGuard direction) seenPositions
    else if
        (newX < 0 || newX >= gridWidth) || (newY < 0 || newY >= gridHeight)
    then
        { SeenPositions = seenPositions
          IsLoop = false }
    else if seenPositions.Contains(newPosition, direction) then
        { SeenPositions = seenPositions
          IsLoop = true }
    else
        performSearch
            obstacles
            newPosition
            direction
            (seenPositions |> Set.add (newPosition, direction))


let visited =
    performSearch
        obstacles
        initialGuardPosition
        Up
        (Set.singleton (initialGuardPosition, Up))
    |> _.SeenPositions
    |> Set.map fst

let solve () : unit =
    visited |> Set.count |> printfn "%A"

    visited
    |> Set.toArray
    |> Array.Parallel.map (fun position ->
        performSearch
            (Set.add position obstacles)
            initialGuardPosition
            Up
            Set.empty)
    |> Array.filter _.IsLoop
    |> Array.length
    |> printfn "%A"
