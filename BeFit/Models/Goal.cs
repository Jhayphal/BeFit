namespace BeFit.Models;

public sealed record class Goal(int Id, string Description, Schedule Schedule);

public sealed record class GoalState(Goal Goal, bool Done);
