type Bracket =
    | Round
    | Square
    | Curly
    | Angle

type Token =
    | Open of Bracket
    | Close of Bracket

let tokenizeString (str: string) : Token list =
    str
    |> Seq.map
        (function
        | '[' -> Open Square
        | ']' -> Close Square
        | '(' -> Open Round
        | ')' -> Close Round
        | '{' -> Open Curly
        | '}' -> Close Curly
        | '<' -> Open Angle
        | '>' -> Close Angle
        | char -> failwithf $"Invalid token %c{char}")
    |> Seq.toList

let rec valueError (line: Token list) : int option =
    let mutable stack = []

    try
        line
        |> List.map
            (fun token ->
                match token with
                | Open _ ->
                    stack <- stack @ [ token ]
                    None
                | Close bracket ->
                    let lastBracket = stack.[stack.Length - 1]

                    match lastBracket with
                    | Open openToken ->
                        if bracket = openToken then
                            stack <- stack.[..stack.Length - 2]
                            None
                        else
                            match bracket with
                            | Round -> Some(3)
                            | Square -> Some(57)
                            | Curly -> Some(1197)
                            | Angle -> Some(25137)
                    | n -> failwithf $"Unexpected token %A{n}")
        |> List.find Option.isSome
    with
    | :? System.Collections.Generic.KeyNotFoundException -> None

let rec valueAutocomplete (line: Token list) : int64 =
    let mutable stack = []

    line
    |> List.iter
        (fun token ->
            match token with
            | Open _ -> stack <- stack @ [ token ]
            | Close _ ->
                let lastBracket = stack.[stack.Length - 1]

                match lastBracket with
                | Open _ -> stack <- stack.[..stack.Length - 2]
                | n -> failwithf $"Unexpected token %A{n}")

    stack
    |> List.rev
    |> List.fold
        (fun acc token ->
            acc * 5L
            + match token with
              | Open bracket ->
                  match bracket with
                  | Round -> 1L
                  | Square -> 2L
                  | Curly -> 3L
                  | Angle -> 4L
              | Close _ -> failwith "Got close")
        0L


let inputData =
    (System.IO.File.ReadAllText "Day10/input.txt")
        .Split "\n"
    |> Array.map tokenizeString

let errorSum =
    inputData
    |> Array.map valueError
    |> Array.filter Option.isSome
    |> Array.map (fun opt -> opt.Value)
    |> Array.sum

let corrections =
    inputData
    |> Array.filter (fun line -> line |> valueError |> Option.isNone)
    |> Array.map valueAutocomplete
    |> Array.sort

let middleCorrection = corrections.[corrections.Length / 2]

printfn $"Part A: %d{errorSum}"
printfn $"Part B: %d{middleCorrection}"
