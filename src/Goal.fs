namespace Domain
open ResultType

type Goal = private {
        SubGoals: Goal list;
        Progress: Progress;
        Description: string;
    }
module Goal =
    let create subGoals description =
        //TODO: validation
        Success { SubGoals = subGoals; Progress = Progress.create 0 |> payload; Description = description }

    let addSubgoal goal subGoal =
        match List.tryFind (fun a -> a = subGoal) goal.SubGoals with
        | Some _ -> Failure <| Error "this goal is already on subgoal list"
        | None -> Success { goal with SubGoals = subGoal :: goal.SubGoals }

    let SubGoals g = g.SubGoals
    let Description g = g.Description
    let Progress g = g.Progress