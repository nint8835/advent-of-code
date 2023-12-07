let inputData =
    System.IO.File.ReadAllText("input.txt").Split "\n"
    |> Array.map (fun line ->
        line.Split " "
        |> Array.filter ((<>) "")
        |> fun pair -> pair[0], int pair[1])

let cardOrder = "AKQJT98765432"
let partBCardOrder = "AKQT98765432J"

type HandType =
    | FiveOfAKind
    | FourOfAKind
    | FullHouse
    | ThreeOfAKind
    | TwoPairs
    | OnePair
    | HighCard

let calculateCardSortKey
    (orderString: string)
    (handType: HandType, cards: string)
    =
    let cardRanks = cards |> Seq.map orderString.IndexOf |> Seq.toArray

    (handType,
     cardRanks[0],
     cardRanks[1],
     cardRanks[2],
     cardRanks[3],
     cardRanks[4])

let rec getHandType (useJokers: bool) (cards: string) =
    let cardCounts =
        cards
        |> Seq.countBy id
        |> Seq.sortBy (fun (_, count) -> -count)
        |> Seq.toArray

    let jokerCount =
        cardCounts
        |> Array.filter (fun (card, _) -> card = 'J')
        |> Array.sumBy snd

    if useJokers && jokerCount > 0 then
        partBCardOrder
        |> Seq.filter ((<>) 'J')
        |> Seq.map string
        |> Seq.map (fun char -> cards.Replace("J", char))
        |> Seq.map (fun cards -> getHandType false cards, cards)
        |> Seq.sortBy (calculateCardSortKey cardOrder)
        |> Seq.head
        |> fst
    else
        match snd cardCounts[0] with
        | 5 -> FiveOfAKind
        | 4 -> FourOfAKind
        | 3 ->
            match snd cardCounts[1] with
            | 2 -> FullHouse
            | _ -> ThreeOfAKind
        | 2 ->
            match snd cardCounts[1] with
            | 2 -> TwoPairs
            | _ -> OnePair
        | _ -> HighCard

let partA =
    inputData
    |> Array.map (fun (cards, bet) -> getHandType false cards, cards, bet)
    |> Array.sortByDescending (fun (handType, cards, _) ->
        calculateCardSortKey cardOrder (handType, cards))
    |> Array.mapi (fun index (_, _, bet) -> (index + 1) * bet)
    |> Array.reduce (+)

let partB =
    inputData
    |> Array.map (fun (cards, bet) -> getHandType true cards, cards, bet)
    |> Array.sortByDescending (fun (handType, cards, _) ->
        calculateCardSortKey partBCardOrder (handType, cards))
    |> Array.mapi (fun index (_, _, bet) -> (index + 1) * bet)
    |> Array.reduce (+)

printfn "%A" partA
printfn "%A" partB
