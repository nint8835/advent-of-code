module AdventOfCode.Solutions.Y2023.D06

open System

let partAInput =
    System.IO.File.ReadAllText("input.txt").Split "\n"
    |> Array.map (fun line ->
        line.Split " "
        |> Array.filter ((<>) "")
        |> Array.skip 1
        |> Array.map uint64)
    |> (fun arrays -> Array.zip arrays[0] arrays[1])

let partBInput =
    System.IO.File.ReadAllText("input.txt").Split "\n"
    |> Array.map (fun line ->
        line |> Seq.filter Char.IsDigit |> Seq.map string |> Seq.reduce (+))
    |> Array.map uint64
    |> (fun numbers -> numbers[0], numbers[1])

let calculatePossibleWinningMethods (time, distance) =
    seq { 0UL .. time }
    |> Seq.toArray
    |> Array.map (fun heldTime -> (heldTime * (time - heldTime)))
    |> Array.filter (fun methodDistance -> methodDistance > distance)
    |> Array.length

let solve () =
    let partA =
        partAInput
        |> Array.map calculatePossibleWinningMethods
        |> Array.reduce (*)

    let partB = partBInput |> calculatePossibleWinningMethods

    printfn "%A" partA
    printfn "%A" partB
