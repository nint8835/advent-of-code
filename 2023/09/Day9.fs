module AdventOfCode.Solutions.Y2023.D09

let inputData =
    System.IO.File.ReadAllText("input.txt").Split "\n"
    |> Array.map (fun line -> line.Split " " |> Array.map int)

type ExtrapolationLocation =
    | Beginning
    | End

let rec extrapolateValue
    (location: ExtrapolationLocation)
    (values: int array)
    : int =
    if values |> Array.filter ((<>) 0) |> Array.length = 0 then
        0
    else
        let nextLine =
            values
            |> Array.windowed 2
            |> Array.map (fun pair -> pair[1] - pair[0])

        let extrapolatedNextLineValue = extrapolateValue location nextLine

        match location with
        | Beginning -> (values |> Array.head) - extrapolatedNextLineValue
        | End -> (values |> Array.last) + extrapolatedNextLineValue

let solve () =
    let partA = inputData |> Array.map (extrapolateValue End) |> Array.sum
    let partB = inputData |> Array.map (extrapolateValue Beginning) |> Array.sum

    printfn "%A" partA
    printfn "%A" partB
