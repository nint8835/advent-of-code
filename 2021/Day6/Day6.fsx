let inputData =
    (System.IO.File.ReadAllText "Day6/input.txt")
        .Split ","
    |> Array.map int

let updateAt (index: int) (value: 'T) (array: 'T []) : 'T [] =
    let newArray = Array.copy array
    Array.set newArray index value
    newArray

let fishCount =
    inputData
    |> Array.fold (fun acc fish -> acc |> updateAt fish (acc.[fish] + 1L)) [| 0L; 0L; 0L; 0L; 0L; 0L; 0L; 0L; 0L |]

let rec simulateLanternFish (days: int) (fish: int64 []) =
    match days with
    | 0 -> fish
    | n ->
        [| 0 .. 8 |]
        |> Array.fold
            (fun acc i ->
                match i with
                | 0 ->
                    acc
                    |> updateAt 0 (acc.[0] - fish.[0])
                    |> updateAt 6 (acc.[6] + fish.[0])
                    |> updateAt 8 (acc.[8] + fish.[0])
                | n ->
                    acc
                    |> updateAt n (acc.[n] - fish.[n])
                    |> updateAt (n - 1) (acc.[n - 1] + fish.[n]))
            fish
        |> simulateLanternFish (n - 1)

let partAFish = fishCount |> simulateLanternFish 80 |> Array.sum
let partBFish = fishCount |> simulateLanternFish 256 |> Array.sum

printfn $"Part A: %d{partAFish}"
printfn $"Part B: %d{partBFish}"
