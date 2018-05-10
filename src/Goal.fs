namespace Domain
open ResultType

type Goal = {
        SubGoals: Goal list;
        Progress: Progress;
        Description: string;
    }
module Goal =
    let create subGoals description =
        Success { SubGoals = subGoals; Progress = Progress.create 0 |> payload; Description = description }
        
