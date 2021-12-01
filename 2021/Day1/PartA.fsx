let inputData =
    (System.IO.File.ReadAllText "Day1/input.txt")
        .Split "\n"
    |> Array.map (int)

let increased =
    inputData
    |> Array.windowed 2
    |> Array.map (fun window -> window[1] > window[0])
    |> Array.filter (fun x -> x)
    |> Array.length

printfn "%d" increased
