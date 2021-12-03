let inputData =
    (System.IO.File.ReadAllText "Day3/input.txt")
        .Split "\n"
    |> Array.map Seq.toArray

let getGammaOrEpsilon pred (input: char [] []) =
    [| 0 .. input.[0].Length - 1 |]
    |> Array.map
        (fun index ->
            (input
             |> Array.countBy (fun charArray -> charArray.[index])
             |> pred snd))
    |> Array.map fst
    |> Array.map string
    |> String.concat ""

let getRating pred sortFunc (input: char [] []) =
    [| 0 .. input.[0].Length - 1 |]
    |> Array.fold
        (fun (remainingItems: char [] []) index ->
            let matchingVal =
                remainingItems
                |> Array.countBy (fun charArray -> charArray.[index])
                |> sortFunc fst
                |> pred snd
                |> fst

            remainingItems
            |> Array.filter (fun item -> item.[index] = matchingVal))
        input
    |> Array.item 0
    |> Array.map string
    |> String.concat ""

let partA (input: char [] []) =
    let gamma = input |> getGammaOrEpsilon Array.maxBy

    let epsilon = input |> getGammaOrEpsilon Array.minBy

    System.Convert.ToInt32(gamma, 2)
    * System.Convert.ToInt32(epsilon, 2)

let partB (input: char [] []) =
    let oxygen =
        input
        |> getRating Array.maxBy Array.sortByDescending

    let co2 =
        input |> getRating Array.minBy Array.sortBy

    System.Convert.ToInt32(oxygen, 2)
    * System.Convert.ToInt32(co2, 2)


printfn $"Part A: %d{inputData |> partA}"
printfn $"Part B: %d{inputData |> partB}"
