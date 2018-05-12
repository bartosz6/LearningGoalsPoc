namespace CQRS

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
    open System
    open Domain

    type Query = { Id : Guid }
    let handle query =
        Goal.create query.Id List.Empty "placeholder"

module GetGoalProgressByGoalId =
    open System
    open Domain

    type Query = { Id : Guid }
    let handle query =
        Progress.create 13

module QueryHandler = 
    type Query =
    | GetGoalById of GetGoalById.Query
    | GetGoalProgressByGoalId of GetGoalProgressByGoalId.Query
    let handle<'T> query =
        let result = 
            match query with
            | GetGoalById q -> GetGoalById.handle q :> obj
            | GetGoalProgressByGoalId q -> GetGoalProgressByGoalId.handle q :> obj
        result :?> ResultType.Result<'T>
