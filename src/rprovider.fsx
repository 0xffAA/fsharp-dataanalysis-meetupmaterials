#I "../packages/RProvider"
#I "../packages/R.NET.Community/"
#I "../packages/R.NET.Community.FSharp"
#load "RProvider.fsx"

open System
open RDotNet
open RProvider
open RProvider.graphics
open RProvider.stats

let rng = Random()
let rand () = rng.NextDouble()

let X1s = [ for i in 0 .. 9 -> 10. * rand () ]
let X2s = [ for i in 0 .. 9 -> 5. * rand () ]

let Ys = [ for i in 0 .. 9 -> 5. + 3. * X1s.[i] - 2. * X2s.[i] + rand () ]

R.plot(Ys)

let ds = namedParams ["Y", box Ys;"X1", box X1s;"X2", box X2s;] |> R.data_frame

let result = R.lm(formula = "Y~X1+X2", data = ds)