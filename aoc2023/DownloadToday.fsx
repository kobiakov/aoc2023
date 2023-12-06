#!/usr/bin/env -S dotnet fsi
#r "nuget: FsHttp"
#load "AOC.fs"

open System
open FsHttp
open aoc.Support

type Token = string

let args = fsi.CommandLineArgs

let dayToFetch =
    if args.Length = 1 then 
        DateTime.Now.Day
    else 
        int args[1]
        
let readAoCToken =
    let trim (s: string) = s.Trim() 
    System.IO.File.ReadAllText ".token" |> trim

let writeToFile (fileName: string) (content: string) =
    System.IO.File.WriteAllText(fileName, content.Trim())

let fetchInputForGivenDay (token: string) (day: int) = 
    http {
        GET $"https://adventofcode.com/2023/day/%d{day}/input"
        CacheControl "no-cache"
        header "cookie" $"session=%s{token}" 
    }
    |> Request.send
    |> Response.toText

let fetchInputAndWriteToFile (day: int) =
    fetchInputForGivenDay readAoCToken day
    |> writeToFile (dayInputFileName day)

fetchInputAndWriteToFile dayToFetch
