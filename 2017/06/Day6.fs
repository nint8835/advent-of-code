module AdventOfCode.Solutions.Y2017.D06

open System.IO
open AdventOfCode.Solutions.Utils

let inputData =
    File.ReadAllText "input.txt" |> String.split "\t" |> Array.map int

[<TailCall>]
let rec redistribute
    (index: int)
    (blocks: int)
    (memory: int array)
    : int array =
    if blocks = 0 then
        memory
    else
        redistribute
            ((index + 1) % memory.Length)
            (blocks - 1)
            (memory |> Array.updateAt index (memory[index] + 1))

[<TailCall>]
let rec performReallocation
    (memory: int array)
    (seen: int array array)
    : int array * int array array =
    if seen |> Array.contains memory then
        memory, seen
    else
        let maxBlocks = memory |> Array.max
        let maxIndex = memory |> Array.findIndex ((=) maxBlocks)

        let updatedMemory =
            memory
            |> Array.updateAt maxIndex 0
            |> redistribute ((maxIndex + 1) % memory.Length) maxBlocks

        performReallocation updatedMemory (seen |> Array.add memory)

let solve () =
    let memory, seen = performReallocation inputData [||]
    let seenCount = seen |> Array.length

    seenCount |> printfn "%d"

    let firstSeenIndex = seen |> Array.findIndex ((=) memory)

    seenCount - firstSeenIndex |> printfn "%d"
