module Tests

open Xunit
open Domain
open ResultType
open System

let private payload result =
    match result with
    | Success payload -> payload
    | _ -> raise (System.InvalidOperationException("cannot access payload of failure!"))

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
    let goal = Goal.create (Guid.NewGuid()) List.empty "desc" |> payload
    Assert.True(Goal.progress goal = (Progress.create 0 |> payload));

[<Fact>]
let ``Description is being set for Goal`` () =
    let goal = Goal.create (Guid.NewGuid()) List.empty "desc" |> payload
    Assert.True(Goal.description goal = "desc");

[<Fact>]
let ``AddGoal adds subgoal to the beggining of subgoal list`` () =
    
    let subGoal = Goal.create (Guid.NewGuid()) List.Empty "old subgoal" |> payload
    let goal = Goal.create (Guid.NewGuid()) ([ subGoal ]) "goal" |> payload
    let newSubgoal = Goal.create (Guid.NewGuid()) List.empty "sub goal" |> payload

    let newGoal = Goal.addSubgoal goal newSubgoal |> payload

    Assert.True(Goal.subGoals newGoal |> List.item 0 = newSubgoal);
    Assert.True(Goal.subGoals newGoal |> List.item 1  = subGoal);

[<Fact>]
let ``AddGoal does not allow duplicates`` () =
    let subGoalsGuid =  Guid.NewGuid();
    let subGoal = Goal.create subGoalsGuid List.Empty "old subgoal" |> payload
    let goal = Goal.create (Guid.NewGuid()) ([ subGoal ]) "goal" |> payload
    let newSubgoal = Goal.create subGoalsGuid List.empty "old subgoal" |> payload

    let result = Goal.addSubgoal goal newSubgoal

    Assert.True(isFailure result);

open CQRS
open CQRS.QueryHandler

[<Fact>]
let ``GetGoalById query is being resolved`` () =
    let id = Guid.NewGuid()
    let query : GetGoalById.Query =  { Id = id }

    let result = handle query |> payload

    Assert.Equal(id, Goal.id result);

[<Fact>]
let ``GetGoalProgressByGoalId is being resolved`` () =
    let id = Guid.NewGuid()
    let query : GetGoalProgressByGoalId.Query =  { Id = id }
    let expectedProgress = Progress.create 13 |> payload;
    
    let result = handle query |> payload

    Assert.Equal(expectedProgress, result);


type private  UnknownQuery = { Id : string }
[<Fact>]
let ``Throws on not registered query`` () =
    let query : UnknownQuery= { Id = "test" }

    Assert.Throws<System.NotImplementedException>(fun () -> handle query |> ignore) |> ignore;