namespace API.HelloDependencies

open System.Data
open API
open Repository

module HelloDependencies =
  let compose (createDbConnection: unit -> Async<IDbConnection>): HelloService.IO =
    { GetNickname = HelloRepository.getNickname createDbConnection }
