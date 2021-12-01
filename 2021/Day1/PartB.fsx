let inputData =
    (System.IO.File.ReadAllText "Day1/input.txt")
        .Split "\n"
    |> Array.map (int)

let increasingWindows =
    Array.zip
        (inputData[0 .. inputData.Length - 2]
         |> Array.windowed 3
         |> Array.map (Array.sum))
        (inputData[1 .. inputData.Length]
         |> Array.windowed 3
         |> Array.map (Array.sum))
    |> Array.map (fun (a, b) -> b > a)
    |> Array.filter (fun x -> x)
    |> Array.length

printfn "%d" increasingWindows
