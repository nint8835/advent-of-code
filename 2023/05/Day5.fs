// aoc-tools:stars 1
module AdventOfCode.Solutions.Y2023.D05

type Range =
    { Destination: uint64 * uint64
      Source: uint64 * uint64 }

let inputData = System.IO.File.ReadAllText("input.txt").Split "\n\n"

let seeds = ((inputData[0].Split ": ")[1]).Split " " |> Array.map uint64

let rangeGroups =
    inputData
    |> Array.skip 1
    |> Array.map (fun group ->
        group.Split "\n"
        |> Array.skip 1
        |> Array.map (fun line ->
            let elements = line.Split " " |> Array.map uint64

            { Destination = elements[0], elements[0] + elements[2] - 1UL
              Source = elements[1], elements[1] + elements[2] - 1UL }))

let solve () =
    let partA =
        seeds
        |> Array.map (fun seed ->
            rangeGroups
            |> Array.fold
                (fun value rangeGroup ->
                    let targetRange =
                        rangeGroup
                        |> Array.tryFind (fun range ->
                            (fst range.Source) <= value
                            && value <= (snd range.Source))

                    match targetRange with
                    | None -> value
                    | Some targetRange ->
                        value - (fst targetRange.Source)
                        + (fst targetRange.Destination))
                seed)
        |> Array.min

    printfn "%A" partA
