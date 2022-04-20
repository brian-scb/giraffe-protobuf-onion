namespace Repository

open System.Data
open Dapper
open System.Data.SQLite

module DapperFSharp =
  let sqlSingleOrNone<'Result> (query: string) (param: obj) (connection: IDbConnection): Async<'Result option> =
    async {
      let! result = connection.QuerySingleOrDefaultAsync<'Result>(query, param)
                   |> Async.AwaitTask
      return match box result with
              | null -> None
              | _ -> Some result
  }

  let sqlSingle<'Result> (query: string) (param: obj) (connection: IDbConnection): Async<'Result> =
    async {
      let! result = connection.QuerySingleAsync<'Result>(query, param)
                   |> Async.AwaitTask
      return result
    }

  let sqlExecute (sql: string) (param: obj) (connection: IDbConnection) =
    connection.ExecuteAsync(sql, param)
    |> Async.AwaitTask
    |> Async.Ignore

  let createSqlConnection (connectionString: string): unit -> Async<IDbConnection> =
    fun () -> async {
      let connection = new SQLiteConnection(connectionString)
      if connection.State <> ConnectionState.Open
      then do! connection.OpenAsync() |> Async.AwaitTask
      return connection :> IDbConnection
    }
