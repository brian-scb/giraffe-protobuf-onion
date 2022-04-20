namespace Dependencies

open Settings
open API
open API.HelloDependencies
open Repository

module Trunk =
    type Trunk = { HelloDependencies: HelloService.IO }

    let compose (settings: Settings) =
        let createDBConnection =
            DapperFSharp.createSqlConnection settings.SqlConnectionString

        { HelloDependencies = HelloDependencies.compose createDBConnection }

module Root =
    type Root =
        { GetNickname: string -> Async<string> }

    let compose (trunk: Trunk.Trunk) =
        { GetNickname = trunk.HelloDependencies.GetNickname }
