module AdventOfCode.Solutions.Y2023.D01

let inputData = System.IO.File.ReadAllText("input.txt").Split "\n"

let textDigits =
    [| ("one", "1")
       ("two", "2")
       ("three", "3")
       ("four", "4")
       ("five", "5")
       ("six", "6")
       ("seven", "7")
       ("eight", "8")
       ("nine", "9") |]

type TargetString =
    { text: string
      digit: string
      index: int }

type Direction =
    | LeftToRight
    | RightToLeft

type DirectionFuncs =
    { sorter: (TargetString -> int) -> TargetString[] -> TargetString[]
      indexer: string -> string -> int }

let directionFuncs =
    Map
        [ (LeftToRight,
           { sorter = Array.sortBy
             indexer = _.IndexOf })
          (RightToLeft,
           { sorter = Array.sortByDescending
             indexer = _.LastIndexOf }) ]

let rec replaceStringDigits (direction: Direction) (str: string) =
    let targets =
        textDigits
        |> Array.map (fun (text, digit) ->
            { text = text
              digit = digit
              index = (directionFuncs[direction].indexer str text) })
        |> Array.filter (fun target -> target.index <> -1)
        |> directionFuncs[direction].sorter _.index

    if targets.Length = 0 then
        str
    else
        let target = targets[0]
        let newStr = str.Replace(target.text, target.digit)
        replaceStringDigits direction newStr

let findDigitChars (str: string) =
    str |> Seq.filter System.Char.IsDigit |> Seq.map string |> Seq.toArray

let getCalibrationValues
    (replacer: Direction -> string -> string)
    (input: string[])
    =
    input
    |> Array.map (fun line ->
        let ltrLine = line |> replacer LeftToRight
        let rtlLine = line |> replacer RightToLeft

        let first = ltrLine |> findDigitChars |> Array.head
        let last = rtlLine |> findDigitChars |> Array.last

        int (first + last))

let solve () =
    let partA = inputData |> getCalibrationValues (fun _ str -> str) |> Array.sum
    let partB = inputData |> getCalibrationValues replaceStringDigits |> Array.sum

    printfn $"{partA}"
    printfn $"{partB}"
