module AdventOfCode.Solutions.Y2025.D03

open System.IO

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (Seq.map (string >> int) >> Seq.toArray)

[<TailCall>]
let rec findBankMaxValue
    (batterySize: int)
    (currentMaxValue: string)
    (bank: int[])
    : int64 =
    let indexedBank = bank |> Array.mapi (fun i v -> (i, v))

    if batterySize = 0 then
        int64 currentMaxValue
    else
        let (maxValuePosition, maxValue) =
            indexedBank
            |> Array.filter (fun (i, _) -> i <= bank.Length - batterySize)
            |> Array.maxBy snd

        findBankMaxValue
            (batterySize - 1)
            (currentMaxValue + string maxValue)
            (indexedBank
             |> Array.filter (fun (i, _) -> i > maxValuePosition)
             |> Array.map snd)

let solve () =
    inputData |> Array.sumBy (findBankMaxValue 2 "") |> printfn "%d"
    inputData |> Array.sumBy (findBankMaxValue 12 "") |> printfn "%d"
