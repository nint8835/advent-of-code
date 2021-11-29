let inputData =
    (System.IO.File.ReadAllText "Day0/input.txt")
        .Split "\n"
    |> Array.map (int)

for val1Index = 0 to inputData.Length - 1 do
    for val2Index = val1Index + 1 to inputData.Length - 1 do
        for val3Index = val2Index + 1 to inputData.Length - 1 do
            let val1 = inputData[val1Index]
            let val2 = inputData[val2Index]
            let val3 = inputData[val3Index]

            if val1 + val2 + val3 = 2020 then
                printfn "%d" (val1 * val2 * val3)
