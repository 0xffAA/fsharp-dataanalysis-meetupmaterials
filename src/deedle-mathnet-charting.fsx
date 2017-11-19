open System.Collections.Generic

#r "../packages/FSharp.Data/lib/net45/FSharp.Data.dll"
#I "../packages/Deedle"
#I "../packages/FSharp.Charting"
#load "Deedle.fsx"
#load "FSharp.Charting.fsx"
#load "../packages/MathNet.Numerics.FSharp/MathNet.Numerics.fsx"

open MathNet.Numerics.Statistics
open Deedle
open FSharp.Data
open FSharp.Charting

let WorldBank = WorldBankData.GetDataContext()

let co2Indicator = 
    WorldBank
        .Countries.``Russian Federation``
        .Indicators.``CO2 emissions (metric tons per capita)``

let populationIndicator =
    WorldBank
        .Countries.``Russian Federation``
        .Indicators.``Population, total``

Chart.Rows(
    [ Chart.Column(co2Indicator,Name="co2",Title="co2")
      Chart.Column(populationIndicator,Name="population",Title="population")]
)

let co2Series = co2Indicator |>  Series.ofObservations

let populationSeries = populationIndicator |>  Series.ofObservations

let dataFrame = Frame(["co2"; "population"], [co2Series; populationSeries]) //|> Frame.dropSparseRows

Chart.Rows(
    [ Chart.Column(dataFrame?co2 |> Series.observations,Name="co2",Title="co2")
      Chart.Column(dataFrame?population |> Series.observations,Name="population",Title="population")]
)

dataFrame?totalCo2 <- dataFrame?co2 * dataFrame?population

Chart.Rows(
    [ Chart.Column(dataFrame?co2 |> Series.observations,Name="co2",Title="co2")
      Chart.Column(dataFrame?population |> Series.observations,Name="population",Title="population")
      Chart.Line(dataFrame?totalCo2 |> Series.observations,Name="totalco2",Title="totalco2")]
)

let avarageCo2 = dataFrame?totalCo2 |> Stats.median

let co2Values = dataFrame?co2 |> Series.values
let populationValues = dataFrame?population |> Series.values

let coef = Correlation.Spearman(populationValues, co2Values)
