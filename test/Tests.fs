module Tests

open Xunit
open Domain
open ResultType

[<Fact>]
let ``Value of progress must not be less than 0`` () =
    let progress = Progress.create -1
    Assert.True(isFailure progress);

[<Fact>]
let ``Value of progress must not be higher than 100`` () =
    let progress = Progress.create 101
    Assert.True(isFailure progress);

[<Fact>]
let ``Value of progress must be between 0 and 100`` () =
    let progress = Progress.create 23
    Assert.True(isSuccess progress);

[<Fact>]
let ``Default value of progress for Goal is 0`` () =
    let goal = Goal.create List.empty "desc" |> payload
    Assert.True(goal.Progress = (Progress.create 0 |> payload));

[<Fact>]
let ``Description is being set for Goal`` () =
    let goal = Goal.create List.empty "desc" |> payload
    Assert.True(goal.Description = "desc");
    

 