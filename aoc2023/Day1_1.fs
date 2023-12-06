module aoc.Day1_1

open Support

let stringToLines (s: string) = s.Split("\n") |> Array.toList

let digits = List.map (fun i -> i.ToString()) [ 0..9 ]

type StringWithIndex = string * int

type FindAnyMode =
    | First
    | Last

let findAnySubstring (mode: FindAnyMode) (substrings: List<string>) (s: string) =
    let indexOf (ss: string) (s: string) = s.IndexOf(ss)
    let lastIndexOf (ss: string) (s: string) = s.LastIndexOf(ss)

    let indexForMode =
        match mode with
        | First -> indexOf
        | Last -> lastIndexOf

    let stringWithIndex (ss: string) = (ss, indexForMode ss s)

    let findCorrect: StringWithIndex list -> StringWithIndex =
        match mode with
        | First -> List.minBy snd
        | Last -> List.maxBy snd

    List.map stringWithIndex substrings
    |> List.filter (fun (_, index) -> index >= 0)
    |> findCorrect

let concat ((s1, i1): StringWithIndex) ((s2, i2): StringWithIndex) = $"%s{s1}{s2}"

let calibrationValue (s: string) =
    let find mode = findAnySubstring mode digits s
    concat (find First) (find Last) |> int

let test = readInput 1 |> List.map calibrationValue |> List.sum

//[<EntryPoint>]
let main args =
    printfn $"%s{test.ToString()}"
    0
