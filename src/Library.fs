namespace Domain
    module Goal =
        type T = {
            SubGoals: Goal list;
            Progress: Progress;
        }

        type Progress = private Progress of int
        module Progress =
            let create value = value
            let value progress = progress
