let inputData =
    (System.IO.File.ReadAllText "Day9/input.txt")
        .Split "\n"
    |> Array.map
        (fun line ->
            line
            |> Seq.toArray
            |> Array.map string
            |> Array.map int)
    |> array2D

let width = (inputData.GetLength 1) - 1
let height = (inputData.GetLength 0) - 1

let calculateLowPointsSum (grid: int [,]) : int =
    let mutable lowPointsSum = 0

    for x = 0 to width do
        for y = 0 to height do
            let pointsToCheck =
                Array.concat [| if x <> 0 then
                                    [| (y, x - 1) |]
                                else
                                    [||]
                                if x < width then
                                    [| (y, x + 1) |]
                                else
                                    [||]
                                if y <> 0 then
                                    [| (y - 1, x) |]
                                else
                                    [||]
                                if y < height then
                                    [| (y + 1, x) |]
                                else
                                    [||] |]

            let lowerPoints =
                pointsToCheck
                |> Array.map (fun (checkY, checkX) -> grid.[checkY, checkX] <= grid.[y, x])
                |> Array.filter id

            if lowerPoints.Length = 0 then
                printfn $"%d{x} %d{y}"
                lowPointsSum <- lowPointsSum + grid.[y, x] + 1

    lowPointsSum

//let findBasinSize (grid: int [,]) (start: int * int) (visited: Set<int * int>) : int =
//    let mutable openList = [ start ]
//    let mutable totalSize = 0
//
//    while openList.Length <> 0 do
//        let  nextPos, remainder = openList
//        openList <- remainder
//
//        let y, x = nextPos
//
//        let newOpenItems =
//            if grid.[y, x] = 9 || visited.Contains(y, x) then
//                []
//            else
//                List.concat [ if x <> 0 then [ (y, x - 1) ] else []
//                              if x < width then [ (y, x + 1) ] else []
//                              if y <> 0 then [ (y - 1, x) ] else []
//                              if y < height then
//                                  [ (y + 1, x) ]
//                              else
//                                  [] ]
//
//        0
//
//
//    0


//let findBasins (grid: int [,]) : int =
//    let visitedNodes = Set<int * int> []
//
//    let basins = [||]
//
//
//    for x = 0 to width do
//        for y = 0 to height do
//            if grid.[y, x] = 9 || visitedNodes.Contains(x, y) then
//                //                pas
//                0


let partA = inputData |> calculateLowPointsSum

printfn $"%d{partA}"
