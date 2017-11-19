#I "../packages/FSharp.Charting"
#load "FSharp.Charting.fsx"

open FSharp.Charting
open System

let d1 = [for x in 0 .. 100 -> (x, 1.0 / (float x + 1.) )]
let d2 = [for x in 0 .. 100 -> (x, Math.Sin(float(x)))]

Chart.Rows [
    Chart.Line(d1,Name="d1",Title="d1")
    Chart.Column(d2,Name="d2",Title="d2")
]

Chart.Combine [
    Chart.Line(d1,Name="d1",Title="d1")
    Chart.Column(d2,Name="d2",Title="d2")
]