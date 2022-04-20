open System
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Configuration
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe

open Settings

open API
open Dependencies

let notFoundHandler =
    text "Not Found" |> RequestErrors.notFound

let configureApp (root: Root.Root) (appBuilder: IApplicationBuilder) =
    appBuilder.UseGiraffe(HelloRouter.endpoints root)
    appBuilder.UseGiraffe notFoundHandler

let configureServices (services: IServiceCollection) = services.AddGiraffe() |> ignore

let configureSettings (configurationBuilder: IConfigurationBuilder) =
    configurationBuilder
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", false)

[<EntryPoint>]
let main args =
    let confBuilder =
        ConfigurationBuilder() |> configureSettings

    let root =
        confBuilder.Build().Get<Settings>()
        |> Trunk.compose
        |> Root.compose

    WebHost
        .CreateDefaultBuilder(args)
        .UseKestrel()
        .Configure(configureApp root)
        .ConfigureServices(configureServices)
        .Build()
        .Run()

    0
