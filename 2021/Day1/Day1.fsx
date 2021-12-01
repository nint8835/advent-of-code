let inputData =
    (System.IO.File.ReadAllText "Day1/input.txt")
        .Split "\n"
    |> Array.map (int)

let getIncreasingWindows windowSize (array: int []) =
    array
    |> Array.windowed (windowSize + 1)
    |> Array.map
        (fun window ->
            window
            |> Array.windowed windowSize
            |> Array.map (Array.sum))
    |> Array.filter (fun window -> window.[1] > window.[0])
    |> Array.length

let partA = inputData |> getIncreasingWindows 1
let partB = inputData |> getIncreasingWindows 3

printfn $"Part A: %d{partA}"
printfn $"Part B: %d{partB}"
