module AdventOfCode.Solutions.Y2023.D04

type Card =
    { CardNumber: int
      Count: int
      MatchCount: int }

let inputData =
    System.IO.File.ReadAllText("input.txt").Split "\n"
    |> Array.map (fun line ->
        let cardParts = (line.Split ":")

        let cardNumber =
            (((cardParts[0]).Split(" ") |> Array.filter ((<>) ""))[1]).Trim()
            |> int

        let matchCount =
            (cardParts[1]).Split(" | ")
            |> Array.map (fun (group: string) ->
                group.Split " "
                |> Array.map _.Trim()
                |> Array.filter ((<>) "")
                |> Array.map int
                |> Set.ofArray)
            |> Array.reduce Set.intersect
            |> Set.toArray
            |> _.Length

        { CardNumber = cardNumber
          Count = 1
          MatchCount = matchCount })

let partA =
    inputData
    |> Array.map _.MatchCount
    |> Array.filter ((<>) 0)
    |> Array.map (fun count -> pown 2 (count - 1))
    |> Array.sum

let rec processPartB (currentIndex: int) (cards: Card array) : Card array =
    if currentIndex >= cards.Length then
        cards
    else
        let currentCard = cards.[currentIndex]

        let updatedCards =
            seq {
                currentCard.CardNumber .. currentCard.CardNumber
                                          + currentCard.MatchCount
                                          - 1
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

let solve () =
    printfn "%A" partA
    printfn "%A" (processPartB 0 inputData |> Array.map _.Count |> Array.sum)
