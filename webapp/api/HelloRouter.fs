module API.HelloRouter

open Dependencies
open API.HelloService
open Brian.V1
open FSharp.Control.Tasks
open Giraffe
open Microsoft.AspNetCore.Http
open ProtoHandler
open ParseProto

let helloHandler (io: IO) (next: HttpFunc) (ctx: HttpContext) =
    task {
        let! req = parseProto<BrianRequest> ctx
        let! res = sayHello io req
        return! proto res next ctx
    }

let endpoints (root: Root.Root) =
    choose [
        route "/" >=> helloHandler { GetNickname = root.GetNickname } ]
