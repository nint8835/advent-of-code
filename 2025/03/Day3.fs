module AdventOfCode.Solutions.Y2025.D03

open System.IO

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (Seq.map (string >> int64) >> Seq.toArray)

[<TailCall>]
let rec findBankMaxValue
    (batterySize: int)
    (currentMaxValue: int64)
    (bank: int64[])
    : int64 =
    let indexedBank = bank |> Array.indexed

    if batterySize = 0 then
        currentMaxValue
    else
        let (maxValuePosition, maxValue) =
            indexedBank
            |> Array.filter (fun (i, _) -> i <= bank.Length - batterySize)
            |> Array.maxBy snd

        findBankMaxValue
            (batterySize - 1)
            (currentMaxValue * 10L + maxValue)
            (indexedBank
             |> Array.filter (fun (i, _) -> i > maxValuePosition)
             |> Array.map snd)

let solve () =
    inputData |> Array.sumBy (findBankMaxValue 2 0L) |> printfn "%d"
    inputData |> Array.sumBy (findBankMaxValue 12 0L) |> printfn "%d"
