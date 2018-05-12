module ResultType
    type Error =
        | ErrorList of string list
        | Error of string

    type Result<'T> =
        | Success of 'T
        | Failure of Error

    let success payload =
        Success payload

    let failure errors =
        Failure errors

    let isSuccess result =
        match result with
        | Success _ -> true
        | _ -> false
    
    let isFailure result = isSuccess result |> not

    let cast<'T> result = 
        let cast (x:obj) = 
            match x with
            | :? 'T as t -> t
            | _ -> raise (System.InvalidOperationException("invalid type cast"))

        match result with 
        | Success x -> Success <| cast x
        | Failure x -> Failure x