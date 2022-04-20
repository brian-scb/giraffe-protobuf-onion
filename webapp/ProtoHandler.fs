module ProtoHandler

open Microsoft.AspNetCore.Http
open Giraffe
open Google.Protobuf

let proto (message: 'a) : HttpHandler =
    let bytes = MessageExtensions.ToByteArray(message)

    fun (_: HttpFunc) (ctx: HttpContext) ->
        ctx.SetContentType("application/x-protobuf")
        ctx.WriteBytesAsync bytes
