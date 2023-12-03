#!/usr/bin/env -S dotnet fsi
#r "nuget: FsHttp"

open System
open FsHttp
open FsHttp.Domain

type Token = string

let readAoCToken = System.IO.File.ReadAllText ".token" |> String.s
    
let fetchInputForGivenDay (token: string) (day: int) = 
    http {
        GET $"https://adventofcode.com/2023/day/%d{day}/input"
        CacheControl "no-cache"
        header "cookie" $"session=%s{token}" 
    }
    |> Request.send
    |> Response.toText

printf $"$s{fetchInputForGivenDay readAoCToken 1}"