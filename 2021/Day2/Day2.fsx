type Position = { x: int; y: int; aim: int }
type Movement = { forward: int; up: int; down: int }

let inputData =
    (System.IO.File.ReadAllText "Day2/input.txt")
        .Split "\n"
    |> Array.map (fun s -> s.Split(" "))
    |> Array.map (fun item -> (item.[0], int item.[1]))

let getFinalPosition useAim (inputData: (string * int) []) : Position =
    inputData
    |> Array.map
        (fun (direction, amount) ->
            match direction with
            | "forward" -> { forward = amount; up = 0; down = 0 }
            | "up" -> { forward = 0; up = amount; down = 0 }
            | "down" -> { forward = 0; up = 0; down = amount }
            | _ -> failwith "Invalid direction")
    |> Array.fold
        (fun acc movement ->
            { x = acc.x + movement.forward
              y =
                  if not useAim then
                      acc.y - movement.up + movement.down
                  else
                      acc.y + movement.forward * acc.aim
              aim =
                  if not useAim then
                      acc.aim
                  else
                      acc.aim + movement.down - movement.up })
        { x = 0
          y = 0
          aim = if not useAim then 1 else 0 }

let partA = inputData |> getFinalPosition false
let partB = inputData |> getFinalPosition true
let partAProduct = partA.x * partA.y
let partBProduct = partB.x * partB.y

printfn $"Part A: %d{partAProduct}"
printfn $"Part B: %d{partBProduct}"
