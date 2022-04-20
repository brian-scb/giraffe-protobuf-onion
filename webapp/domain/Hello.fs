namespace Domain

type Greeting = string

module Hello =
  let sayHello (nickname: string): Greeting =
    sprintf "Hello, %s" nickname
