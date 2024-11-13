[<RequireQualifiedAccess>]
module String =
    /// Pipelinable version of the .Split method on strings.
    /// Similar to FSharpPlus's version, but returns an array instead of a generic sequence for ease of performing subsequent operations in solutions without requiring a call to Seq.toArray.
    let split (separator: string) (str: string) : string[] = str.Split separator
