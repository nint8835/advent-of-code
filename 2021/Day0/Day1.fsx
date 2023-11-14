let inputData =
    (System.IO.File.ReadAllText "Day0/input.txt")
        .Split "\n"
    |> Array.map int

let rec generateGroups (size: int) (array: int []) : int [] [] =
    array
    |> Array.mapi
        (fun i x ->
            array.[i + 1..]
            |> match size with
               | 2 -> Array.map (fun y -> [| x; y |])
               | n ->
                   fun subArr ->
                       subArr
                       |> generateGroups (n - 1)
                       |> Array.map (Array.append [| x |]))
    |> Array.concat

let calculateGroupSolution (size: int) (array: int []) : int =
    array
    |> generateGroups size
    |> Array.find (fun group -> Array.sum group = 2020)
    |> Array.reduce (*)

let partA = inputData |> calculateGroupSolution 2

let partB = inputData |> calculateGroupSolution 3

printfn $"Part A: %d{partA}"
printfn $"Part B: %d{partB}"
