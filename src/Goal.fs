namespace Domain
open ResultType
open System

type Goal = private {
        Id: Guid;
        SubGoals: Goal list;
        Progress: Progress;
        Description: string;
    }
module Goal =
    let subGoals g = g.SubGoals
    let description g = g.Description
    let progress g = g.Progress
    let id g = g.Id

    let create id subGoals description =
        //TODO: validation
        match Progress.create 0 with 
        | Success progress -> Success { Id = id; SubGoals = subGoals; Progress = progress; Description = description }
        | Failure error -> Failure error

    let addSubgoal goal subGoal =
        match List.tryFind (fun a -> a.Id = subGoal.Id) goal.SubGoals with
        | Some _ -> Failure <| Error "this goal is already on subgoal list"
        | None -> Success { goal with SubGoals = subGoal :: goal.SubGoals }
