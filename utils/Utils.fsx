[<RequireQualifiedAccess>]
module String =
    /// Pipelinable version of the .Split method on strings.
    /// Similar to FSharpPlus's version, but returns an array instead of a generic sequence for ease of performing subsequent operations in solutions without requiring a call to Seq.toArray.
    let split (separator: string) (str: string) : string[] = str.Split separator

[<RequireQualifiedAccess>]
module Array =
    /// Converts an array containing two elements to a tuple.
    let toTuple2 (arr: 'T[]) : 'T * 'T =
        if arr.Length <> 2 then
            failwith
                "Array must have exactly two elements to convert to a tuple"

        (arr[0], arr[1])

[<RequireQualifiedAccess>]
module Array2D =
    /// Extracts a range of values from a 2D array.
    let range
        ((startX, startY): int * int)
        ((endX, endY): int * int)
        (arr: 'T[,])
        : 'T[] =
        let xValues =
            if startX = endX then
                Array.create (abs (endY - startY) + 1) startX
            else
                [| startX .. (sign (endX - startX)) .. endX |]

        let yValues =
            if startY = endY then
                Array.create (abs (endX - startX) + 1) startY
            else
                [| startY .. (sign (endY - startY)) .. endY |]

        Array.zip xValues yValues |> Array.map (fun (x, y) -> arr[y, x])
