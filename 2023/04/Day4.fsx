type Card =
    { CardNumber: int
      Count: int
      Groups: Set<int> array }

let inputData =
    System.IO.File.ReadAllText("input.txt").Split "\n"
    |> Array.map (fun line ->
        let cardParts = (line.Split ":")

        let cardNumber =
            (((cardParts[0]).Split(" ") |> Array.filter ((<>) ""))[1]).Trim()
            |> int

        let groups =
            (cardParts[1]).Split(" | ")
            |> Array.map (fun (group: string) ->
                group.Split " "
                |> Array.map _.Trim()
                |> Array.filter ((<>) "")
                |> Array.map int
                |> Set.ofArray)

        { CardNumber = cardNumber
          Count = 1
          Groups = groups })

let partA =
    inputData
    |> Array.map _.Groups
    |> Array.map (fun card ->
        let matchCount =
            card |> Array.reduce Set.intersect |> Set.toArray |> _.Length

        if matchCount > 0 then pown 2 (matchCount - 1) else 0)
    |> Array.sum

let rec processPartB (currentIndex: int) (cards: Card array) : Card array =
    if currentIndex >= cards.Length then
        cards
    else
        let currentCard = cards.[currentIndex]

        let winCount =
            currentCard.Groups
            |> Array.reduce Set.intersect
            |> Set.toArray
            |> _.Length

        let updatedCards =
            seq {
                currentCard.CardNumber .. currentCard.CardNumber + winCount - 1
            }
            |> Seq.toArray
            |> Array.fold
                (fun acc index ->
                    let existingCard = cards[index]

                    Array.updateAt
                        index
                        { existingCard with
                            Count = existingCard.Count + currentCard.Count }
                        acc)
                cards

        processPartB (currentIndex + 1) updatedCards

printfn "%A" partA
printfn "%A" (processPartB 0 inputData |> Array.map _.Count |> Array.sum)
