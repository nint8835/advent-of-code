open System
open System.Text.RegularExpressions

type PossiblePartNumber =
    { PartNumber: int
      AdjacencyBoxStart: int * int
      AdjacencyBoxEnd: int * int }

let partNumberRegex = Regex(@"(\d+)")

let inputData = System.IO.File.ReadAllText("input.txt").Split "\n"

let gridWidth, gridHeight = inputData[0].Length, inputData.Length

let possiblePartNumbers =
    inputData
    |> Array.mapi (fun y line ->
        line
        |> partNumberRegex.Matches
        |> Seq.filter _.Success
        |> Seq.map (fun partMatch ->
            { PartNumber = int partMatch.Value
              AdjacencyBoxStart =
                Math.Max(partMatch.Index - 1, 0), Math.Max(y - 1, 0)
              AdjacencyBoxEnd =
                Math.Min(partMatch.Index + partMatch.Length, gridWidth - 1),
                Math.Min(y + 1, gridHeight - 1) })
        |> Seq.toArray)
    |> Array.concat

let partNumbers =
    possiblePartNumbers
    |> Array.filter (fun partNumber ->
        let boxStartX, boxStartY = partNumber.AdjacencyBoxStart
        let boxEndX, boxEndY = partNumber.AdjacencyBoxEnd

        let box =
            [ for y in boxStartY..boxEndY do
                  for x in boxStartX..boxEndX do
                      yield inputData[y].[x] ]
            |> List.toArray

        box
        |> Array.filter ((<>) '.')
        |> Array.filter (fun c -> not (Char.IsDigit c))
        |> Array.length
        |> (<>) 0)

let gears =
    inputData
    |> Array.mapi (fun y line ->
        line
        |> Seq.mapi (fun x c ->
            if c <> '*' then
                None
            else
                let connectedPartNumbers =
                    partNumbers
                    |> Array.filter (fun partNumber ->
                        let boxX, boxY = partNumber.AdjacencyBoxStart
                        let boxEndX, boxEndY = partNumber.AdjacencyBoxEnd

                        x >= boxX
                        && x <= boxEndX
                        && y >= boxY
                        && y <= boxEndY)

                if connectedPartNumbers.Length <> 2 then
                    None
                else
                    Some(
                        connectedPartNumbers
                        |> Array.map _.PartNumber
                        |> Array.fold (*) 1
                    )

        )
        |> Seq.toArray)
    |> Array.concat


let partA = partNumbers |> Array.map _.PartNumber |> Array.sum

let partB =
    gears |> Array.filter Option.isSome |> Array.map Option.get |> Array.sum

printfn "%A" partA
printfn "%A" partB
