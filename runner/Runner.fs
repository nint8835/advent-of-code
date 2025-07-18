open System.IO
open System.Reflection

let NullToOption (v: 'T | null) : Option<'T> =
    match v with
    | null -> None
    | nonNullVal -> Some nonNullVal

type SolutionDay =
    { Day: string
      Year: string
      Path: DirectoryInfo }

let GetDay (targetFile: string) : Result<SolutionDay, string> =
    targetFile
    |> Directory.GetParent
    |> NullToOption
    |> function
        | Some dayFolder -> Ok dayFolder
        | None -> Error $"file {targetFile} not found"
    |> Result.bind (fun dayFolder ->
        dayFolder.Parent
        |> NullToOption
        |> function
            | Some yearFolder ->
                { Day = dayFolder.Name
                  Year = yearFolder.Name
                  Path = dayFolder }
                |> Ok
            | None -> Error "year folder not found")

let GetSolutionMethod
    (solutionDay: SolutionDay)
    : Result<SolutionDay * MethodInfo, string> =
    let solutionAssembly = Assembly.Load "AdventOfCode.Solutions"

    let solutionNs =
        $"AdventOfCode.Solutions.Y%s{solutionDay.Year}.D%s{solutionDay.Day}"

    solutionNs
    |> solutionAssembly.GetType
    |> NullToOption
    |> function
        | Some solutionType -> Ok(solutionDay, solutionType)
        | None ->
            Error
                $"namespace {solutionNs} not found in assembly AdventOfCode.Solutions"
    |> Result.bind (fun (solutionDay, solutionType) ->
        solutionType.GetMethod "solve"
        |> NullToOption
        |> function
            | Some solveMethod -> Ok(solutionDay, solveMethod)
            | None ->
                Error
                    $"solve method not found in solution type {solutionType.FullName}")

let InvokeSolution
    ((solutionDay, solveMethod): SolutionDay * MethodInfo)
    : Result<unit, string> =
    solutionDay.Path.FullName |> Directory.SetCurrentDirectory

    solveMethod.Invoke(null, [||]) |> ignore

    Ok()

let TryInvokeSolution (targetFile: string) : Result<unit, string> =
    targetFile
    |> GetDay
    |> Result.bind GetSolutionMethod
    |> Result.bind InvokeSolution

[<EntryPoint>]
let main args =
    args
    |> Array.head
    |> TryInvokeSolution
    |> function
        | Ok _ -> 0
        | Error err ->
            printfn $"Error: %s{err}"
            1
