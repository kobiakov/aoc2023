module aoc.Support

let dayInputFileName (day: int) = $"%s{__SOURCE_DIRECTORY__}/Day%d{day}.input"

let readInput (day: int) =
    let trim (s: string) = s.Trim()
    let splitToLines (s: string) = s.Split("\n")
    dayInputFileName day
    |> System.IO.File.ReadAllText
    |> trim
    |> splitToLines
    |> Array.toList