module aoc2023.TestInputFetcher

open System
open System.Net
open System.Net.Http
open System.Net.Http.Headers
open System.Threading.Tasks
open FsHttp

let fetchInput (day: int): string list = failwith "bla"

let private httpClient = new HttpClient()

let private getResponseAsString (url: string) =
    async {
        try
            let token = Environment.GetEnvironmentVariable("YOUR_TOKEN_ENV_VARIABLE_NAME")
            let request = new HttpRequestMessage(HttpMethod.Get, url)
            request.Headers.Add("Cookie", $"session=%s{token}")
            let! response = httpClient.SendAsync(request) |> Async.AwaitTask
            let statusCode = response.StatusCode
            if statusCode = HttpStatusCode.OK then
                let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
                return content
            else
                failwithf $"Failed to get a successful response. Status code: %A{statusCode}"
        with
        | ex -> failwithf $"Exception occurred: %s{ex.Message}"
    }

let url = "https://api.example.com/data"
async {
    try
        let! result = getResponseAsString url
        printfn $"Response: %s{result}"
    with
    | ex -> printfn $"Error: %s{ex.Message}"
}
|> Async.RunSynchronously