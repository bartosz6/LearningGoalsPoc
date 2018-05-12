namespace Domain
open ResultType

type Progress = private Progress of int
module Progress =
    let create value = 
        match value with
        | i when i >= 0 && i<= 100 -> Success <| Progress value    
        | _ -> Failure <| Error "progress value must be between 0 and 100"