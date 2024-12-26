open System.IO
open System.Reflection

[<EntryPoint>]
let main args =
    let targetFile = args |> Array.head
    let dayFolder = targetFile |> Directory.GetParent
    let yearFolder = dayFolder.Parent
    
    let year = yearFolder.Name
    let day = dayFolder.Name
    
    let solutionAssembly = Assembly.Load "AdventOfCode.Solutions"
    let solutionType = $"AdventOfCode.Solutions.Y%s{year}.D%s{day}" |> solutionAssembly.GetType
    let solveMethod = solutionType.GetMethod "solve"
    
    solveMethod.Invoke(null, [||]) |> ignore

    0
