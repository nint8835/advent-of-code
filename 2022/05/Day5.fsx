#load "../../utils/Utils.fsx"

open System.IO
open Utils

let inputData = File.ReadAllText "input.txt" |> String.split "\n\n"

type Instruction =
    { count: int
      from_col: int
      to_col: int }

let applyMovement
    (reverse: bool)
    (grid: char[][])
    (instruction: Instruction)
    : char[][] =
    grid
    |> Array.updateAt
        (instruction.from_col - 1)
        grid[instruction.from_col - 1].[instruction.count ..]
    |> Array.updateAt
        (instruction.to_col - 1)
        (Array.concat
            [| grid[instruction.from_col - 1].[0 .. instruction.count - 1]
               |> (if reverse then Array.rev else id)
               grid[instruction.to_col - 1] |])

let getTopString
    (reverse: bool)
    (instructions: Instruction[])
    (grid: char[][])
    : string =
    instructions
    |> Array.fold (applyMovement reverse) grid
    |> Array.map Array.head
    |> Array.map string
    |> String.concat ""

let gridLines = inputData |> Array.head |> String.split "\n"

let stackCount =
    Array.last gridLines
    |> String.split " "
    |> Array.filter (fun index -> index.Length <> 0)
    |> Array.map int
    |> Array.last

let grid: char[][] =
    [| 1..stackCount |]
    |> Array.map (fun columnNum ->
        gridLines[.. gridLines.Length - 2]
        |> Array.map (fun line -> line[4 * (columnNum - 1) + 1])
        |> Array.filter ((<>) ' '))

let instructions =
    inputData[1].Split "\n"
    |> Array.map (String.split " ")
    |> Array.map (fun instruction ->
        { count = int instruction[1]
          from_col = int instruction[3]
          to_col = int instruction[5] })

getTopString true instructions grid |> printfn "%s"
getTopString false instructions grid |> printfn "%s"
