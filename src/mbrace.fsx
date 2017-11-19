#I __SOURCE_DIRECTORY__
#I "../packages/MBrace.Thespian/tools" 
#I "../packages/Streams/lib/net45" 
#r "../packages/Streams/lib/net45/Streams.dll"
#I "../packages/MBrace.Flow/lib/net45" 
#r "../packages/MBrace.Flow/lib/net45/MBrace.Flow.dll"
#load "../packages/MBrace.Thespian/MBrace.Thespian.fsx"

open MBrace.Core
open MBrace.Runtime
open MBrace.Thespian
open MBrace.Flow
open System

let rng = Random()
let rand () = rng.NextDouble()

let sourceValues = [|for x in 0 .. 16 -> [|for y in 0 .. 2000 -> rand()|]|]

let cluster = ThespianCluster.InitOnCurrentMachine(4, logger = new ConsoleLogger(), logLevel = LogLevel.Info)

let r1 = 
    [|for x in sourceValues -> cloud { return x |> Array.sum}|]
    |> Cloud.Parallel
    |> cluster.Run
    |> Array.sum

let r2 = 
    sourceValues
    |> CloudFlow.OfArray
    |> CloudFlow.map (fun x -> x |> Array.sum )
    |> CloudFlow.reduce (+)
    |> cluster.Run

cluster.KillAllWorkers()