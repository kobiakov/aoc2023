module aoc2023.Day2_1

open aoc.Support

type GameSet =
    { red: int
      green: int
      blue: int }

    static member Empty = { red = 0; green = 0; blue = 0 }

type NCube = int * string

type Game = { id: int; sets: GameSet list }

let parseSet (s: string) =
    let partToNCubes (part: string) : NCube list =
        splitAndTrim "," s
        |> List.map (fun x ->
            match (splitAndTrim " " x) with
            | n :: [ colour ] -> (int n, colour)
            | _ -> failwith $"Not really a part: %s{part}")

    let applyNCubes (gs: GameSet) (nc: NCube) : GameSet =
        match nc with
        | (i, "red") -> { gs with red = i }
        | (i, "green") -> { gs with green = i }
        | (i, "blue") -> { gs with blue = i }
        | _ -> gs

    List.fold applyNCubes GameSet.Empty (partToNCubes s)

let parseGame (s: string) =
    let parts = s.Split(" ", 3) |> Array.toList |> List.map (fun x -> x.Trim())
    let parseId (rawId: string) = rawId.Trim([| ':' |]) |> int

    let parseSets (rawSets: string) =
        splitAndTrim ";" rawSets |> List.map parseSet

    match parts with
    | _ :: rawId :: [ rawSets ] ->
        { id = parseId rawId
          sets = parseSets rawSets }
    | _ -> failwith $"Some unexpected input %s{s}"

let isPlausibleGame (g: Game) =
    let isPlausibleSet (s: GameSet) =
        s.red <= 12 & s.green <= 13 & s.blue <= 14
    List.forall isPlausibleSet g.sets

let processInput (input: string list) =
    input
    |> List.map parseGame
    |> List.filter isPlausibleGame
    |> List.sumBy (fun g -> g.id)

//[<EntryPoint>]
let main args =
    printfn $"%d{readInput 2 |> processInput}"
    0
