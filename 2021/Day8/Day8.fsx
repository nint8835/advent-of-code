let inputData =
    (System.IO.File.ReadAllText "Day8/input.txt")
        .Split "\n"
    |> Array.map
        (fun str ->
            str.Split " | "
            |> Array.map (fun substr -> substr.Split " "))

let allGivenVals = Array.concat inputData |> Array.concat

type DigitValues = Map<Set<char>, int>

let getIntersectionCount (a: Set<'T>) (b: Set<'T>) : int = Set.intersect a b |> Set.count

let getDefiniteDigits (inputs: string []) : DigitValues =
    inputs
    |> Array.map Set
    |> Array.map
        (fun input ->
            input
            |> Set
            |> Set.count
            |> function
                | 2 -> Some(input, 1)
                | 3 -> Some(input, 7)
                | 4 -> Some(input, 4)
                | 7 -> Some(input, 8)
                | _ -> None)
    |> Array.filter Option.isSome
    |> Array.map (fun option -> option.Value)
    |> Map

let determineRemainingDigits (definiteDigits: DigitValues) (allVals: string []) : DigitValues =
    let one =
        definiteDigits |> Map.findKey (fun _ v -> v = 1)

    let four =
        definiteDigits |> Map.findKey (fun _ v -> v = 4)

    let seven =
        definiteDigits |> Map.findKey (fun _ v -> v = 7)

    let eight =
        definiteDigits |> Map.findKey (fun _ v -> v = 8)

    allVals
    |> Array.map Set
    |> Array.map
        (fun value ->
            (value,
             [| one; four; seven; eight |]
             |> Array.map (getIntersectionCount value)
             |> Array.map string
             |> String.concat ""
             |> int))
    |> Array.map
        (fun (charSet, intersections) ->
            match intersections with
            | 2222 -> (charSet, 1)
            | 1225 -> (charSet, 2)
            | 2335 -> (charSet, 3)
            | 2424 -> (charSet, 4)
            | 1325 -> (charSet, 5)
            | 1326 -> (charSet, 6)
            | 2233 -> (charSet, 7)
            | 2437 -> (charSet, 8)
            | 2436 -> (charSet, 9)
            | 2336 -> (charSet, 0)
            | _ -> failwithf $"Unexpected intersection number %d{intersections} for charset %A{charSet}")
    |> Map

let calculateDigitValues (row: string [] []) : (int * int) =
    let definiteDigits = row |> Array.concat |> getDefiniteDigits

    let outputValues = row.[1] |> Array.map Set

    let definiteDigitOutputCount =
        outputValues
        |> Array.filter (definiteDigits.ContainsKey)
        |> Array.length

    let digitValues =
        determineRemainingDigits definiteDigits (Array.concat row)

    let outputNumber =
        outputValues
        |> Array.map (fun outVal -> digitValues.[outVal])
        |> Array.map string
        |> String.concat ""
        |> int

    (definiteDigitOutputCount, outputNumber)

let inputResults =
    inputData |> Array.map calculateDigitValues

let definiteDigits = inputResults |> Array.sumBy fst
let outputSum = inputResults |> Array.sumBy snd

printfn $"Part A: %d{definiteDigits}"
printfn $"Part B: %d{outputSum}"
