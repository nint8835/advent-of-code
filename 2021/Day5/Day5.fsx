open System

let inputData =
    (System.IO.File.ReadAllText "Day5/input.txt")
        .Split "\n"

let getLineEnds (input: string []) =
    input
    |> Array.map
        (fun line ->
            line.Split " -> "
            |> Array.map (fun coordPair -> coordPair.Split "," |> Array.map int))

type Axis =
    | X = 0
    | Y = 1

let getMaxCoord (axis: Axis) (lineEnds: int [] [] []) =
    lineEnds
    |> Array.map (fun line -> line |> Array.map (fun pair -> pair.[int axis]))
    |> Array.concat
    |> Array.max

let lineEnds = inputData |> getLineEnds
let maxX = lineEnds |> getMaxCoord Axis.X
let maxY = lineEnds |> getMaxCoord Axis.Y

let rec calculateHits (allowDiags: bool) (hits: int [,]) (remainingLineEnds: int [] [] list) =
    match remainingLineEnds with
    | [] -> hits
    | hit :: tail ->
        let startX = hit.[0].[0]
        let startY = hit.[0].[1]
        let endX = hit.[1].[0]
        let endY = hit.[1].[1]

        if startY = endY then
            [| Math.Min(startX, endX) .. Math.Max(startX, endX) |]
            |> Array.iter (fun (x: int) -> hits.[x, startY] <- hits.[x, startY] + 1)
        else if startX = endX then
            [| Math.Min(startY, endY) .. Math.Max(startY, endY) |]
            |> Array.iter (fun (y: int) -> hits.[startX, y] <- hits.[startX, y] + 1)
        else if allowDiags then
            let maxOffset = Math.Abs(startX - endX)
            let xDirection = Math.Sign(endX - startX)
            let yDirection = Math.Sign(endY - startY)

            [| 0 .. maxOffset |]
            |> Array.iter
                (fun (offset: int) ->
                    let newX = startX + (xDirection * offset)
                    let newY = startY + (yDirection * offset)
                    hits.[newX, newY] <- hits.[newX, newY] + 1)
        else
            printfn "Ignore"

        calculateHits allowDiags hits tail

let hitsA =
    Array2D.init (maxX + 1) (maxY + 1) (fun _ _ -> 0)

let calculatedHitsA =
    calculateHits false hitsA (Array.toList lineEnds)

let aOverlapping =
    calculatedHitsA
    |> Seq.cast<int>
    |> Seq.filter (fun i -> i > 1)
    |> Seq.length

let hitsB =
    Array2D.init (maxX + 1) (maxY + 1) (fun _ _ -> 0)

let calculatedHitsB =
    calculateHits true hitsB (Array.toList lineEnds)

let bOverlapping =
    calculatedHitsB
    |> Seq.cast<int>
    |> Seq.filter (fun i -> i > 1)
    |> Seq.length

printfn $"%A{aOverlapping} %A{bOverlapping}"
