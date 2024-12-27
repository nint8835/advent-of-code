open System.IO
open System.Reflection

let NullToOption (v: 'T | null): Option<'T> =
    match v with
    | null -> None
    | nonNullVal -> Some nonNullVal

[<EntryPoint>]
let main args =
    // TODO: Better error handling
    let targetFile = args |> Array.head
    let dayFolder = targetFile |> Directory.GetParent |> NullToOption |> function
        | Some dayFolder -> dayFolder
        | None -> failwith "Day folder not found"
    let yearFolder = dayFolder.Parent |> NullToOption |> function
        | Some yearFolder -> yearFolder
        | None -> failwith "Year folder not found"
    
    let year = yearFolder.Name
    let day = dayFolder.Name
    
    let solutionAssembly = Assembly.Load "AdventOfCode.Solutions"
    let solutionType = $"AdventOfCode.Solutions.Y%s{year}.D%s{day}" |> solutionAssembly.GetType
    let solveMethod = solutionType.GetMethod "solve" |> NullToOption |> function
        | Some solveMethod -> solveMethod
        | None -> failwith "solve method not found"
        
    dayFolder.FullName |> Directory.SetCurrentDirectory
    
    solveMethod.Invoke(null, [||]) |> ignore

    0
