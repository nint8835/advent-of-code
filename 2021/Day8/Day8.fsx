let inputData =
    (System.IO.File.ReadAllText "Day8/input.txt")
        .Split "\n"
    |> Array.map
        (fun str ->
            str.Split " | "
            |> Array.map (fun substr -> substr.Split " "))

let allGivenVals = Array.concat inputData |> Array.concat
printfn $"%A{inputData}"

type DigitValues = Map<Set<char>, int>

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
        definiteDigits |> Map.findKey (fun k v -> v = 1)

    let four =
        definiteDigits |> Map.findKey (fun k v -> v = 4)

    let seven =
        definiteDigits |> Map.findKey (fun k v -> v = 7)

    let eight =
        definiteDigits |> Map.findKey (fun k v -> v = 8)

    let middleLine =
        (((((((Set.intersect (Set.difference eight one) (Set.difference four one)
               |> Set.toArray))))))).[0]

    let topLeftLine =
        ((((((((Set.remove middleLine (Set.difference four one))
               |> Set.toArray))))))).[0]

    let zero = Set.remove middleLine eight

    // TODO: The middle / top left line thing is messed up and giving an incorrect result for 3. Needs to be fixed

    let nineAndSix =
        allVals
        |> Array.map Set
        |> Array.map
            (fun inputSet ->
                if inputSet.Count <> 6 then
                    None
                else
                    let fourIntersection = Set.intersect four inputSet

                    match fourIntersection.Count with
                    | 4 -> Some(inputSet, 9)
                    | 3 ->
                        if Set.contains middleLine fourIntersection then
                            Some(inputSet, 6)
                        else
                            None
                    | _ -> failwith "this should be unreachable")
        |> Array.filter (Option.isSome)
        |> Array.map (fun option -> option.Value)
        |> Map

    let nine =
        nineAndSix |> Map.findKey (fun k v -> v = 9)

    let six =
        nineAndSix |> Map.findKey (fun k v -> v = 6)

    let twoFiveThree =
        allVals
        |> Array.map Set
        |> Array.map
            (fun inputSet ->
                if inputSet.Count <> 5 then
                    None
                else
                    let nineIntersection = Set.intersect nine inputSet
                    printfn $"9I: %A{nineIntersection} %A{topLeftLine} %A{Set.intersect seven inputSet}"
                   

                    match nineIntersection.Count with
                    | 5 ->
                        if (Set.intersect seven inputSet).Count = 3 then
                            Some(inputSet, 3)
                        else
                            Some(inputSet, 5)
                    | 4 -> Some(inputSet, 2)
                    | _ -> failwith "this should be unreachable")
        |> Array.filter (Option.isSome)
        |> Array.map (fun option -> option.Value)
        |> Map

    let two =
        twoFiveThree |> Map.findKey (fun k v -> v = 2)

    let five =
        twoFiveThree |> Map.findKey (fun k v -> v = 5)

    let three =
        twoFiveThree |> Map.findKey (fun k v -> v = 3)

    //    let bottomRightLine =
//        allVals
//        |> Array.map Set
//        |> Array.map (fun inputSet ->
//            if inputSet.Count <> 5 then return
//
//            Set.intersect inputSet seven)

    // 0 (6) = top, top left, top right, bottom left, bottom right, bottom - done
    // 1 (2) = top right, bottom right - done
    // 2 (5) = top, top right, middle, bottom left, bottom - done
    // 3 (5) = top, top right, middle, bottom right, bottom - done
    // 4 (4) = top left, top right, middle, bottom right - done
    // 5 (5) = top, top left, middle, bottom right, bottom -done
    // 6 (6) = top, top left, middle, bottom left, bottom right, bottom - done
    // 7 (3) = top, top right, bottom right - done
    // 8 (7) = top, top left, top right, middle, bottom left, bottom right, bottom - done
    // 9 (6) = top, top left, top right, middle, bottom right, bottom - done

    Map [ (one, 1)
          (two, 2)
          (three, 3)
          (four, 4)
          (five, 5)
          (six, 6)
          (seven, 7)
          (eight, 8)
          (nine, 9)
          (zero, 0) ]


let definiteDigits = allGivenVals |> getDefiniteDigits

let outputValues =
    inputData
    |> Array.map (fun row -> row.[1] |> Array.map Set)

let definiteDigitOutputCount =
    outputValues
    |> Array.concat
    |> Array.filter (definiteDigits.ContainsKey)
    |> Array.length

printfn $"Part A: %d{definiteDigitOutputCount}"

let digitValues =
    determineRemainingDigits definiteDigits allGivenVals

printfn $"%A{digitValues}"

let fiveDigitValues =
    digitValues |> Map.filter (fun k v -> k.Count = 5)

printfn $"%A{fiveDigitValues}"

let outputNumberSum =
    outputValues
    |> Array.map
        (fun value ->
            value
            |> Array.map
                (fun valSet ->
                    printfn $"%A{valSet}"
                    digitValues.[valSet])
            |> Array.map string
            |> String.concat ""
            |> int)
    |> Array.sum

printfn $"%A{outputNumberSum}"
