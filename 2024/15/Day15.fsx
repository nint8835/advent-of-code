// aoc-tools:stars 1
#load "../../utils/Utils.fsx"

open System.IO
open Utils

let gridLines, movementLines =
    File.ReadAllText "input.txt" |> String.split "\n\n" |> Array.toTuple2

let grid =
    gridLines
    |> String.split "\n"
    |> Array.map (Seq.map string >> Seq.toArray)
    |> array2D

let wallLocations = grid |> Array2D.findIndices ((=) "#") |> Set.ofArray
let boxLocations = grid |> Array2D.findIndices ((=) "O") |> Set.ofArray
let robotLocation = grid |> Array2D.findIndices ((=) "@") |> Array.head

let movements =
    movementLines
    |> String.split "\n"
    |> Array.map (Seq.map string >> Seq.toArray)
    |> Array.concat

let getOffset (movement: string) : int * int =
    match movement with
    | "^" -> 0, -1
    | "v" -> 0, 1
    | "<" -> -1, 0
    | ">" -> 1, 0
    | _ -> failwith "Invalid movement"

let rec findBoxLineEnd
    (offset: int * int)
    (boxLocations: Set<int * int>)
    (currentLocation: int * int)
    : int * int =
    let newLocation = Tuple.add2 currentLocation offset

    if boxLocations |> Set.contains newLocation then
        findBoxLineEnd offset boxLocations newLocation
    else
        currentLocation

let moveRobot
    ((boxLocations: Set<int * int>, (robotLocation: int * int)))
    (movement: string)
    : Set<int * int> * (int * int) =
    printfn "%A" robotLocation

    let offset = getOffset movement
    let newLocation = Tuple.add2 robotLocation offset

    if wallLocations |> Set.contains newLocation then
        boxLocations, robotLocation
    else if not (boxLocations |> Set.contains newLocation) then
        boxLocations, newLocation
    else
        let boxLineEnd = findBoxLineEnd offset boxLocations newLocation
        printfn "Box line end: %A" boxLineEnd

        if wallLocations |> Set.contains (Tuple.add2 boxLineEnd offset) then
            boxLocations, robotLocation
        else
            boxLocations
            |> Set.remove newLocation
            |> Set.add (Tuple.add2 boxLineEnd offset),
            newLocation

movements
|> Array.fold moveRobot (boxLocations, robotLocation)
|> fst
|> Set.toArray
|> Array.map (fun (x, y) -> 100 * y + x)
|> Array.sum
|> printfn "%A"
