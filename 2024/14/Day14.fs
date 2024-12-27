module AdventOfCode.Solutions.Y2024.D14

open System.IO
open AdventOfCode.Solutions.Utils
open SkiaSharp

/// Euclidean remainder, the proper modulo operation
/// Taken from https://stackoverflow.com/a/35848799
let inline (%!) a b = (a % b + b) % b

type Robot =
    { Position: int * int
      Velocity: int * int }

[<Literal>]
// let areaWidth = 11
let areaWidth = 101

[<Literal>]
// let areaHeight = 7
let areaHeight = 103

let inputData =
    File.ReadAllLines "input.txt"
    |> Array.map (fun line ->
        let position, velocity =
            line
            |> String.split " "
            |> Array.map (fun coords ->
                coords
                |> String.split "="
                |> Array.skip 1
                |> Array.head
                |> String.split ","
                |> Array.map int
                |> Array.toTuple2)
            |> Array.toTuple2

        { Position = position
          Velocity = velocity })

let tick (robot: Robot) : Robot =
    { robot with
        Position =
            ((fst robot.Position + fst robot.Velocity) %! areaWidth,
             (snd robot.Position + snd robot.Velocity) %! areaHeight) }

let quadrant (robot: Robot) : Option<int> =
    match robot.Position with
    | x, y when x < areaWidth / 2 && y < areaHeight / 2 -> Some(1)
    | x, y when x > areaWidth / 2 && y < areaHeight / 2 -> Some(2)
    | x, y when x < areaWidth / 2 && y > areaHeight / 2 -> Some(3)
    | x, y when x > areaWidth / 2 && y > areaHeight / 2 -> Some(4)
    | _ -> None

let tickTimes (times: int) (robot: Robot) : Robot =
    [| 1..times |] |> Array.fold (fun robot _ -> tick robot) robot

let solve () : unit =
    inputData
    |> Array.map (tickTimes 100)
    |> Array.groupBy quadrant
    |> Array.filter (fst >> Option.isSome)
    |> Array.map (snd >> Array.length)
    |> Array.fold (*) 1
    |> printfn "%A"

    // Images cannot be unit tested, and SkiaSharp does not work automatically in GitHub Actions
    CI.exitIfCI ()

    Directory.CreateDirectory "images"

    do
        [| 0..10000 |]
        |> Array.fold
            (fun robots i ->
                let surface =
                    SKSurface.Create(
                        new SKImageInfo(
                            areaWidth,
                            areaHeight,
                            SKColorType.Rgba8888,
                            SKAlphaType.Premul
                        )
                    )

                let canvas = surface.Canvas
                canvas.DrawColor(SKColors.White)

                robots
                |> Array.map _.Position
                |> Set.ofArray
                |> Set.iter (fun (x, y) ->
                    canvas.DrawPoint(
                        new SKPoint(float32 x, float32 y),
                        new SKPaint(
                            Color = SKColors.Black,
                            StrokeWidth = 0.0f
                        )
                    ))

                let image = surface.Snapshot()
                let data = image.Encode(SKEncodedImageFormat.Png, 100)
                let path = Path.Combine("images", $"{i}.png")
                File.WriteAllBytes(path, data.ToArray())

                Array.map tick robots)
            inputData
