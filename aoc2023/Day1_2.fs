module aoc.Day1_2

open Support

let stringToLines (s: string) = s.Split("\n") |> Array.toList

let digitsToWords (d: int) =
    match d with
    | 0 -> "zero"
    | 1 -> "one"
    | 2 -> "two"
    | 3 -> "three"
    | 4 -> "four"
    | 5 -> "five"
    | 6 -> "six"
    | 7 -> "seven"
    | 8 -> "eight"
    | 9 -> "nine"
    | _ -> failwith "Uhm..."

let wordsToDigits (word: string) =
    match word.ToLower() with
    | "zero" -> "0"
    | "one" -> "1"
    | "two" -> "2"
    | "three" -> "3"
    | "four" -> "4"
    | "five" -> "5"
    | "six" -> "6"
    | "seven" -> "7"
    | "eight" -> "8"
    | "nine" -> "9"
    | _ -> word    

let digits =
    List.map (fun i -> i.ToString()) [ 0..9 ] @ (List.map digitsToWords [ 0..9 ])


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

let concat ((s1, _): StringWithIndex) ((s2, _): StringWithIndex) = $"%s{wordsToDigits s1}{wordsToDigits s2}"

let calibrationValue (s: string) =
    let find mode = findAnySubstring mode digits s
    concat (find First) (find Last) |> int


let test = readInput 1 |> List.map calibrationValue |> List.sum

//[<EntryPoint>]
let main args =
    printfn $"%s{test.ToString()}"
    0
