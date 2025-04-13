module AdventOfCode.Solutions.Y2023.D08

open System.Text.RegularExpressions

// lcm implementation taken from https://gist.github.com/krishnabhargav/da6686e295638d000aab
let rec gcd a b =
    match (a, b) with
    | (x, y) when x = y -> x
    | (x, y) when x > y -> gcd (x - y) y
    | (x, y) -> gcd x (y - x)

let lcm a b = a * b / (gcd a b)

let networkLineRegex =
    Regex(@"^([A-Z0-9]{3}) = \(([A-Z0-9]{3}), ([A-Z0-9]{3})\)$")

let inputData = System.IO.File.ReadAllText("input.txt").Split "\n"

let instructions = inputData[0]

let network =
    inputData
    |> Array.skip 2
    |> Array.map (fun line ->
        let matches = networkLineRegex.Match(line)

        matches.Groups[1].Value,
        matches.Groups[2].Value,
        matches.Groups[3].Value)
    |> Array.fold
        (fun network (source, left, right) ->
            network |> Map.add source (left, right))
        Map.empty

let applyInstruction
    (network: Map<string, string * string>)
    currentStep
    currentLocation
    =
    let instruction = instructions[currentStep % instructions.Length]

    match instruction with
    | 'L' -> fst network[currentLocation]
    | 'R' -> snd network[currentLocation]
    | _ -> failwith "Invalid instruction"

let rec walkNetwork
    (network: Map<string, string * string>)
    (successChecker: string -> bool)
    (currentStep: int)
    (currentLocation: string)
    : int =
    if successChecker currentLocation then
        currentStep
    else
        let nextLocation = applyInstruction network currentStep currentLocation

        walkNetwork network successChecker (currentStep + 1) nextLocation

let solve () =
    let partA = walkNetwork network ((=) "ZZZ") 0 "AAA"

    let partB =
        network
        |> Map.keys
        |> Seq.filter (fun key -> key.EndsWith "A")
        |> Seq.toArray
        |> Array.map (
            walkNetwork network (fun location -> location.EndsWith "Z") 0
        )
        |> Array.map uint64
        |> Array.reduce lcm

    printfn "%A" partA
    printfn "%A" partB
