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