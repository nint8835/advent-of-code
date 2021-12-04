let inputData =
    (System.IO.File.ReadAllText "Day4/input.txt")
        .Split "\n"

let bingoCalls =
    inputData.[0].Split ","
    |> Array.map int
    |> Array.toList

let bingoCards =
    inputData.[1..]
    |> Array.filter (fun row -> row <> "")
    |> Array.chunkBySize 5
    |> Array.map
        (fun card ->
            card
            |> Array.map
                (fun row ->
                    row.Split " "
                    |> Array.filter (fun word -> word <> "")
                    |> Array.map int))
    |> Array.map array2D

let rec callBingo
    (targettingLastWinner: bool)
    (calls: int list)
    lastNumber
    (lastWinners: int [,] [])
    (cards: int [,] [])
    =
    let winner =
        cards
        |> Array.filter
            (fun card ->
                [| 0 .. 4 |]
                |> Array.map
                    (fun index ->
                        ((card.[index, *]
                          |> Array.filter (fun item -> item = -1)
                          |> Array.length = 5)
                         || (card.[*, index]
                             |> Array.filter (fun item -> item = -1)
                             |> Array.length = 5)))
                |> Array.contains true)
        |> Array.sortBy
            (fun card ->
                card
                |> Seq.cast<int>
                |> Seq.filter (fun num -> num = -1)
                |> Seq.length)

    let newWinner =
        winner
        |> Array.filter (fun card -> not (Array.contains card lastWinners))

    if newWinner.Length > 0
       && ((targettingLastWinner
            && winner.Length = cards.Length)
           || not targettingLastWinner) then
        (winner.[0], lastNumber)
    else
        match calls with
        | head :: tail ->
            callBingo
                targettingLastWinner
                tail
                head
                winner
                (cards
                 |> Array.filter (fun card -> not (Array.contains card winner))
                 |> Array.map
                     (fun card ->
                         card
                         |> Array2D.map (fun num -> if num = head then -1 else num)))
        | [] -> failwith "No winner found"

let winningCard, lastNumber =
    callBingo false bingoCalls -1 [||] bingoCards

let lastWinningCard, lastWinningNumber =
    callBingo true bingoCalls -1 [||] bingoCards

let unmarkedSum =
    winningCard
    |> Seq.cast<int>
    |> Seq.filter (fun num -> num <> -1)
    |> Seq.sum

let lastUnmarkedSum =
    lastWinningCard
    |> Seq.cast<int>
    |> Seq.filter (fun num -> num <> -1)
    |> Seq.sum

printfn $"Part A: %d{unmarkedSum * lastNumber}"
printfn $"Part B: %d{lastUnmarkedSum * lastWinningNumber}"
