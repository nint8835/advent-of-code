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
