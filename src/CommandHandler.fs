namespace CQRS

open System
module CreateGoal =
    open ResultType

    type Command = { Description : string }
    let handle command =
        Success <| printfn "created goal with description %s" command.Description
    
module CommandHandler =

    type Command =
    | CreateGoal of CreateGoal.Command

    let handle command =
        match command with 
        | CreateGoal c -> CreateGoal.handle c

module GetGoalById =
    open Domain

    type Query = { Id : Guid }
    let handle query =
        Goal.create query.Id List.Empty "placeholder"

module GetGoalProgressByGoalId =
    open Domain

    type Query = { Id : Guid }
    let handle query =
        Progress.create 13

module QueryHandler = 
    let handle<'T> (query:obj) =
        let result = 
            match query with
            | :? GetGoalById.Query as q -> GetGoalById.handle q :> obj
            | :? GetGoalProgressByGoalId.Query as q -> GetGoalProgressByGoalId.handle q :> obj
            
            | _ -> raise (new NotImplementedException("handler for that query is not implemented"))
        result :?> ResultType.Result<'T>
