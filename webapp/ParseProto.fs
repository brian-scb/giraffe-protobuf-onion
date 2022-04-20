module ParseProto

open FSharp.Control.Tasks
open Google.Protobuf
open Microsoft.AspNetCore.Http
open System.IO
open System.Text

let readBody (ctx: HttpContext) =
    task {
        use reader =
            new StreamReader(ctx.Request.Body, Encoding.Default)

        let! body = reader.ReadToEndAsync()
        return Encoding.Default.GetBytes(body)
    }

let parseProto<'T when 'T :> IMessage<'T> and 'T: (new : unit -> 'T)> (ctx: HttpContext) =
    // 'T must be an IMessage and it must have a constructor
    task {
        let! body = readBody ctx

        let parser =
            new MessageParser<'T>(fun () -> new 'T())

        return parser.ParseFrom(body)
    }
