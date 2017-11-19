#r "../packages/FSharp.Data/lib/net45/FSharp.Data.dll"

open FSharp.Data

type GitHub = JsonProvider<"https://api.github.com/repos/dotnet/coreclr/issues">

let topRecentlyUpdatedIssues = 
    GitHub.GetSamples()
    |> Seq.sortBy (fun x -> x.UpdatedAt)
    |> Seq.truncate 5

for issue in topRecentlyUpdatedIssues do
    printfn "#%d %s" issue.Id issue.Title

