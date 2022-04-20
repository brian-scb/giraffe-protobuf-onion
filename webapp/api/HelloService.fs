namespace API
open Domain
open Brian.V1

module HelloService =
  type IO = {
    GetNickname: string -> Async<string>
  }

  let sayHello (io: IO) (request: BrianRequest) =
    let name = request.Name

    async {
      let! nickname = io.GetNickname name
      let message = Hello.sayHello nickname
      return BrianResponse(Name=name, Message=message)
    }
