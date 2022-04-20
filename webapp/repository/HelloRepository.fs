namespace Repository

open DapperFSharp

module HelloRepository =
  let getNickname createConnection (_name: string) =
    async {
      use! connection = createConnection()
      return! sqlSingle<string> "select @nick" {| nick = "Bub" |} connection
    }
