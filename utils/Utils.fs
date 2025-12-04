namespace AdventOfCode.Solutions.Utils

open System

[<AutoOpen>]
module Operators =
    /// Euclidean remainder, the proper modulo operation
    /// Taken from https://stackoverflow.com/a/35848799
    let inline (%!) a b = (a % b + b) % b

[<RequireQualifiedAccess>]
module String =
    /// Pipelinable version of the .Split method on strings.
    /// Similar to FSharpPlus's version, but returns an array instead of a generic sequence for ease of performing subsequent operations in solutions without requiring a call to Seq.toArray.
    let split (separator: string) (str: string) : string[] = str.Split separator

    /// Reverse a given string.
    let reverse (str: string) : string =
        str |> Seq.toArray |> Array.rev |> Array.map string |> String.concat ""

[<RequireQualifiedAccess>]
module Array =
    /// Converts an array containing two elements to a tuple.
    let toTuple2 (arr: 'T[]) : 'T * 'T =
        if arr.Length <> 2 then
            failwith
                "Array must have exactly two elements to convert to a tuple"

        (arr[0], arr[1])

    /// Converts an array containing three elements to a tuple.
    let toTuple3 (arr: 'T[]) : 'T * 'T * 'T =
        if arr.Length <> 3 then
            failwith
                "Array must have exactly three elements to convert to a tuple"

        (arr[0], arr[1], arr[2])

    /// Generate a list of all permutations of a given size for the provided array, with repetition not allowed.
    let permutations (size: int) (items: 'T[]) : 'T[][] =
        let rec permute (size: int) (items: 'T[]) (acc: 'T[]) : 'T[][] =
            if size = 0 then
                [| acc |]
            else
                items
                |> Array.collect (fun item ->
                    permute
                        (size - 1)
                        (items |> Array.filter ((<>) item))
                        (Array.append acc [| item |]))

        permute size items [||]

    /// Generate a list of all permutations of a given size for the provided array, with repetition allowed.
    let permutationsWithRepetition (size: int) (items: 'T[]) : 'T[][] =
        let rec permute (size: int) (items: 'T[]) (acc: 'T[]) : 'T[][] =
            if size = 0 then
                [| acc |]
            else
                items
                |> Array.collect (fun item ->
                    permute (size - 1) (items) (Array.append acc [| item |]))

        permute size items [||]

    /// Add an element to the end of an array.
    let add (element: 'T) (arr: 'T[]) : 'T[] = Array.append arr [| element |]

    /// Turn an array into an array of index-value pairs.
    let indexed (arr: 'T[]) : (int * 'T)[] =
        arr |> Array.mapi (fun i v -> (i, v))

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

    /// Flattens a 2D array into a 1D array.
    let flatten (arr: 'T[,]) : 'T[] =
        let y = Array2D.length1 arr
        let x = Array2D.length2 arr

        Array.init (x * y) (fun i -> arr.[i / y, i % y])

    /// Turn a 2D array into an array of (x, y, value) tuples.
    let indexed (arr: 'T[,]) : (int * int * 'T)[] =
        arr |> Array2D.mapi (fun y x value -> (x, y, value)) |> flatten

    /// Find indices in an array where a given predicate is true.
    let findIndices (predicate: 'T -> bool) (arr: 'T[,]) : (int * int)[] =
        arr
        |> indexed
        |> Array.filter (fun (_, _, value) -> predicate value)
        |> Array.map (fun (x, y, _) -> (x, y))

    /// Returns the indices of surrounding elements of a given position in a 2D array.
    let neighbourIndicesWithDiagonals
        (x: int)
        (y: int)
        (arr: 'T[,])
        : (int * int)[] =
        [| for dy in -1 .. 1 do
               for dx in -1 .. 1 do
                   if dx <> 0 || dy <> 0 then
                       let newX = x + dx
                       let newY = y + dy

                       if
                           newX >= 0
                           && newX < Array2D.length2 arr
                           && newY >= 0
                           && newY < Array2D.length1 arr
                       then
                           yield (newX, newY) |]

    /// Returns the values of surrounding elements of a given position in a 2D array.
    let neighbourValuesWithDiagonals (x: int) (y: int) (arr: 'T[,]) : 'T[] =
        neighbourIndicesWithDiagonals x y arr
        |> Array.map (fun (nx, ny) -> arr[ny, nx])

    /// Returns the indices of direct neighbours of a given position in a 2D array.
    let neighbourIndices (x: int) (y: int) (arr: 'T[,]) : (int * int)[] =
        [| for dy in -1 .. 1 do
               for dx in -1 .. 1 do
                   if (dx = 0 || dy = 0) && (dx <> 0 || dy <> 0) then
                       let newX = x + dx
                       let newY = y + dy

                       if
                           newX >= 0
                           && newX < Array2D.length2 arr
                           && newY >= 0
                           && newY < Array2D.length1 arr
                       then
                           yield (newX, newY) |]

    /// Returns the values of direct neighbours of a given position in a 2D array.
    let neighbourValues (x: int) (y: int) (arr: 'T[,]) : 'T[] =
        neighbourIndices x y arr |> Array.map (fun (nx, ny) -> arr[ny, nx])

    /// Update a value in a 2D array at the specified coordinates, returning a new array.
    let updateAt (x: int) (y: int) (value: 'T) (arr: 'T[,]) : 'T[,] =
        let newArr = Array2D.copy arr
        newArr[y, x] <- value
        newArr


[<RequireQualifiedAccess>]
module Tuple =
    /// Add two tuples together.
    let inline add2 (a: 'T * 'T) (b: 'T * 'T) : 'T * 'T =
        (fst a + fst b, snd a + snd b)

    /// Subtract one tuple from another.
    let inline subtract2 (a: 'T * 'T) (b: 'T * 'T) : 'T * 'T =
        (fst a - fst b, snd a - snd b)

[<RequireQualifiedAccess>]
module CI =
    let exitIfCI () =
        if Environment.GetEnvironmentVariable("GITHUB_ACTIONS") = "true" then
            Environment.Exit 0
