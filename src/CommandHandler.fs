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
    let handle<'TQuery, 'TResult> (query:'TQuery)  : ResultType.Result<'TResult> =
        let result = 
            match box query with
            | :? GetGoalById.Query as q -> GetGoalById.handle q |> box
            | :? GetGoalProgressByGoalId.Query as q -> GetGoalProgressByGoalId.handle q |> box
            
            | _ -> raise (new NotImplementedException("handler for that query is not implemented"))
        unbox result
